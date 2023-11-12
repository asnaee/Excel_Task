using work_01.Model;

namespace work_01.Allergie_REPO
{
    public interface IAllergie
    {
        Task<List<Allergie>> GetAll();
        Task<Allergie> GetById(int AllergieId);
        Task Create(Allergie  allergie);
        Task Edit(Allergie  allergie);
        Task Delete(int AllergieId);
    }
}
