using work_01.Model;

namespace work_01.Disease_REPO
{
    public interface IDisease
    {
        Task<List<DiseaseInformation>> GetAll();
        Task<DiseaseInformation> GetById(int diseaseid);
        Task Create(DiseaseInformation disease);
        Task Edit(DiseaseInformation disease);
        Task Delete(int diseaseid);
    }
}
