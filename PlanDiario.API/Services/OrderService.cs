using Microsoft.EntityFrameworkCore;
using PlanDiario.API.Services.Interfaces;
using PlanDiario.Application.DTOs;
using PlanDiario.Infrastructure.Persistence;
using PlanDiario.Domain;

namespace PlanDiario.API.Services
{
    public class OrderService : IOrderService
    {
        private readonly PlanDiarioDbContext _context;

        public OrderService(PlanDiarioDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
        {
            var orders = await _context.Orders
                .Include(o => o.Customer)
                    .ThenInclude(c => c.Locality)
                        .ThenInclude(l => l.Department)
                .Include(o => o.Customer.Collector)
                .Include(o => o.Customer.Salesman)
                .Include(o => o.Period)
                .Include(o => o.BusinessType)
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.Product)
                        .ThenInclude(p => p.Subcategory)
                            .ThenInclude(s => s.Category)
                                .ThenInclude(c => c.Line)
                .ToListAsync();

            return orders.Select(MapToDto);
        }

        public async Task<OrderDto?> GetOrderByIdAsync(Guid id)
        {
            var order = await _context.Orders
                .Include(o => o.Customer)
                    .ThenInclude(c => c.Locality)
                        .ThenInclude(l => l.Department)
                .Include(o => o.Customer.Collector)
                .Include(o => o.Customer.Salesman)
                .Include(o => o.Period)
                .Include(o => o.BusinessType)
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.Product)
                        .ThenInclude(p => p.Subcategory)
                            .ThenInclude(s => s.Category)
                                .ThenInclude(c => c.Line)
                .FirstOrDefaultAsync(o => o.Id == id);

            return order != null ? MapToDto(order) : null;
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersByCustomerIdAsync(Guid customerId)
        {
            var orders = await _context.Orders
                .Where(o => o.CustomerId == customerId)
                .Include(o => o.Customer)
                    .ThenInclude(c => c.Locality)
                        .ThenInclude(l => l.Department)
                .Include(o => o.Customer.Collector)
                .Include(o => o.Customer.Salesman)
                .Include(o => o.Period)
                .Include(o => o.BusinessType)
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.Product)
                        .ThenInclude(p => p.Subcategory)
                            .ThenInclude(s => s.Category)
                                .ThenInclude(c => c.Line)
                .ToListAsync();

            return orders.Select(MapToDto);
        }

        public async Task<OrderDto> CreateOrderAsync(CreateOrderRequest request)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var order = new Order
                {
                    Id = Guid.NewGuid(),
                    Description = request.Description,
                    CustomerId = request.CustomerId,
                    SellingPrice = request.SellingPrice,
                    CostPrice = request.CostPrice,
                    PeriodId = request.PeriodId,
                    PeriodName = string.Empty,
                    BusinessTypeId = request.BusinessTypeId,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    OrderStartDate = request.OrderStartDate ?? DateTime.UtcNow,
                    Status = Domain.OrderStatus.Created,
                    WeekDay = (Domain.WeekDay)request.WeekDay,
                    OrderType = (Domain.OrderType)request.OrderType
                };

                _context.Orders.Add(order);
                await _context.SaveChangesAsync(); // Save the Order first

