using Pawn_Shop.Dto;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Pawn_Shop.IServices.UpdatePrices
{
    interface IGoldPriceService
    {
        Task<ObservableCollection<GoldPrice>> GetByDate<GoldPrice>(ObservableCollection<GoldPrice> list, string date);

        Task<ObservableCollection<GoldPrice>> GetByDateRange<GoldPrice>(ObservableCollection<GoldPrice> list, string fromDate, string toDate);

        Task<bool> Save(GoldPrice newGoldPrice);
    }
}
