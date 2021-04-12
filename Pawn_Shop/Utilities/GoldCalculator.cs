using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawn_Shop.Utilities
{
    class GoldCalculator
    {
        public static double CalculateCurrentMarketValue(double currentMarketValuePerKyat, string strKyat, string strPae, string strYwae)
        {
            double kyat = 0, pae = 0, ywae = 0;

            if (!"".Equals(strKyat)) kyat = Convert.ToDouble(strKyat) * currentMarketValuePerKyat;
            if (!"".Equals(strPae)) pae = (currentMarketValuePerKyat / 16) * Convert.ToDouble(strPae);
            if (!"".Equals(strYwae)) ywae = (currentMarketValuePerKyat / 128) * Convert.ToDouble(strYwae);

            return kyat + pae + ywae;
        }
    }
}
