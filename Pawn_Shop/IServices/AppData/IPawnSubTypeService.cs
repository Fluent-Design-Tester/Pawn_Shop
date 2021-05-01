using Pawn_Shop.Dto;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawn_Shop.IServices.AppData
{
    interface IPawnSubTypeService
    {
        Task<ObservableCollection<T>> GetByTypeId<T>(ObservableCollection<T> list, string typeId);

        Task<bool> Save(PawnSubType newPawnSubType);

        Task<bool> Update(PawnSubType updatedPawnSubType);

        Task<bool> Delete(int id);
    }
}
