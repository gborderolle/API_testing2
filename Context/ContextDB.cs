using API_testing2.Models;
using API_testing2.Models.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace API_testing2.Context
{
    public class ContextDB : DbContext
    {
        public ContextDB(DbContextOptions<ContextDB> options) : base(options)
        {
        }

        public DbSet<Villa> Villa { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
                new Villa()
                {
                    Id = new Guid(),
                    Name = "Villa Real 1",
                    Details = "La Villa Real 1 es grande y linda",
                    ImageUrl = "",
                    Tenants = 5,
                    SizeMeters = 50,
                    Fee = 200,
                    Creation = DateTime.Now,
                    Update = DateTime.Now
                },
                new Villa()
                {
                    Id = new Guid(),
                    Name = "Villa Real 2",
                    Details = "La Villa Real 2 es chica",
                    ImageUrl = "",
                    Tenants = 2,
                    SizeMeters = 23,
                    Fee = 100,
                    Creation = DateTime.Now,
                    Update = DateTime.Now
                }
            );
        }

        internal async Task<List<VillaDto>> GetVillas()
        {
            return await Villa.Select(c => c.ToDTO()).ToListAsync();
        }

        internal async Task<Villa> GetVilla(Guid id)
        {
            return await Villa.FirstAsync(x => x.Id == id);
        }

        internal async Task<Villa> CreateVillaAsync(VillaDto villaDto)
        {
            Villa villa = new()
            {
                Name = villaDto.Name,
                Details = villaDto.Details,
                ImageUrl = villaDto.ImageUrl,
                Tenants = villaDto.Tenants,
                SizeMeters = villaDto.SizeMeters,
                Fee = villaDto.Fee,
                Creation = villaDto.Creation,
                Update = DateTime.Now
            };
            EntityEntry<Villa> response = await Villa.AddAsync(villa);
            await SaveChangesAsync();
            return await GetVilla(response.Entity.Id);
        }

        internal async Task<bool> UpdateVilla(VillaDto villaDto)
        {
            Villa villa = new()
            {
                Id = villaDto.Id,
                Name = villaDto.Name,
                Details = villaDto.Details,
                ImageUrl = villaDto.ImageUrl,
                Tenants= villaDto.Tenants,
                SizeMeters= villaDto.SizeMeters,
                Fee= villaDto.Fee,
                Creation= villaDto.Creation,
                Update= villaDto.Update,

            };
            Villa.Update(villa);
            await SaveChangesAsync();
            return true;
        }

        internal async Task<bool> DeleteVilla(Guid id)
        {
            Villa entity = await GetVilla(id);
            Villa.Remove(entity);
            SaveChanges();
            return true;
        }

        internal bool Exists(VillaDto villa)
        {
            return Villa.Any(v => v.Name.ToLower() == villa.Name.ToLower());
        }
    }
}
