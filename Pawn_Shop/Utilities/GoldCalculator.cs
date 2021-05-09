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

        public Dictionary<string, double> ConvertFromGramToKPY(double gram)
        {
            var weightInKPY = new Dictionary<string, double>();

            double kyat = Math.Truncate(gram / 16.6);

            double _pae = ((gram / 16.6) - kyat) * 16;
            double pae = Math.Truncate(_pae);

            double ywae = Math.Round((_pae - pae) * 8, 1);

            weightInKPY["kyat"] = kyat;
            weightInKPY["pae"] = pae;
            weightInKPY["ywae"] = ywae;

            return weightInKPY;
        }
    }
}
