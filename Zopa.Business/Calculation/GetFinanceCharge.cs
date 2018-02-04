namespace Zopa.Business.Calculation
{
    /// <summary>
    /// Formula : Finance Charge = Loan Amount x Rate x Total Number Of Years
    /// </summary>
    internal class GetFinanceCharge<T> where T : Model.FinanceCharge
    {
        internal decimal Calculate(T financecharge)
        {
            if (Helper.Validate.ValidateObject(financecharge).Count > 0)
            {
                return default(decimal);
            }

            return financecharge.LoanAmount *
                   financecharge.Rate *
                   financecharge.NumberOfYears;
        }
    }
}