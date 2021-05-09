using System.Collections.Generic;

namespace Pawn_Shop.Utilities.IUtilities
{
    internal interface IGoldCalculator
    {
        decimal CalculateCurrentMarketValue(decimal currentMarketValuePerKyat, string strKyat, string strPae, string strYwae);

        Dictionary<string, double> ConvertFromGramToKPY(double gram); 
    }
}