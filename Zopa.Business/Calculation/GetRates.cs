namespace Zopa.Business.Calculation
{
    /// <summary>
    /// Formula : Interest = Principal.Rate.Time
    /// We will use this formula to calculate Rate
    /// </summary>

    internal class GetRates<T> where T : Model.Rates
    {
        internal decimal Calculate(T rates)
        {
            if (Helper.Validate.ValidateObject(rates).Count > 0)
            {
                return default(decimal);
            }
            return ((rates.TotalInterest - rates.LoanAmount) / rates.LoanAmount);
        }
    }
}