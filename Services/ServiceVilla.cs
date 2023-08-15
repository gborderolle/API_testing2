using API_testing2.Context;
using API_testing2.Models;
using API_testing2.Models.Dto;
using Azure;

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

        internal async Task<VillaDto> GetVilla(int id)
        {
            Villa villa = await _contextDB.GetVilla(id);
            return villa.ToDTO();
        }

        internal async Task<VillaDto> CreateVilla(VillaCreateDto villa)
        {
            Villa temp = await _contextDB.CreateVillaAsync(villa);
            return temp.ToDTO();
        }

        internal async Task<VillaDto> UpdateVilla(VillaUpdateDto villa)
        {
            return await _contextDB.UpdateVilla(villa);
        }

        internal async Task<bool> DeleteVilla(int id)
        {
            return await _contextDB.DeleteVilla(id);
        }

        internal bool ExistsByName(VillaCreateDto villa)
        {
            return _contextDB.ExistsByName(villa);
        }

    }
}
