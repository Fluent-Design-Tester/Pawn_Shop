using Pawn_Shop.Dto;
using Pawn_Shop.IServices.UpdatePrices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawn_Shop.Services.UpdatePrices
{
    class GoldPriceService : Service, IGoldPriceService
    {
        public GoldPriceService(string uri) : base(uri)
        {
        }

        public async Task<ObservableCollection<T>> GetByDate<T>(ObservableCollection<T> list, string date)
        {
            return await GetAll<T>(list, $"?date={date}");
        }

        public async Task<ObservableCollection<T>> GetByDateRange<T>(ObservableCollection<T> list, string fromDate, string toDate)
        {
            return await GetAll<T>(list, $"?from={fromDate}&to={toDate}");
        }

        public async Task<bool> Save(GoldPrice newGoldPrice)
        {
            return await Save<GoldPrice>(newGoldPrice);
        }
    }
}
