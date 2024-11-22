namespace LendingPlatform.ValidationHandlers
{
    public class LargeLoanLTVRatioAssessment : ValidationHandler
    {
        private readonly int _loanSizeThreshold;
        private readonly decimal _requiredLTV;
        private readonly int _requiredCreditScore;

        public LargeLoanLTVRatioAssessment(int loanSizeThreshold, decimal requiredLTV, int requiredCreditScore)
        {
            _loanSizeThreshold = loanSizeThreshold;
            _requiredLTV = requiredLTV;
            _requiredCreditScore = requiredCreditScore;
        }

        public override ApplicationResult Handle(int loanValue, decimal ltv, int creditScore)
        {
            if (ValidRule(loanValue))
            {
                if (creditScore >= _requiredCreditScore && ltv <= _requiredLTV)
                    return ApplicationResult.Success;

                return ApplicationResult.Failure;
            }
            else
            {
                return _nextHandler?.Handle(loanValue, ltv, creditScore) ?? ApplicationResult.Failure;
            }
        }

        private bool ValidRule(int loanValue)
        {
            return loanValue >= _loanSizeThreshold;

        }
    }
}
