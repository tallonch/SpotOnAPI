using Microsoft.AspNetCore.Mvc;
using SpotOnAPI.V2.Interfaces;
using SpotOnAPI.V2.Models;

namespace SpotOnAPI.V2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollarController : ControllerBase
    {

        // Creating the variables for DB connection
        private readonly ICollarRepository _collarRepository;

        public CollarController(ICollarRepository collarRepository)
        {
            _collarRepository = collarRepository;
        }

        [HttpGet("GetCollars")]
        public async Task<IActionResult> GetCollars()
        {
            try
            {
                var x = await _collarRepository.GetCollars();
                return Ok(x);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("GetCustomerCollars/{id}")]
        public async Task<IActionResult> GetCustomerCollars(int id)
        {
            try
            {
                var x = await _collarRepository.GetCustomerCollars(id);
                if (x.Count == 0)
                    return NotFound("Collar not found :(");
                return Ok(x);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("CreateCollar")]
        public async Task<IActionResult> CreateCollar([FromBody] Collar collar)
        {
            try
            {
                var collars = await _collarRepository.CreateCollar(collar);
                if (collars.UserId == 99999)
                    return BadRequest("Collar already exists. :(");
                if (collars.UserId == 99998)
                    return BadRequest("Longitude needs to be between -180 and 180.");
                if (collars.UserId == 99997)
                    return BadRequest("Latitude needs to be between -90 and 90.");
                return Ok(collars);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("EditCollar")]
        public async Task<IActionResult> EditCollar([FromBody] Collar collar)
        {
            try
            {
                var editedCollar = await _collarRepository.EditCollar(collar);
                if (editedCollar.UserId == 99999)
                    return NotFound("Collar not found :(");
                if (editedCollar.UserId == 99998)
                    return NotFound("Longitude needs to be between -180 and 180.");
                if (editedCollar.UserId == 99997)
                    return NotFound("Latitude needs to be between -90 and 90.");
                return Ok(editedCollar);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("DeleteCollar/{id}")]
        public async Task<IActionResult> DeleteCollar(Guid id)
        {
            try
            {
                var collar = await _collarRepository.DeleteCollar(id);
                if (collar == false)
                    return NotFound("Collar not found :(");
                return Ok(collar);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}