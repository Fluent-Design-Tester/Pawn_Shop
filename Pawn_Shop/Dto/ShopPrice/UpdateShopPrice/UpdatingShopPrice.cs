using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawn_Shop.Dto.ShopPrice.UpdateShopPrice
{
    class UpdatingShopPrice
    {
        public decimal extraPrice { get; set; }
        public decimal htdGoldPrice { get; set; }
        public decimal htdTypeAPrice { get; set; }
        public decimal htdTypeBPrice { get; set; }
        public decimal htdTypeCPrice { get; set; }
        public RealSalePrice htdRealSalePriceRequest { get; set; }
        public PurchasingPrice htdPurchasePriceRequest { get; set; }
        public PawningPrice htdPawnPriceRequest { get; set; }
        public DownPurchasingPrice downPurchasePriceRequest { get; set; }
        public DownSalePrice downSalePriceRequest { get; set; }
    }
}
