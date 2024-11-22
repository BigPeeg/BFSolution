namespace LendingPlatform.ValidationHandlers
{
    public class SmallLoanMaxLTVExceeded : ValidationHandler
    {
        private readonly int _loanSizeThreshold;
        private readonly decimal _ltvThreshold;

        public SmallLoanMaxLTVExceeded(int loanSizeThreshold, decimal ltvThreshold)
        {
            _loanSizeThreshold = loanSizeThreshold;
            _ltvThreshold = ltvThreshold;
        }
        public override ApplicationResult Handle(int loanValue, decimal ltv, int creditScore)
        {
            if (ValidRule(loanValue) && ltv > _ltvThreshold)
            {
                return ApplicationResult.Failure;
            }
            else
            {
                return _nextHandler?.Handle(loanValue, ltv, creditScore) ?? ApplicationResult.Failure;
            }
        }

        private bool ValidRule(int loanValue)
        {
            return loanValue < _loanSizeThreshold;
        }
    }
}
