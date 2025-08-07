using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlanDiario.Infrastructure.Persistence;

namespace PlanDiario.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesmenController : ControllerBase
    {
        private readonly PlanDiarioDbContext _context;

        public SalesmenController(PlanDiarioDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetSalesmen()
        {
            var salesmen = await _context.Salesmen
                .OrderBy(s => s.Name)
                .Select(s => new { s.Id, s.Name, s.CommissionPercentage })
                .ToListAsync();

            return Ok(salesmen);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetSalesman(int id)
        {
            var salesman = await _context.Salesmen
                .Where(s => s.Id == id)
                .Select(s => new { s.Id, s.Name, s.CommissionPercentage })
                .FirstOrDefaultAsync();

            if (salesman == null)
            {
                return NotFound();
            }

            return Ok(salesman);
        }
    }
}