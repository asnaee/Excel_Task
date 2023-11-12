using work_01.Model.ViewModel;
using work_01.Model;
using Microsoft.EntityFrameworkCore;

namespace work_01.Patien_Repo
{
    public class PatientREPO : IPatient
    {
        private readonly AsigmentContext _context;
        public PatientREPO(AsigmentContext _context)
        {
            this._context = _context;
        }
        public async Task<List<Patient>> GetAll()
        {
            return await _context.Patients.ToListAsync();
        }

        public async Task<List<Patient>> GetAllPatientsIncludingDetails()
        {
            ////        var query = _context.Patients
            ////.Include(n => n.NCDDetails).ThenInclude(c => c.NCD)
            ////.Include(a => a.AllergiesDetails).ThenInclude(g => g.Allergie)
            ////.Include(d => d.DiseaseInformation)
            ////.Select(patient => new
            ////{
            ////    patient.PatientID,
            ////    patient.PatientName,
            ////    patient.Epilepsy,
            ////    DiseaseName = patient.DiseaseInformation.DiseaseName,
            ////    NCDNames = patient.NCDDetails.Select(ncdDetail => ncdDetail.NCD.NCDName),
            ////    AllergiesNames = patient.AllergiesDetails.Select(allergiesDetail => allergiesDetail.Allergie.AllergiesName)
            ////})
            ////.ToList();

            return await _context.Patients
                .Include(n => n.NCDDetails).ThenInclude(c => c.NCD)
                .Include(a => a.AllergiesDetails).ThenInclude(g => g.Allergie)
                .Include(d => d.DiseaseInformation)
                .ToListAsync();
        }
        public async Task<object> GetById(int patientId)
        {
            var query = await _context.Patients
                .Where(patient => patient.PatientID == patientId)
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
                .FirstOrDefaultAsync();

            return query;
        }

        public async Task Create(PatientVM patientVM)
        {
            var patient = new Patient
            {
                PatientName = patientVM.PatientName,
                Epilepsy = patientVM.Epilepsy,
                DiseaseID = patientVM.DiseaseId
            };

            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();

            foreach (var ncdId in patientVM.NCDID)
            {
                var ncdDetails = new NCD_Detail
                {
                    NCDID = ncdId,
                    PatientID = patient.PatientID
                };
                _context.NCDDetails.Add(ncdDetails);
            }
            foreach (var allergyId in patientVM.AllergiesID)
            {
                var allergiesDetails = new Allergies_Detail
                {
                    AllergiesID = allergyId,
                    PatientID = patient.PatientID
                };
                _context.AllergiesDetails.Add(allergiesDetails);
            }

            await _context.SaveChangesAsync();
        }

        public async Task Edit(int patientId, PatientVM patientVM)
        {
            var patient = await _context.Patients.FindAsync(patientId);
            if (patient == null)
            {
                throw new NotFoundException("Patient not found");
            }

            patient.PatientName = patientVM.PatientName;
            patient.Epilepsy = patientVM.Epilepsy;
            patient.DiseaseID = patientVM.DiseaseId;

            // Remove existing NCD_Details and Allergies_Details
            _context.NCDDetails.RemoveRange(_context.NCDDetails.Where(nd => nd.PatientID == patientId));
            _context.AllergiesDetails.RemoveRange(_context.AllergiesDetails.Where(ad => ad.PatientID == patientId));

            // Add new NCD_Details
            foreach (var ncdId in patientVM.NCDID)
            {
                var ncdDetails = new NCD_Detail
                {
                    NCDID = ncdId,
                    PatientID = patient.PatientID
                };
                _context.NCDDetails.Add(ncdDetails);
            }

            // Add new Allergies_Details
            foreach (var allergyId in patientVM.AllergiesID)
            {
                var allergiesDetails = new Allergies_Detail
                {
                    AllergiesID = allergyId,
                    PatientID = patient.PatientID
                };
                _context.AllergiesDetails.Add(allergiesDetails);
            }

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int patientId)
        {
            var patient = await _context.Patients.FindAsync(patientId);
            if (patient == null)
            {
                throw new NotFoundException("Patient not found");
            }

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
        }
    }
}
