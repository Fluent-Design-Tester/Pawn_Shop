using Newtonsoft.Json;
using Pawn_Shop.Dto;
using System;
using System.Collections.ObjectModel;
using Windows.Storage.Streams;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace Pawn_Shop.Services.AppData
{

    class PawnTypeService : Service
    {
        public PawnTypeService(string uri) : base(uri)
        {
        }

        public async Task<ObservableCollection<T>> GetByCategoryId<T>(ObservableCollection<T> list, string categoryId)
        {
            return await GetById<T>(list, "?categoryId=", categoryId);
        }

        public async Task<bool> Save(PawnType newPawnType)
        {
            return await Save<PawnType>(newPawnType);
        }

        public async Task<bool> Update(PawnType updatedPawnType)
        {
            return await Update<PawnType>(updatedPawnType);
        }

        public async Task<bool> Delete(int id)
        {
            return await DeleteById(id);
        }
    }
}
