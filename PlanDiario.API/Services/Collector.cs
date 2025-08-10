using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlanDiario.API.Services.Interfaces;
using PlanDiario.Application.DTOs;
using PlanDiario.Domain;
using PlanDiario.Infrastructure.Persistence;

namespace PlanDiario.API.Services
{
    public class CollectorService : ICollectorService
    {
        private readonly PlanDiarioDbContext _context;

        public CollectorService(PlanDiarioDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CollectorDto>> GetAllAsync()
        {
            return await _context.Collectors
                .OrderBy(c => c.Name)
                .Select(c => new CollectorDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    CommisionPercentage = c.CommisionPercentage,
                    Phone = c.Phone
                })
                .ToListAsync();
        }

        public async Task<CollectorDto?> GetByIdAsync(int id)
        {
            var collector = await _context.Collectors
                .FirstOrDefaultAsync(c => c.Id == id);

            if (collector == null)
                return null;

            return new CollectorDto
            {
                Id = collector.Id,
                Name = collector.Name,
                CommisionPercentage = collector.CommisionPercentage,
                Phone = collector.Phone
            };
        }

        public async Task<CollectorDto> CreateAsync(CollectorDto collectorDto)
        {
            var collector = new Collector
            {
                Name = collectorDto.Name,
                CommisionPercentage = collectorDto.CommisionPercentage,
                Phone = collectorDto.Phone
            };

            _context.Collectors.Add(collector);
            await _context.SaveChangesAsync();

            collectorDto.Id = collector.Id;
            return collectorDto;
        }

        public async Task<bool> UpdateAsync(int id, CollectorDto collectorDto)
        {
            var collector = await _context.Collectors.FindAsync(id);
            if (collector == null)
                return false;

            collector.Name = collectorDto.Name;
            collector.CommisionPercentage = collectorDto.CommisionPercentage;
            collector.Phone = collectorDto.Phone;

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
            var collector = await _context.Collectors.FindAsync(id);
            if (collector == null)
                return false;

            // Check if collector has customers
            var hasCustomers = await _context.Customers
                .AnyAsync(c => c.CollectorId == id);

            if (hasCustomers)
                throw new InvalidOperationException("Cannot delete collector with assigned customers.");

            _context.Collectors.Remove(collector);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Collectors.AnyAsync(c => c.Id == id);
        }
    }
}
