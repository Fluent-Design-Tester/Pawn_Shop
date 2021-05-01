using Pawn_Shop.Dto;
using Pawn_Shop.IServices.AppData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawn_Shop.Services.AppData
{
    class PawnSubTypeService : Service, IPawnSubTypeService
    {
        private const string URI = "/api/sub-types";

        public PawnSubTypeService() : base(URI)
        {
        }

        public async Task<ObservableCollection<T>> GetByTypeId<T>(ObservableCollection<T> list, string typeId)
        {
            return await GetAll<T>(list, $"?typeId={typeId}");
        }

        public async Task<bool> Save(PawnSubType newPawnSubType)
        {
            return await Save<PawnSubType>(newPawnSubType);
        }

        public async Task<bool> Update(PawnSubType updatedPawnSubType)
        {
            return await Update<PawnSubType>(updatedPawnSubType);
        }

        public async Task<bool> Delete(int id)
        {
            return await DeleteById(id);
        }
    }
}
