using Microsoft.EntityFrameworkCore;
using work_01.Model;

namespace work_01.Ncd_REPO
{
    public class NcdRepo : Incd
    {
        private readonly AsigmentContext _context;
        public NcdRepo(AsigmentContext _context)
        {
                this._context = _context;
        }
        public async Task<List<NCD>> GetAll()
        {
            return await _context.NCDs.ToListAsync();
        }

        public async Task<NCD> GetById(int ncdId)
        {
            return await _context.NCDs.FindAsync(ncdId);
        }

        public async Task Create(NCD ncd)
        {
            _context.NCDs.Add(ncd);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(NCD ncd)
        {
            _context.Entry(ncd).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int ncdId)
        {
            var ncd = await _context.NCDs.FindAsync(ncdId);
            if (ncd != null)
            {
                _context.NCDs.Remove(ncd);
                await _context.SaveChangesAsync();
            }
        }
    }
}
