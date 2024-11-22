namespace LendingPlatform.ValidationHandlers
{
    public abstract class ValidationHandler
    {
        protected ValidationHandler? _nextHandler;
        public void SetNext(ValidationHandler nextHandler)
        {
            _nextHandler = nextHandler;
        }
        public abstract ApplicationResult Handle(int loanValue, decimal ltv, int creditScore);
    }
}
