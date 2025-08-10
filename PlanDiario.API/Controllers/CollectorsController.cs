using Microsoft.AspNetCore.Mvc;
using PlanDiario.Application.DTOs;
using PlanDiario.API.Services.Interfaces;

namespace PlanDiario.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CollectorsController : ControllerBase
    {
        private readonly ICollectorService _collectorService;

        public CollectorsController(ICollectorService collectorService)
        {
            _collectorService = collectorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CollectorDto>>> GetCollectors()
        {
            var collectors = await _collectorService.GetAllAsync();
            return Ok(collectors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CollectorDto>> GetCollector(int id)
        {
            var collector = await _collectorService.GetByIdAsync(id);

            if (collector == null)
            {
                return NotFound($"Collector with ID {id} not found.");
            }

            return Ok(collector);
        }

        [HttpPost]
        public async Task<ActionResult<CollectorDto>> CreateCollector(CollectorDto collectorDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdCollector = await _collectorService.CreateAsync(collectorDto);
            return CreatedAtAction(nameof(GetCollector), new { id = createdCollector.Id }, createdCollector);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCollector(int id, CollectorDto collectorDto)
        {
            if (id != collectorDto.Id)
            {
                return BadRequest("ID mismatch.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updated = await _collectorService.UpdateAsync(id, collectorDto);
            if (!updated)
            {
                return NotFound($"Collector with ID {id} not found.");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCollector(int id)
        {
            try
            {
                var deleted = await _collectorService.DeleteAsync(id);
                if (!deleted)
                {
                    return NotFound($"Collector with ID {id} not found.");
                }

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}