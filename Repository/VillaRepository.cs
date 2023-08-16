using API_testing2.Context;
using API_testing2.Models;
using API_testing2.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace API_testing2.Repository
{
    public class VillaRepository : Repository<Villa>, IVillaRepository
    {
        /// <summary>
        /// Igual a la capa Services, pero éste hereda de interfaces
        /// </summary>
        private readonly DbContext _dbContext;

        public VillaRepository(ContextDB dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Villa> Update(Villa entity)
        {
            entity.Update = DateTime.Now;
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

    }
}
