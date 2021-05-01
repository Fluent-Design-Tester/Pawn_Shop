using Pawn_Shop.Dto;
using Pawn_Shop.IServices.AppData;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Pawn_Shop.Services.AppData
{

    class PawnTypeService : Service, IPawnTypeService
    {
        private const string URI = "/api/types";

        public PawnTypeService() : base(URI)
        {
        }

        public async Task<ObservableCollection<T>> GetByCategoryId<T>(ObservableCollection<T> list, string categoryId)
        {
            return await GetAll<T>(list, $"?categoryId={categoryId}");
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
