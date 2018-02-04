using System.ComponentModel.DataAnnotations;

namespace Zopa.Business.Abstracts
{
    public abstract class BaseValues
    {
        [Range(1, int.MaxValue)]
        public int NumberOfYears { get; set; }

        [Range(1, int.MaxValue)]
        public int LoanAmount { get; set; }
    }
}