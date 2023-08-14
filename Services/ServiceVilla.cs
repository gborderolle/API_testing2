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

        internal async Task<VillaDto> CreateVilla(VillaDto villa)
        {
            Villa temp = await _contextDB.CreateVillaAsync(villa);
            return temp.ToDTO();
        }

        internal async Task<VillaDto> UpdateVilla(VillaDto villa)
        {
            return await _contextDB.UpdateVilla(villa);
        }

        internal async Task<bool> DeleteVilla(int id)
        {
            return await _contextDB.DeleteVilla(id);
        }

        internal bool Exists(VillaDto villa)
        {
            return _contextDB.Exists(villa);
        }

        internal async Task<VillaDto> UpdatePartialVilla(JsonPatchDocument patchDto)
        {
            return await _contextDB.UpdatePartialVilla(patchDto);
        }
    }
}