                // Create OrderProduct entities using the quantities from the request
                foreach (var orderProductRequest in request.OrderProducts)
                {
                    var orderProduct = new OrderProduct
                    {
                        OrderId = order.Id,
                        ProductId = orderProductRequest.ProductId,
                        Quantity = orderProductRequest.Quantity,
                    };
                    _context.Set<OrderProduct>().Add(orderProduct);
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return await GetOrderByIdAsync(order.Id) ?? throw new InvalidOperationException("Order not found after creation");
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<OrderDto> UpdateOrderAsync(UpdateOrderRequest request)
        {
            var order = await _context.Orders
                .Include(o => o.OrderProducts)
                .FirstOrDefaultAsync(o => o.Id == request.Id);

            if (order == null)
                throw new ArgumentException("Order not found");

            order.Description = request.Description;
            order.CustomerId = request.CustomerId;
            order.SellingPrice = request.SellingPrice;
            order.CostPrice = request.CostPrice;
            order.PeriodId = request.PeriodId;
            order.BusinessTypeId = request.BusinessTypeId;
            order.OrderStartDate = request.OrderStartDate;
            order.Status = (Domain.OrderStatus)request.Status;
            order.WeekDay = (Domain.WeekDay)request.WeekDay;
            order.OrderType = (Domain.OrderType)request.OrderType;
            order.CreatedAt = request.CreatedAt ?? order.CreatedAt;
            order.UpdatedAt = DateTime.UtcNow;
            _context.Set<OrderProduct>().RemoveRange(order.OrderProducts);

            foreach (var productId in request.ProductIds)
            {
                var orderProduct = new OrderProduct
                {
                    OrderId = order!.Id,
                    ProductId = productId,
                    Quantity = order?.OrderProducts.FirstOrDefault(op => op.ProductId == productId)?.Quantity ?? 1
                };
                _context.Set<OrderProduct>().Add(orderProduct);
            }

            await _context.SaveChangesAsync();
            return await GetOrderByIdAsync(order.Id) ?? throw new InvalidOperationException("Order not found after update");
        }

        public async Task<bool> DeleteOrderAsync(Guid id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
                return false;

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> OrderExistsAsync(Guid id)
        {
            return await _context.Orders.AnyAsync(o => o.Id == id);
        }

        private static OrderDto MapToDto(Order order)
        {
            return new OrderDto
            {
                Id = order.Id,
                Description = order.Description,
                CustomerId = order.CustomerId,
                Customer = new CustomerDto
                {
                    Id = order.Customer.Id,
                    DNI = order.Customer.DNI,
                    Name = order.Customer.Name,
                    LastName = order.Customer.LastName,
                    Address1 = order.Customer.Address1,
                    Address2 = order.Customer.Address2,
                    Phone1 = order.Customer.Phone1,
                    Phone2 = order.Customer.Phone2,
                    Phone3 = order.Customer.Phone3,
                    Email = order.Customer.Email,
                    LocalityId = order.Customer.LocalityId,
                    Locality = new LocalityDto
                    {
                        Id = order.Customer.Locality.Id,
                        Name = order.Customer.Locality.Name,
                        PostalCode = order.Customer.Locality.PostalCode,
                        DepartmentId = order.Customer.Locality.DepartmentId,
                        Department = new DepartmentDto
                        {
                            Id = order.Customer.Locality.Department.Id,
                            Name = order.Customer.Locality.Department.Name
                        }
                    },
                    CollectorId = order.Customer.CollectorId,
                    Collector = new CollectorDto
                    {
                        Id = order.Customer.Collector.Id,
                        Name = order.Customer.Collector.Name,
                        CommisionPercentage = order.Customer.Collector.CommisionPercentage,
                        Phone = order.Customer.Collector.Phone
                    },
                    SalesmanId = order.Customer.SalesmanId,
                    Salesman = new SalesmanDto
                    {
                        Id = order.Customer.Salesman.Id,
                        Name = order.Customer.Salesman.Name,
                        CommissionPercentage = order.Customer.Salesman.CommissionPercentage,
                        Phone = order.Customer.Salesman.Phone
                    }
                },
                SellingPrice = order.SellingPrice,
                CostPrice = order.CostPrice,
                PeriodName = order.PeriodName,
                PeriodId = order.PeriodId,
                Period = new PeriodDto
                {
                    Id = order.Period.Id,
                    Name = order.Period.Name
                },
                BusinessTypeId = order.BusinessTypeId,
                BusinessType = new BusinessTypeDto
                {
                    Id = order.BusinessType.Id,
                    Name = order.BusinessType.Name
                },
                CreatedAt = order.CreatedAt,
                UpdatedAt = order.UpdatedAt,
                OrderStartDate = order.OrderStartDate,
                Status = (Application.DTOs.OrderStatus)order.Status,
                WeekDay = (Application.DTOs.WeekDay)order.WeekDay,
                OrderType = (Application.DTOs.OrderType)order.OrderType,
                OrderProducts = order.OrderProducts.Select(op => new OrderProductDto
                {
                    Id = op.Id,
                    OrderId = op.OrderId,
                    ProductId = op.ProductId,
                    Quantity = op.Quantity,
                    Product = new ProductDto
                    {
                        Id = op.Product.Id,
                        Name = op.Product.Name,
                        Description = op.Product.Description,
                        Characteristics = op.Product.Characteristics,
                        CostPrice = op.Product.CostPrice,
                        SalePrice = op.Product.SalePrice,
                        Image = op.Product.Image,
                        SubcategoryId = op.Product.SubcategoryId,
                        Subcategory = new SubcategoryDto
                        {
                            Id = op.Product.Subcategory.Id,
                            Name = op.Product.Subcategory.Name,
                            CategoryId = op.Product.Subcategory.CategoryId,
                            Category = new CategoryDto
                            {
                                Id = op.Product.Subcategory.Category.Id,
                                Name = op.Product.Subcategory.Category.Name,
                                LineId = op.Product.Subcategory.Category.LineId,
                                Line = new LineDto
                                {
                                    Id = op.Product.Subcategory.Category.Line.Id,
                                    Name = op.Product.Subcategory.Category.Line.Name
                                }
                            }
                        },
                        Quantity = op.Quantity,
                        ProviderUrl = op.Product.ProviderUrl
                    }
                }).ToList()
            };
        }
    }
}
