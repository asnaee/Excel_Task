using Microsoft.EntityFrameworkCore;
using work_01.Model;

namespace work_01.Disease_REPO
{
    public class DiseaseInformationREPO:IDisease
    {
        private readonly AsigmentContext _context;
        public DiseaseInformationREPO(AsigmentContext _context)
        {
            this._context = _context;
        }

        public async Task<List<DiseaseInformation>> GetAll()
        {
            return await _context.DiseaseInformations.ToListAsync();
        }

        public async Task<DiseaseInformation> GetById(int diseaseId)
        {
            return await _context.DiseaseInformations.FindAsync(diseaseId);
        }

        public async Task Create(DiseaseInformation disease)
        {
            _context.DiseaseInformations.Add(disease);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(DiseaseInformation disease)
        {
            _context.Entry(disease).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int diseaseId)
        {
            var disease = await _context.DiseaseInformations.FindAsync(diseaseId);
            if (disease != null)
            {
                _context.DiseaseInformations.Remove(disease);
                await _context.SaveChangesAsync();
            }
        }
    }
}
