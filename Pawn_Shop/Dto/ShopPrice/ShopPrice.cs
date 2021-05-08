using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawn_Shop.Dto.ShopPrice
{
    // Used in title bar in UpdateShopPrice Page
    class ShopPrice
    {
        public int id { get; set; }
        public int displayNo { get; set; }
        public decimal ygnGoldPrice { get; set; }
        public decimal worldGoldPrice { get; set; }
        public decimal usDollars { get; set; }
        public decimal? differenceGoldPrice { get; set; }
        public DateTime updatedDate { get; set; }
    }
}
