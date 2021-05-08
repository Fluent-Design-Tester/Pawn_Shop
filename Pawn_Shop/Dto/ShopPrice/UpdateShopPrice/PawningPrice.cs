using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawn_Shop.Dto.ShopPrice.UpdateShopPrice
{
    class PawningPrice
    {
        public string pawnPriceType { get; set; }
        public decimal pawnValue { get; set; }
        public decimal pawnTypeSPrice { get; set; }
        public decimal pawnTypeAPrice { get; set; }
        public decimal pawnTypeBPrice { get; set; }
        public decimal pawnTypeCPrice { get; set; }
    }
}
