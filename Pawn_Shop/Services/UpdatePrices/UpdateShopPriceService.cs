using Pawn_Shop.Dto.ShopPrice.UpdateShopPrice;
using Pawn_Shop.IServices.UpdatePrices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawn_Shop.Services.UpdatePrices
{

    class UpdateShopPriceService : Service, IUpdateShopPriceService
    {
        private const string URI = "/api/htd_gold_prices";

        public UpdateShopPriceService() : base(URI)
        {
        }

        public async Task<bool> Save(UpdatingShopPrice newShopPrice)
        {
            return await Save<UpdatingShopPrice>(newShopPrice);
        }
    }
}
