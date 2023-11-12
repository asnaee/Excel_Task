using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using work_01.Allergie_REPO;
using work_01.Model;

namespace work_01.Controllers.Allergie_Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllergieController : ControllerBase
    {
        private readonly IAllergie _allergieRepo;

        public AllergieController(IAllergie allergieRepo)
        {
            _allergieRepo = allergieRepo;
        }

        [HttpGet]
        public async Task<ActionResult<List<Allergie>>> GetAll()
        {
            try
            {
                var allergies = await _allergieRepo.GetAll();
                return Ok(allergies);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet("{allergieId}")]
       // [Route("{one}")]
        public async Task<ActionResult<Allergie>> GetById(int allergieId)
        {
            try
            {
                var allergie = await _allergieRepo.GetById(allergieId);
                if (allergie == null)
                {
                    return NotFound();
                }
                return Ok(allergie);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Allergie allergie)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _allergieRepo.Create(allergie);
                return CreatedAtAction(nameof(GetById), new { allergieId = allergie.AllergiesID }, allergie);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPut("{allergieId}")]
        public async Task<ActionResult> Edit(int allergieId, [FromBody] Allergie allergie)
        {
            try
            {
                if (allergieId != allergie.AllergiesID)
                {
                    return BadRequest();
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _allergieRepo.Edit(allergie);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpDelete("{allergieId}")]
        public async Task<ActionResult> Delete(int allergieId)
        {
            try
            {
                await _allergieRepo.Delete(allergieId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }

}
