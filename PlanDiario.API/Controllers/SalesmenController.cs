using Microsoft.AspNetCore.Mvc;
using PlanDiario.API.Services.Interfaces;
using PlanDiario.Application.DTOs;

namespace PlanDiario.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesmenController : ControllerBase
    {
        private readonly ISalesmanService _salesmanService;
        public SalesmenController(ISalesmanService salesmanService)
        {
            _salesmanService = salesmanService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalesmanDto>>> GetSalesmen()
        {
            var salesmen = await _salesmanService.GetAllAsync();
            return Ok(salesmen);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SalesmanDto>> GetSalesman(int id)
        {
            var salesman = await _salesmanService.GetByIdAsync(id);

            if (salesman == null)
            {
                return NotFound($"Salesman with ID {id} not found.");
            }

            return Ok(salesman);
        }

        [HttpPost]
        public async Task<ActionResult<SalesmanDto>> CreateSalesman(SalesmanDto salesmanDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdSalesman = await _salesmanService.CreateAsync(salesmanDto);
            return CreatedAtAction(nameof(GetSalesman), new { id = createdSalesman.Id }, createdSalesman);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSalesman(int id, SalesmanDto salesmanDto)
        {
            if (id != salesmanDto.Id)
            {
                return BadRequest("ID mismatch.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updated = await _salesmanService.UpdateAsync(id, salesmanDto);
            if (!updated)
            {
                return NotFound($"Salesman with ID {id} not found.");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalesman(int id)
        {
            try
            {
                var deleted = await _salesmanService.DeleteAsync(id);
                if (!deleted)
                {
                    return NotFound($"Salesman with ID {id} not found.");
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