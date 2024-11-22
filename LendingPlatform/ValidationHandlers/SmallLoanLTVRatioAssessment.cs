namespace LendingPlatform.ValidationHandlers
{
    public class SmallLoanLTVRatioAssessment : ValidationHandler
    {
        private readonly int _loanSizeThreshold;
        private readonly decimal _requiredLTV;
        private readonly int _requiredCreditScore;

        public SmallLoanLTVRatioAssessment(int loanSizeThreshold, decimal requiredLTV, int requiredCreditScore)
        {
            _loanSizeThreshold = loanSizeThreshold;
            _requiredLTV = requiredLTV;
            _requiredCreditScore = requiredCreditScore;
        }

        public override ApplicationResult Handle(int loanValue, decimal ltv, int creditScore)
        {
            if (ValidRule(loanValue))
            {
                if (ltv < _requiredLTV && creditScore >= _requiredCreditScore)
                    return ApplicationResult.Success;

                return _nextHandler?.Handle(loanValue, ltv, creditScore) ?? ApplicationResult.Failure;
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
