using Pawn_Shop.Dto.ShopPrice;
using Pawn_Shop.Dto.ShopPrice.UpdateShopPrice;
using System.Threading.Tasks;

namespace Pawn_Shop.IServices.UpdatePrices
{
    interface IShopPriceService
    {
        Task<ShopPrice> GetLatestGoldPrice<ShopPrice>();
    }
}
