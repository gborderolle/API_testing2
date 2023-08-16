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

        internal async Task<List<Villa>> GetVillas()
        {
            return await _contextDB.GetVillas();
        }

        internal async Task<Villa> GetVilla(int id)
        {
            return await _contextDB.GetVilla(id);
        }

        internal async Task<Villa> GetUpdateVilla(int id)
        {
            return await _contextDB.GetVilla(id);
        }

        internal async Task<Villa> CreateVilla(Villa villa)
        {
            return await _contextDB.CreateVillaAsync(villa);
        }

        internal async Task<Villa> UpdateVilla(Villa villa)
        {
            return await _contextDB.UpdateVilla(villa);
        }

        internal async Task<bool> DeleteVilla(int id)
        {
            return await _contextDB.DeleteVilla(id);
        }

        internal async Task<bool> ExistsByName(VillaCreateDto villa)
        {
            return await _contextDB.ExistsByName(villa);
        }

    }
}
