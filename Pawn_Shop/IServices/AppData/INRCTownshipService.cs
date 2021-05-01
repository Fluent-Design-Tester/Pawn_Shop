using Pawn_Shop.Dto;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawn_Shop.IServices.AppData
{
    interface INRCTownshipService
    {
        Task<ObservableCollection<T>> GetByRegionId<T>(ObservableCollection<T> list, string regionId);

        Task<bool> Save(NRCTownship newNRCTownship);

        Task<bool> Update(NRCTownship updatedNRCTownship);

        Task<bool> Delete(int nrcTownshipId);
    }
}
