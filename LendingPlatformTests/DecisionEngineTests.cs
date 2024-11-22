using LendingPlatform;

namespace LendingPlatformTests
{
    public class DecisionEngineTests
    {
        private readonly DecisionEngine decisionEngine = new();

        [TestCase(1_000_000, 60, 950)]
        [TestCase(999_999, 59, 750)]
        [TestCase(999_999, 59, 800)]
        [TestCase(999_999, 59, 900)]
        [TestCase(999_999, 79, 800)]
        [TestCase(999_999, 89, 900)]
        public void ValidApplication(int loanValue, decimal ltv, int creditScore)
        {
            var result = decisionEngine.Handle(loanValue, ltv, creditScore);
            Assert.That(result, Is.EqualTo(ApplicationResult.Success), $"Loan Value={loanValue}, LTV={ltv}, CreditScore={creditScore}");
        }

        [TestCase(1_500_001)]
        [TestCase(99_999)]
        public void LoanValueOutORange(int loanValue)
        {
            var ltv = 90;
            var creditScore = 999;
            var result = decisionEngine.Handle(loanValue, ltv, creditScore);
            Assert.That(result, Is.EqualTo(ApplicationResult.Failure), "Application must fail if loan out of range");
        }

        [TestCase(60, 949)]
        [TestCase(61, 950)]
        public void InsufficientLoanToValueOrCreditForLargeLoan(decimal ltv, int creditScore)
        {
            var loanValue = 1_000_000;
            var result = decisionEngine.Handle(loanValue, ltv, creditScore);
            Assert.That(result, Is.EqualTo(ApplicationResult.Failure), "If the value of the loan is £1 million or more then the LTV must be 60% or less and the credit score of the applicant must be 950 or more");
        }

        [TestCase(90)]
        [TestCase(91)]
        public void LoanToValueTooHighForUnderOneMillionLoan(decimal ltv)
        {
            var loanValue = 999_999;
            var creditScore = 999;
            var result = decisionEngine.Handle(loanValue, ltv, creditScore);
            Assert.That(result, Is.EqualTo(ApplicationResult.Failure), "If the value of the loan is less than £1 million and the LTV is 90% or more, the application must be declined");
        }

        [TestCase(59, 749)]
        [TestCase(79, 799)]
        [TestCase(89, 899)]
        public void InsufficientCreditScoreForLoanToValueForUnderOneMillionLoan(decimal ltv, int creditScore)
        {
            var loanValue = 999_999;
            var result = decisionEngine.Handle(loanValue, ltv, creditScore);
            Assert.That(result, Is.EqualTo(ApplicationResult.Failure), "If the value of the loan is less than £1 million then rules apply on required credit score");
        }
    }
}