using Microsoft.EntityFrameworkCore;
using PlanDiario.API.Services.Interfaces;
using PlanDiario.Application.DTOs;
using PlanDiario.Infrastructure.Persistence;
using PlanDiario.Domain;

namespace PlanDiario.API.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly PlanDiarioDbContext _context;

        public CustomerService(PlanDiarioDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CustomerDto>> GetAllCustomersAsync()
        {
            var customers = await _context.Customers
                .Include(c => c.Locality)
                    .ThenInclude(l => l.Department)
                .Include(c => c.Collector)
                .Include(c => c.Salesman)
                .ToListAsync();

            return customers.Select(MapToDto);
        }

        public async Task<CustomerDto?> GetCustomerByIdAsync(Guid id)
        {
            var customer = await _context.Customers
                .Include(c => c.Locality)
                    .ThenInclude(l => l.Department)
                .Include(c => c.Collector)
                .Include(c => c.Salesman)
                .FirstOrDefaultAsync(c => c.Id == id);

            return customer != null ? MapToDto(customer) : null;
        }

        public async Task<CustomerDto?> GetCustomerByDNIAsync(string dni)
        {
            var customer = await _context.Customers
                .Include(c => c.Locality)
                    .ThenInclude(l => l.Department)
                .Include(c => c.Collector)
                .Include(c => c.Salesman)
                .FirstOrDefaultAsync(c => c.DNI == dni);

            return customer != null ? MapToDto(customer) : null;
        }

        public async Task<CustomerDto> CreateCustomerAsync(CreateCustomerRequest request)
        {
            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                DNI = request.DNI,
                Name = request.Name,
                LastName = request.LastName,
                Address1 = request.Address1,
                Address2 = request.Address2,
                Phone1 = request.Phone1,
                Phone2 = request.Phone2,
                Phone3 = request.Phone3,
                Email = request.Email,
                LocalityId = request.LocalityId,
                CollectorId = request.CollectorId,
                SalesmanId = request.SalesmanId
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return await GetCustomerByIdAsync(customer.Id) ?? throw new InvalidOperationException("Customer not found after creation");
        }

        public async Task<CustomerDto> UpdateCustomerAsync(UpdateCustomerRequest request)
        {
            var customer = await _context.Customers.FindAsync(request.Id);
            if (customer == null)
                throw new ArgumentException("Customer not found");

            customer.DNI = request.DNI;
            customer.Name = request.Name;
            customer.LastName = request.LastName;
            customer.Address1 = request.Address1;
            customer.Address2 = request.Address2;
            customer.Phone1 = request.Phone1;
            customer.Phone2 = request.Phone2;
            customer.Phone3 = request.Phone3;
            customer.Email = request.Email;
            customer.LocalityId = request.LocalityId;
            customer.CollectorId = request.CollectorId;
            customer.SalesmanId = request.SalesmanId;

            await _context.SaveChangesAsync();

            return await GetCustomerByIdAsync(customer.Id) ?? throw new InvalidOperationException("Customer not found after update");
        }

        public async Task<bool> DeleteCustomerAsync(Guid id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
                return false;

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CustomerExistsAsync(Guid id)
        {
            return await _context.Customers.AnyAsync(c => c.Id == id);
        }

        public async Task<bool> CustomerExistsByDNIAsync(string dni)
        {
            return await _context.Customers.AnyAsync(c => c.DNI == dni);
        }

        private static CustomerDto MapToDto(Customer customer)
        {
            return new CustomerDto
            {
                Id = customer.Id,
                DNI = customer.DNI,
                Name = customer.Name,
                LastName = customer.LastName,
                Address1 = customer.Address1,
                Address2 = customer.Address2,
                Phone1 = customer.Phone1,
                Phone2 = customer.Phone2,
                Phone3 = customer.Phone3,
                Email = customer.Email,
                LocalityId = customer.LocalityId,
                Locality = new LocalityDto
                {
                    Id = customer.Locality.Id,
                    Name = customer.Locality.Name,
                    PostalCode = customer.Locality.PostalCode,
                    DepartmentId = customer.Locality.DepartmentId,
                    Department = new DepartmentDto
                    {
                        Id = customer.Locality.Department.Id,
                        Name = customer.Locality.Department.Name
                    }
                },
                CollectorId = customer.CollectorId,
                Collector = new CollectorDto
                {
                    Id = customer.Collector.Id,
                    Name = customer.Collector.Name,
                    CommisionPercentage = customer.Collector.CommisionPercentage,
                    Phone = customer.Collector.Phone
                },
                SalesmanId = customer.SalesmanId,
                Salesman = new SalesmanDto
                {
                    Id = customer.Salesman.Id,
                    Name = customer.Salesman.Name,
                    CommissionPercentage = customer.Salesman.CommissionPercentage,
                    Phone = customer.Salesman.Phone
                }
            };
        }
    }
}
