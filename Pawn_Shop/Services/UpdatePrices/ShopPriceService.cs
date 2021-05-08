using Pawn_Shop.Dto.ShopPrice.UpdateShopPrice;
using Pawn_Shop.IServices.UpdatePrices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawn_Shop.Services.UpdatePrices
{
    class ShopPriceService : Service, IShopPriceService
    {
        private const string URI = "/api/gold_prices";

        public ShopPriceService() : base(URI)
        {
        }

        public async Task<ShopPrice> GetLatestGoldPrice<ShopPrice>()
        {
            return await GetOne<ShopPrice>("/latest");
        }
    }
}
