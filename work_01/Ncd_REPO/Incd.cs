using work_01.Model;

namespace work_01.Ncd_REPO
{
    public interface Incd
    {
        Task<List<NCD>> GetAll();
        Task<NCD> GetById(int ncdId);
        Task Create(NCD ncd);
        Task Edit(NCD ncd);
        Task Delete(int ncdId);
    }
}
