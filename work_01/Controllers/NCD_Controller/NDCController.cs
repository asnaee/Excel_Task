using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using work_01.Model;
using work_01.Ncd_REPO;

namespace work_01.Controllers.NCD_Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class NDCController : ControllerBase
    {
        private readonly Incd _ncdRepo;

        public NDCController(Incd ncdRepo)
        {
            _ncdRepo = ncdRepo;
        }

        [HttpGet]
        public async Task<ActionResult<List<NCD>>> GetAll()
        {
            var ncds = await _ncdRepo.GetAll();
            return Ok(ncds);
        }

        [HttpGet("{ncdId}")]
        public async Task<ActionResult<NCD>> GetById(int ncdId)
        {
            var ncd = await _ncdRepo.GetById(ncdId);
            if (ncd == null)
            {
                return NotFound();
            }
            return Ok(ncd);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] NCD ncd)
        {
            await _ncdRepo.Create(ncd);
            return CreatedAtAction(nameof(GetById), new { ncdId = ncd.NCDID }, ncd);
        }

        [HttpPut("{ncdId}")]
        public async Task<ActionResult> Edit(int ncdId, [FromBody] NCD ncd)
        {
            if (ncdId != ncd.NCDID)
            {
                return BadRequest();
            }

            await _ncdRepo.Edit(ncd);

            return NoContent();
        }

        [HttpDelete("{ncdId}")]
        public async Task<ActionResult> Delete(int ncdId)
        {
            await _ncdRepo.Delete(ncdId);
            return NoContent();
        }
    }
}

