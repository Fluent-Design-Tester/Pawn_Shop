using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawn_Shop.Dto
{
    class GoldPrice
    {
        public int id { get; set; }
        public int displayNo { get; set; }
        public decimal ygnGoldPrice { get; set; }
        public decimal worldGoldPrice { get; set; }
        public decimal dollarPrice { get; set; }
        public decimal differenceGoldPrice { get; set; }
    }
}
