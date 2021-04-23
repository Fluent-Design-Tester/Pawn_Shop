using Pawn_Shop.Dto;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawn_Shop.Services.AppData
{
    class ReferalPersonService : Service
    {
        public ReferalPersonService(string uri) : base(uri)
        {
        }

        public async Task<ObservableCollection<T>> GetAllReferalPersons<T>(ObservableCollection<T> list)
        {
            return await GetAll<T>(list);
        }
    }
}
