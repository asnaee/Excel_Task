using Microsoft.EntityFrameworkCore;
using work_01.Model;

namespace work_01.Allergie_REPO
{
    public class AllergieREPO : IAllergie
    {
        private readonly AsigmentContext _context;
        public AllergieREPO(AsigmentContext _context)
        {
            this._context = _context;
        }
        public async Task<List<Allergie>> GetAll()
        {
            return await _context.Allergies.ToListAsync();
        }

        public async Task<Allergie> GetById(int allergieId)
        {
            return await _context.Allergies.FindAsync(allergieId);
        }

        public async Task Create(Allergie allergie)
        {
            _context.Allergies.Add(allergie);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(Allergie allergie)
        {
            _context.Entry(allergie).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int allergieId)
        {
            var allergie = await _context.Allergies.FindAsync(allergieId);
            if (allergie != null)
            {
                _context.Allergies.Remove(allergie);
                await _context.SaveChangesAsync();
            }
        }
    }
}
