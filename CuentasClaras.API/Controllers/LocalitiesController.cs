using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CuentasClaras.Infrastructure.Persistence;

namespace CuentasClaras.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocalitiesController : ControllerBase
    {
        private readonly CuentasClarasDbContext _context;

        public LocalitiesController(CuentasClarasDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetLocalities()
        {
            var localities = await _context.Localities
                .Include(l => l.Department)
                .OrderBy(l => l.Department.Name)
                .ThenBy(l => l.Name)
                .Select(l => new { l.Id, l.Name, DepartmentName = l.Department.Name })
                .ToListAsync();

            return Ok(localities);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetLocality(int id)
        {
            var locality = await _context.Localities
                .Include(l => l.Department)
                .Where(l => l.Id == id)
                .Select(l => new { l.Id, l.Name, DepartmentName = l.Department.Name })
                .FirstOrDefaultAsync();

            if (locality == null)
            {
                return NotFound();
            }

            return Ok(locality);
        }
    }
}
