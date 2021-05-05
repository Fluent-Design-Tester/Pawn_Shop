using Pawn_Shop.Utilities.IUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawn_Shop.Utilities
{
    class ShopPriceCalculator : IShopPriceCalculator
    {
        public decimal CalculateAPrice(decimal SPrice) => Math.Round((SPrice / 17) * 16);

        public decimal CalculateBPrice(decimal SPrice) => Math.Round((decimal)((double)SPrice / 17.5) * 16);

        public decimal CalculateCPrice(decimal SPrice) => Math.Round((SPrice / 18) * 16);

        public decimal CalculateSPrice(decimal ygnGoldPrice, decimal extraCost) => Math.Round(ygnGoldPrice + extraCost);
    }
}
