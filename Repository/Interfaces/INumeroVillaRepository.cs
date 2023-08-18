using API_testing2.Models;

namespace API_testing2.Repository.Interfaces
{
    public interface INumeroVillaRepository : IRepository<NumeroVilla>
    {
        Task<NumeroVilla> Update(NumeroVilla entity);
    }
}
