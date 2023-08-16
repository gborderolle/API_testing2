using API_testing2.Models;

namespace API_testing2.Repository.Interfaces
{
    public interface IVillaRepository : IRepository<Villa>
    {
        Task<Villa> Update(Villa entity);
    }
}
