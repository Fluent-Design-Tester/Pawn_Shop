using Pawn_Shop.Dto;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawn_Shop.Services.UpdatePrices
{
    class GoldPriceService : Service
    {
        public GoldPriceService(string uri) : base(uri)
        {
        }

        public async Task<ObservableCollection<T>> FilterByDateRange<T>(ObservableCollection<T> list, string fromDate, string toDate)
        {
            return await GetAll<T>(list, $"?from={fromDate}&to={toDate}");
        }

        public async Task<bool> Save(GoldPrice newGoldPrice)
        {
            return await Save<GoldPrice>(newGoldPrice);
        }
    }
}
