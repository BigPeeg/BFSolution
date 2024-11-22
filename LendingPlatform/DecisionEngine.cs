using LendingPlatform.ValidationHandlers;

namespace LendingPlatform
{
    public class DecisionEngine
    {
        private const int LargeLoanThresholdValue = 1_000_000;
        private readonly ValidationHandler _validationHandler;

        public DecisionEngine()
        {
            //If the value of the loan is more than £1.5 million or less than £100,000 then the application must be declined
            var loanValueAssessment = new LoanValueAssessment(100_000, 1_500_000);
            // If the value of the loan is £1 million or more then the LTV must be 60 % or less and the credit score of the applicant must be 950 or more
            var largeLoanLTVRatioAssessment = new LargeLoanLTVRatioAssessment(LargeLoanThresholdValue, 60, 950);

            //If the value of the loan is less than LargeLoanThresholdValue then the following rules apply:
            //If the LTV is less than 60%, the credit score of the applicant must be 750 or more
            var ltvUnder60 = new SmallLoanLTVRatioAssessment(LargeLoanThresholdValue, 60, 750);
            //If the LTV is less than 80%, the credit score of the applicant must be 800 or more
            var ltvUnder80 = new SmallLoanLTVRatioAssessment(LargeLoanThresholdValue, 80, 800);
            //If the LTV is less than 90%, the credit score of the applicant must be 900 or more
            var ltvUnder90 = new SmallLoanLTVRatioAssessment(LargeLoanThresholdValue, 90, 900);
            //If the LTV is 90% or more, the application must be declined
            var ltvMaxExceeded = new SmallLoanMaxLTVExceeded(LargeLoanThresholdValue, 90);

            loanValueAssessment.SetNext(largeLoanLTVRatioAssessment);
            largeLoanLTVRatioAssessment.SetNext(ltvUnder60);
            ltvUnder60.SetNext(ltvUnder80);
            ltvUnder80.SetNext(ltvUnder90);
            ltvUnder90.SetNext(ltvMaxExceeded);

            _validationHandler = loanValueAssessment;
        }

        public ApplicationResult Handle(int loanValue, decimal ltv, int creditScore)
        {
            return _validationHandler.Handle(loanValue, ltv, creditScore);
        }
    }
}
