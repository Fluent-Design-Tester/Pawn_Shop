using Pawn_Shop.Utilities.IUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawn_Shop.Utilities
{
    class GoldCalculator : IGoldCalculator
    {
        public decimal CalculateCurrentMarketValue(decimal currentMarketValuePerKyat, string strKyat, string strPae, string strYwae)
        {
            decimal kyat = 0, pae = 0, ywae = 0;

            if (!"".Equals(strKyat)) kyat = Convert.ToDecimal(strKyat) * currentMarketValuePerKyat;
            if (!"".Equals(strPae)) pae = (currentMarketValuePerKyat / 16) * Convert.ToDecimal(strPae);
            if (!"".Equals(strYwae)) ywae = (currentMarketValuePerKyat / 128) * Convert.ToDecimal(strYwae);

            return kyat + pae + ywae;
        }
    }
}
