using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlanDiario.Infrastructure.Persistence;

namespace PlanDiario.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CollectorsController : ControllerBase
    {
        private readonly PlanDiarioDbContext _context;

        public CollectorsController(PlanDiarioDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetCollectors()
        {
            var collectors = await _context.Collectors
                .OrderBy(c => c.Name)
                .Select(c => new { c.Id, c.Name, c.CommisionPercentage })
                .ToListAsync();

            return Ok(collectors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetCollector(int id)
        {
            var collector = await _context.Collectors
                .Where(c => c.Id == id)
                .Select(c => new { c.Id, c.Name, c.CommisionPercentage })
                .FirstOrDefaultAsync();

            if (collector == null)
            {
                return NotFound();
            }

            return Ok(collector);
        }
    }
}