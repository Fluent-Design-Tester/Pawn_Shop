using System;

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
    }
}
