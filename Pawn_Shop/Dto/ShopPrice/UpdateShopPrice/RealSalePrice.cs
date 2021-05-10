using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawn_Shop.Dto.ShopPrice.UpdateShopPrice
{
    class RealSalePrice
    {
        public int? id { get; set; }
        public decimal rsPrice { get; set; }
        public decimal rsTypeAPrice { get; set; }
        public decimal rsTypeBPrice { get; set; }
        public decimal rsTypeCPrice { get; set; }
    }
}
