using Microsoft.AspNetCore.Mvc;
using SpotOnAPI.V2.Interfaces;
using SpotOnAPI.V2.Models;

namespace SpotOnAPI.V2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollarController : ControllerBase
    {
        private readonly ICollarRepository _collarRepository;

        public CollarController(ICollarRepository collarRepository)
        {
            _collarRepository = collarRepository;
        }

        [HttpGet("GetCollars")]
        public async Task<IActionResult> GetCollars()
        {
            var x = await _collarRepository.GetCollars();
            return Ok(x);
        }

        [HttpGet("GetCustomerCollars/{id}")]
        public async Task<IActionResult> GetCustomerCollars(int id)
        {
            var x = await _collarRepository.GetCustomerCollars(id);
            if (x is null)
                return NotFound("Collar not found :(");
            return Ok(x);
        }

        [HttpPost("CreateCollar")]
        public async Task<IActionResult> CreateCollar([FromBody] Collar collar)
        {
            var collars = await _collarRepository.CreateCollar(collar);
            return Ok(collars);
        }


        [HttpPut("EditCollar")]
        public async Task<IActionResult> EditCollar([FromBody] Collar collar)
        {
            var editedCollar = await _collarRepository.EditCollar(collar);
            if (editedCollar is null)
                return NotFound("Collar not found :(");
            return Ok(editedCollar);
        }

        [HttpDelete("DeleteCollar/{id}")]
        public async Task<IActionResult> DeleteCollar(Guid id)
        {
            var collar = await _collarRepository.DeleteCollar(id);
            return Ok(collar);
        }

    }
}
