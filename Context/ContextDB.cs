using API_testing2.Models;
using API_testing2.Models.Dto;
using Azure;
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
            modelBuilder.Entity<Villa>()
                .HasData(
                new Villa()
                {
                    Id = 1,
                    Name = "Villa 1",
                    Details = "",
                    ImageUrl = "",
                    Tenants = 0,
                    SizeMeters = 0,
                    Fee = 10,
                    Creation = DateTime.Now,
                    Update = DateTime.Now
                },
                new Villa()
                {
                    Id = 2,
                    Name = "Villa 2",
                    Details = "",
                    ImageUrl = "",
                    Tenants = 0,
                    SizeMeters = 0,
                    Fee = 20,
                    Creation = DateTime.Now,
                    Update = DateTime.Now
                }
                );
        }

        internal async Task<List<VillaDto>> GetVillas()
        {
            return await Villa.Select(c => c.ToDTO()).ToListAsync();
        }

        internal async Task<Villa> GetVilla(int id)
        {
            return await Villa.AsNoTracking().FirstAsync(x => x.Id == id);
        }

        internal async Task<Villa> CreateVillaAsync(VillaCreateDto villaDto)
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

        internal async Task<VillaDto> UpdateVilla(VillaUpdateDto villaDto)
        {
            Villa villa = new()
            {
                Id = villaDto.Id,
                Name = villaDto.Name,
                Details = villaDto.Details,
                ImageUrl = villaDto.ImageUrl,
                Tenants = villaDto.Tenants,
                SizeMeters = villaDto.SizeMeters,
                Fee = villaDto.Fee,
                Creation = villaDto.Creation,
                Update = villaDto.Update,

            };
            Villa.Update(villa);
            await SaveChangesAsync();
            return villa.ToDTO();
        }

        internal async Task<bool> DeleteVilla(int id)
        {
            Villa entity = await GetVilla(id);
            Villa.Remove(entity);
            await SaveChangesAsync();
            return true;
        }

        internal bool ExistsByName(VillaCreateDto villa)
        {
            return Villa.Any(v => v.Name.ToLower() == villa.Name.ToLower());
        }

    }
}