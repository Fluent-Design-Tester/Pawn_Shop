using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawn_Shop.Dto.ShopPrice.UpdateShopPrice
{
    class DownSalePrice
    {
        public string downSalePriceType { get; set; }
        public decimal downSaleValue { get; set; }
        public decimal downSaleTypeSPrice { get; set; }
        public decimal downSaleTypeAPrice { get; set; }
        public decimal downSaleTypeBPrice { get; set; }
        public decimal downSaleTypeCPrice { get; set; }
    }
}
