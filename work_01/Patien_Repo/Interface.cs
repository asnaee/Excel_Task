using work_01.Model.ViewModel;
using work_01.Model;

namespace work_01.Patien_Repo
{
    public interface IPatient
    {
        Task<List<Patient>> GetAll();
        Task<List<Patient>> GetAllPatientsIncludingDetails();
        Task<object> GetById(int patientId);
        Task Create(PatientVM patientVM);
        Task Edit(int patientId, PatientVM patientVM);
        Task Delete(int patientId);
    }
}
