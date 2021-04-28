using Pawn_Shop.Dto;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawn_Shop.IServices.AppData
{
    interface IReferalPersonService
    {
        Task<ObservableCollection<ReferalPerson>> GetAllReferalPersons<ReferalPerson>(ObservableCollection<ReferalPerson> list);

        Task<bool> Save(ReferalPerson newReferalPerson);

        Task<bool> Update(ReferalPerson updatedReferalPerson);

        Task<bool> Delete(int id);
    }
}
