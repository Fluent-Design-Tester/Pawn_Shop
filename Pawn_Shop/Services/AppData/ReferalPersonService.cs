using Pawn_Shop.Dto;
using Pawn_Shop.IServices.AppData;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawn_Shop.Services.AppData
{
    class ReferalPersonService : Service, IReferalPersonService
    {
        public ReferalPersonService(string uri) : base(uri)
        {
        }

        public async Task<ObservableCollection<T>> GetAllReferalPersons<T>(ObservableCollection<T> list)
        {
            return await GetAll<T>(list);
        }

        public async Task<bool> Save(ReferalPerson newReferalPerson)
        {
            return await Save<ReferalPerson>(newReferalPerson);
        }

        public async Task<bool> Update(ReferalPerson updatedReferalPerson)
        {
            return await Update<ReferalPerson>(updatedReferalPerson);
        }

        public async Task<bool> Delete(int id)
        {
            return await DeleteById(id);
        }
    }
}
