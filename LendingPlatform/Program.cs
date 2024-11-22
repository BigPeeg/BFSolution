using LendingPlatform;
using LendingPlatform.Analytics;
using LendingPlatform.ValidationHandlers;

AnalyticsStore analyticsStore = new();

Console.WriteLine("Lending Plaform");
bool @continue;
do
{
    Console.WriteLine();
    var (loanValue, assetValue, creditScore) = GetUserInput();

    ApplicationResult result = ValidateApplication(loanValue, assetValue, creditScore);

    Console.WriteLine($"Application was {result}");

    StoreAnalytics(result, loanValue, assetValue);
    PublishMetrics();

    Console.WriteLine("Process another application (y/n)");
    ConsoleKeyInfo input = Console.ReadKey();
    @continue = input.KeyChar == 'y' || input.KeyChar == 'Y';
} while (@continue);



void StoreAnalytics(ApplicationResult result, int loanValue, int assetValue)
{
    analyticsStore.Add(result, loanValue, assetValue);
}

void PublishMetrics()
{
    Console.WriteLine($"Total applications = {analyticsStore.TotalApplicationsCount} of which {analyticsStore.SuccessfulApplicationsCount} where successful");
    Console.WriteLine($"Total value of loans written to date £{analyticsStore.TotalLoanValue}");
    Console.WriteLine($"The mean average Loan to Value of all applications received to date {analyticsStore.GetMeanLTV()}");
}

ApplicationResult ValidateApplication(int loanValue, int assetValue, int creditScore)
{
    var decisionEngine = new DecisionEngine();
    decimal ltv = LTVUtilities.LoanToValue(loanValue, assetValue);
    return decisionEngine.Handle(loanValue, ltv, creditScore);
}

static (int loanValue, int assetValue, int creditScore) GetUserInput()
{
    int loanValue, assetValue, creditScore;
    Console.WriteLine("Enter load Value (GBP)");
    bool validInput;
    do
    {
        validInput = Int32.TryParse(Console.ReadLine(), out loanValue);
        if (!validInput)
            Console.WriteLine("Invalid input, value must be GBP");
    } while (!validInput);

    Console.WriteLine("Enter Asset Value (GBP)");
    do
    {
        validInput = Int32.TryParse(Console.ReadLine(), out assetValue);
        if (!validInput)
            Console.WriteLine("Invalid input, value must be GBP");
    } while (!validInput);
    Console.WriteLine("Enter Credit Score(1-999)");
    do
    {
        validInput = Int32.TryParse(Console.ReadLine(), out creditScore) && creditScore > 1 && creditScore < 999;
        if (!validInput)
            Console.WriteLine("Invalid input, value must be between 1 and 999");
    } while (!validInput);

    return (loanValue, assetValue, creditScore);
}