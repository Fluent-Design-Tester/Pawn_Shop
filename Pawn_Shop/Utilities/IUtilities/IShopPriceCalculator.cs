using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawn_Shop.Utilities.IUtilities
{
    interface IShopPriceCalculator
    {
        decimal CalculateSPrice(decimal ygnGoldPrice, decimal extraCost);
        decimal CalculateAPrice(decimal htdGoldPrice);
        decimal CalculateBPrice(decimal htdGoldPrice);
        decimal CalculateCPrice(decimal htdGoldPrice);
    }
}
