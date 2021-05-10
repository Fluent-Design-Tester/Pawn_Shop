using Pawn_Shop.Dto.ShopPrice.UpdateShopPrice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawn_Shop.Dto.AcceptGold
{
    class LatestShopPrices
    {
        public RealSalePrice htdRealSalePriceResponse { get; set; }
        public PawningPrice htdPawnPriceResponse { get; set; }
        public PurchasingPrice htdPurchasePriceResponse { get; set; }
    }
}
