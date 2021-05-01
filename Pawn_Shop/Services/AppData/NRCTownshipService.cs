using Pawn_Shop.Dto;
using Pawn_Shop.IServices.AppData;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Pawn_Shop.Services.AppData
{
    class NRCTownshipService : Service, INRCTownshipService
    {
        private const string URI = "/api/nrc-townships";

        public NRCTownshipService() : base(URI)
        {
        }

        public async Task<ObservableCollection<T>> GetByRegionId<T>(ObservableCollection<T> list, string regionId)
        {
            return await GetAll<T>(list, $"?regionId={regionId}");
        }

        public async Task<bool> Save(NRCTownship newNRCTownship)
        {
            return await Save<NRCTownship>(newNRCTownship);
        }

        public async Task<bool> Update(NRCTownship updatedNRCTownship)
        {
            return await Update<NRCTownship>(updatedNRCTownship);
        }

        public async Task<bool> Delete(int nrcTownshipId)
        {
            return await DeleteById(nrcTownshipId);
        }
    }
}
