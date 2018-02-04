namespace Zopa.Business.Calculation
{
    /// <summary>
    /// Formula : Finance Charge = Loan Amount x Rate x Total Number Of Years
    /// </summary>
    internal class GetMonthlyPayment<T> where T : Model.MonthlyPayment
    {
        private const int NumberOfMonthlyInOneYear = 12;

        internal decimal Calculate(T monthlypayments)
        {
            if (Helper.Validate.ValidateObject(monthlypayments).Count > 0)
            {
                return default(decimal);
            }

            return ((monthlypayments.LoanAmount + (monthlypayments.TotalInterest - monthlypayments.LoanAmount)) /
                    (monthlypayments.NumberOfYears * NumberOfMonthlyInOneYear));
        }
    }
}