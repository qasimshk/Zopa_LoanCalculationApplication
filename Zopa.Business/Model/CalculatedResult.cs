namespace Zopa.Business.Model
{
    public class CalculatedResult
    {
        public int RequestedAmount { get; set; }
        public decimal Rate { get; set; }
        public decimal MonthlyRepayments { get; set; }
        public decimal TotalRepayments { get; set; }
    }
}