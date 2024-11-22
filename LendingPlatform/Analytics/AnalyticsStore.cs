using LendingPlatform.ValidationHandlers;

namespace LendingPlatform.Analytics
{
    public class AnalyticsStore
    {
        private int _successfulApplicationsCount;
        private int _totalApplicationCount;
        private int _totalLoanValue;
        private decimal _lvtTally;

        public void Add(ApplicationResult result, int loanValue, int assetValue)
        {
            ProcessApplicationCount(result);
            ProcessLoanValue(result, loanValue);
            ProcessLTV(loanValue, assetValue);
        }

        public decimal GetMeanLTV()
        {
            //The mean average Loan to Value of all applications received to date
            return _lvtTally / _totalApplicationCount;
        }

        public int SuccessfulApplicationsCount => _successfulApplicationsCount;

        public int TotalApplicationsCount => _totalApplicationCount;

        public int TotalLoanValue => _totalLoanValue;   //The total value of successful loans received to date

        private void ProcessApplicationCount(ApplicationResult result)
        {
            _totalApplicationCount++;
            if (result == ApplicationResult.Success)
                _successfulApplicationsCount++;
        }

        private void ProcessLoanValue(ApplicationResult result, int loanValue)
        {
            // Assuming only successful applications increment the total
            if (result == ApplicationResult.Success)
                _totalLoanValue += loanValue;
        }

        private void ProcessLTV(int loanValue, int assetValue)
        {
            _lvtTally += LTVUtilities.LoanToValue(loanValue, assetValue);
        }
    }
}
