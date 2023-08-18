using API_testing2.Context;
using API_testing2.Models;
using API_testing2.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace API_testing2.Repository
{
    public class NumeroVillaRepository : Repository<NumeroVilla>, INumeroVillaRepository
    {
        /// <summary>
        /// Igual a la capa Services, pero éste hereda de interfaces (mejor)
        /// 
        /// En program.cs:
        /// builder.Services.AddScoped<IVillaRepository, VillaRepository>();
        /// </summary>
        private readonly DbContext _dbContext;

        public NumeroVillaRepository(ContextDB dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<NumeroVilla> Update(NumeroVilla entity)
        {
            entity.Update = DateTime.Now;
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

    }
}
