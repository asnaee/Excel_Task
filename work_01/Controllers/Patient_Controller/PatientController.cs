using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using work_01.Model.ViewModel;
using work_01.Model;
using work_01.Patien_Repo;
using Microsoft.EntityFrameworkCore;

namespace work_01.Controllers.Patient_Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatient _patientRepo;
        private readonly AsigmentContext _context;

        public PatientController(IPatient patientRepo, AsigmentContext context)
        {
            _patientRepo = patientRepo;
            _context = context; 
        }

        [HttpGet]
        public async Task<ActionResult<List<Patient>>> GetAll()
        {
            var patients = await _patientRepo.GetAll();
            return Ok(patients);
        }
        [HttpGet("All")]
        public IActionResult GetPatientDetails() 
        {
            var query = _context.Patients
                .Include(n => n.NCDDetails).ThenInclude(c => c.NCD)
                .Include(a => a.AllergiesDetails).ThenInclude(g => g.Allergie)
                .Include(d => d.DiseaseInformation)
                .Select(patient => new
                {
                    patient.PatientID,
                    patient.PatientName,
                    patient.Epilepsy,
                    DiseaseName = patient.DiseaseInformation.DiseaseName,
                    NCDNames = patient.NCDDetails.Select(ncdDetail => ncdDetail.NCD.NCDName),
                    AllergiesNames = patient.AllergiesDetails.Select(allergiesDetail => allergiesDetail.Allergie.AllergiesName)
                })
                .ToList();
            return Ok(query);
        }
        // Return as JSON response
        ////public async Task<ActionResult<List<Patient>>> GetAllproduct()
        ////{
        ////    var patients = await _patientRepo.GetAllPatientsIncludingDetails();
        ////    return Ok(patients);
        ////}

        [HttpGet("{patientId}")]
        public async Task<ActionResult<Patient>> GetById(int patientId)
        {
            var patient = await _patientRepo.GetById(patientId);
            if (patient == null)
            {
                return NotFound();
            }
            return Ok(patient);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] PatientVM patientVM)
        {
            await _patientRepo.Create(patientVM);
            return Ok();
        }

        [HttpPut("{patientId}")]
        public async Task<ActionResult> Edit(int patientId, [FromBody] PatientVM patientVM)
        {
            try
            {
                await _patientRepo.Edit(patientId, patientVM);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{patientId}")]
        public async Task<ActionResult> Delete(int patientId)
        {
            try
            {
                await _patientRepo.Delete(patientId);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }

}
