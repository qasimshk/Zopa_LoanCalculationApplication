namespace Zopa.Business
{
    public class ValidateRequest : Interfaces.IValidateRequest
    {
        private const int MaximumLoanAmount = 15000;
        private const int MinimumLoanAmount = 1000;
        private const int IncreasingLoadAmountValue = 100;

        public string IsValid(string value, out int loadamount)
        {
            //Check if the value is a proper interger value
            loadamount = default(int);
            if (!int.TryParse(value, out loadamount))
            {
                return "Please provide a valid amount";
            }

            //Check if the value is divisible by 100
            if (loadamount % IncreasingLoadAmountValue != 0)
            {
                return ($"Please provide an amount which is an increment of {IncreasingLoadAmountValue:c2}");
            }

            //Check if the value is with the given limits
            if (loadamount > MaximumLoanAmount || loadamount < MinimumLoanAmount)
            {
                return ($"Please provide a valid amount between {MinimumLoanAmount:c2} & {MaximumLoanAmount:c2}");
            }
            return "TRUE";
        }
    }
}