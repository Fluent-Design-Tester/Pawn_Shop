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
        public decimal? usDollars { get; set; }
        public decimal differenceGoldPrice { get; set; }
        public DateTime updatedDate { get; set; }

        /*
         "[{\"id\":9,\"displayNo\":1,\"ygnGoldPrice\":1111111.00,\"worldGoldePrice\":33333.00,\"usDollars\":1640.00,\"differenceGoldPrice\":12000.00,\"updatedDate\":\"2021-04-23T14:59:11Z\"},{\"id\":10,\"displayNo\":2,\"ygnGoldPrice\":12345.00,\"worldGoldePrice\":1222.00,\"usDollars\":null,\"differenceGoldPrice\":1222.00,\"updatedDate\":\"2021-04-23T15:01:23Z\"}]"
         */
    }
}
