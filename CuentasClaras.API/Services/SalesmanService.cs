using Microsoft.EntityFrameworkCore;
using CuentasClaras.Application.DTOs;
using CuentasClaras.Domain;
using CuentasClaras.Infrastructure.Persistence;
using CuentasClaras.API.Services.Interfaces;

namespace CuentasClaras.API.Services
{
    public class SalesmanService : ISalesmanService
    {
        private readonly CuentasClarasDbContext _context;

        public SalesmanService(CuentasClarasDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SalesmanDto>> GetAllAsync()
        {
            return await _context.Salesmen
                .OrderBy(s => s.Name)
                .Select(s => new SalesmanDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    CommissionPercentage = s.CommissionPercentage,
                    Phone = s.Phone
                })
                .ToListAsync();
        }

        public async Task<SalesmanDto?> GetByIdAsync(int id)
        {
            var salesman = await _context.Salesmen
                .FirstOrDefaultAsync(s => s.Id == id);

            if (salesman == null)
                return null;

            return new SalesmanDto
            {
                Id = salesman.Id,
                Name = salesman.Name,
                CommissionPercentage = salesman.CommissionPercentage,
                Phone = salesman.Phone
            };
        }

        public async Task<SalesmanDto> CreateAsync(SalesmanDto salesmanDto)
        {
            var salesman = new Salesman
            {
                Name = salesmanDto.Name,
                CommissionPercentage = salesmanDto.CommissionPercentage,
                Phone = salesmanDto.Phone
            };

            _context.Salesmen.Add(salesman);
            await _context.SaveChangesAsync();

            salesmanDto.Id = salesman.Id;
            return salesmanDto;
        }

        public async Task<bool> UpdateAsync(int id, SalesmanDto salesmanDto)
        {
            var salesman = await _context.Salesmen.FindAsync(id);
            if (salesman == null)
                return false;

            salesman.Name = salesmanDto.Name;
            salesman.CommissionPercentage = salesmanDto.CommissionPercentage;
            salesman.Phone = salesmanDto.Phone;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var salesman = await _context.Salesmen.FindAsync(id);
            if (salesman == null)
                return false;

            // Check if salesman has customers
            var hasCustomers = await _context.Customers
                .AnyAsync(c => c.SalesmanId == id);

            if (hasCustomers)
                throw new InvalidOperationException("Cannot delete salesman with assigned customers.");

            _context.Salesmen.Remove(salesman);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Salesmen.AnyAsync(s => s.Id == id);
        }
    }
}
