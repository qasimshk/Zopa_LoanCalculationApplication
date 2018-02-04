using System.ComponentModel.DataAnnotations;

namespace Zopa.Business.Model
{
    public class MonthlyPayment : Abstracts.BaseValues
    {
        [Range(typeof(decimal), "1", "79228162514264337593543950335")]
        public decimal TotalInterest { get; set; }
    }
}