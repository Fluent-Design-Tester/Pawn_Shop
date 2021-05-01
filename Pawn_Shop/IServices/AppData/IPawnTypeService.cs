using Pawn_Shop.Dto;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Pawn_Shop.IServices.AppData
{
    interface IPawnTypeService
    {
        Task<ObservableCollection<T>> GetByCategoryId<T>(ObservableCollection<T> list, string categoryId);

        Task<bool> Save(PawnType newPawnType);

        Task<bool> Update(PawnType updatedPawnType);

        Task<bool> Delete(int id);
    }
}
