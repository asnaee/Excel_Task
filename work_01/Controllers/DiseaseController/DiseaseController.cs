using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using work_01.Disease_REPO;
using work_01.Model;

namespace work_01.Controllers.DiseaseController
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiseaseController : ControllerBase
    {
        private readonly IDisease _diseaseRepo;

        public DiseaseController(IDisease diseaseRepo)
        {
            _diseaseRepo = diseaseRepo;
        }

        [HttpGet]
        public async Task<ActionResult<List<DiseaseInformation>>> GetAll()
        {
            try
            {
                var diseases = await _diseaseRepo.GetAll();
                return Ok(diseases);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet("{diseaseId}")]
        public async Task<ActionResult<DiseaseInformation>> GetById(int diseaseId)
        {
            try
            {
                var disease = await _diseaseRepo.GetById(diseaseId);
                if (disease == null)
                {
                    return NotFound();
                }
                return Ok(disease);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] DiseaseInformation disease)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _diseaseRepo.Create(disease);
                return CreatedAtAction(nameof(GetById), new { diseaseId = disease.DiseaseID }, disease);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPut("{diseaseId}")]
        public async Task<ActionResult> Edit(int diseaseId, [FromBody] DiseaseInformation disease)
        {
            try
            {
                if (diseaseId != disease.DiseaseID)
                {
                    return BadRequest();
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _diseaseRepo.Edit(disease);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpDelete("{diseaseId}")]
        public async Task<ActionResult> Delete(int diseaseId)
        {
            try
            {
                await _diseaseRepo.Delete(diseaseId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }

}
