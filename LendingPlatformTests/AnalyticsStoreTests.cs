using LendingPlatform;
using LendingPlatform.Analytics;

namespace LendingPlatformTests
{
    public class AnalyticsStoreTests
    {
        private readonly AnalyticsStore _store = new();

        private const int TestLoanValue = 1_000_000;
        private const int TestAssetValue = 2_000_000;


        [OneTimeSetUp]
        public void Setup()
        {
            _store.Add(ApplicationResult.Success, TestLoanValue, TestAssetValue);
            _store.Add(ApplicationResult.Failure, TestLoanValue, TestAssetValue);
            _store.Add(ApplicationResult.Success, TestLoanValue, TestAssetValue);
        }

        [Test]
        public void CheckTotalApplicationCount()
        {
            int expectedTotalApplicationCount = 3;
            Assert.That(_store.TotalApplicationsCount, Is.EqualTo(expectedTotalApplicationCount));
        }

        [Test]
        public void CheckSuccessfulApplicationCount()
        {
            int expectedSuccessfulApplicationCount = 2;
            Assert.That(_store.SuccessfulApplicationsCount, Is.EqualTo(expectedSuccessfulApplicationCount));
        }

        [Test]
        public void CheckTotalValueOfLoans()
        {
            int expectedTotalValueOfLoans = 2_000_000;
            Assert.That(_store.TotalLoanValue, Is.EqualTo(expectedTotalValueOfLoans));
        }

        [Test]
        public void CheckLoanToValueMean()
        {
            decimal expectedLTVMean = 50;
            Assert.That(_store.GetMeanLTV(), Is.EqualTo(expectedLTVMean));
        }

    }
}
