namespace LendingPlatform.ValidationHandlers
{
    static public class LTVUtilities
    {
        public static decimal LoanToValue(int loanValue, int assetValue)
        {
            return Decimal.Divide(loanValue, assetValue) * 100;
        }
    }
}
