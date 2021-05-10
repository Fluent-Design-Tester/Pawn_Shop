using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawn_Shop.Dto.ShopPrice.UpdateShopPrice
{
    class PurchasingPrice
    {
        public int? id { get; set; }
        public string purchasePriceType { get; set; }
        public decimal purchaseValue { get; set; }
        public decimal purchaseTypeSPrice { get; set; }
        public decimal purcahseTypeAPrice { get; set; }
        public decimal purchaseTypeBPrice { get; set; }
        public decimal purcahseTypeCPrice { get; set; }
    }
}
