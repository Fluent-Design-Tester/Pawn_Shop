using Newtonsoft.Json;
using Pawn_Shop.Dto;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.Web.Http;

namespace Pawn_Shop.Services.AppData
{
    class NRCTownshipService : Service
    {

        public NRCTownshipService(string uri) : base(uri)
        {
        }

        public async Task<ObservableCollection<T>> GetByRegionId<T>(ObservableCollection<T> list, string regionId)
        {
            return await GetById<T>(list, "?regionId=", regionId);
        }

        public async Task<bool> Save(NRCTownship newNrcTownship)
        {
            return await Save<NRCTownship>(newNrcTownship);
        }

        public async Task<bool> Update(NRCTownship updatedNrcTownship)
        {
            return await Update<NRCTownship>(updatedNrcTownship);
        }

        public async Task<bool> Delete(int nrcTownshipId)
        {
            return await DeleteById(nrcTownshipId);
        }
    }
}
