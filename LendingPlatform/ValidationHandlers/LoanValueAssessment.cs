namespace LendingPlatform.ValidationHandlers
{
    public class LoanValueAssessment : ValidationHandler
    {
        private readonly int _lowerLimit;
        private readonly int _upperLimit;

        public LoanValueAssessment(int lowerLimit, int upperLimit)
        {
            _lowerLimit = lowerLimit;
            _upperLimit = upperLimit;
        }

        public override ApplicationResult Handle(int loanValue, decimal ltv, int creditScore)
        {
            return LoanValueOutOfRange(loanValue)
                ? ApplicationResult.Failure
                : _nextHandler?.Handle(loanValue, ltv, creditScore) ?? ApplicationResult.Failure;
        }

        private bool LoanValueOutOfRange(int loanValue)
        {
            return loanValue < _lowerLimit || loanValue > _upperLimit;
        }
    }
}
