namespace Pawn_Shop.Utilities.IUtilities
{
    internal interface IGoldCalculator
    {
        decimal CalculateCurrentMarketValue(decimal currentMarketValuePerKyat, string strKyat, string strPae, string strYwae);
    }
}