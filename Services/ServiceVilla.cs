using API_testing2.Context;
using API_testing2.Models;
using API_testing2.Models.Dto;

namespace API_testing2.Services
{
    public class ServiceVilla
    {
        private readonly ContextDB _contextDB;

        public ServiceVilla(ContextDB contextDB)
        {
            _contextDB = contextDB;
        }

        internal async Task<List<VillaDto>> GetVillas()
        {
            return await _contextDB.GetVillas();
        }

        internal async Task<VillaDto> GetVilla(Guid id)
        {
            Villa villa = await _contextDB.GetVilla(id);
            return villa.ToDTO();
        }

        internal async Task<VillaDto> CreateVilla(VillaDto villa)
        {
            return await _contextDB.CreateVillaAsync(villa).Result.ToDTOAsync();
        }

        internal async Task<bool> UpdateVilla(VillaDto villa)
        {
            return await _contextDB.UpdateVilla(villa);
        }

        internal async Task<bool> DeleteVilla(Guid id)
        {
            return await _contextDB.DeleteVilla(id);
        }

        internal bool Exists(VillaDto villa)
        {
            return _contextDB.Exists(villa);
        }
    }
}
