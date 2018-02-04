using System.Collections.Generic;
using Zopa.Business.Entity;
using Zopa.Business.Model;

namespace Zopa
{
    public interface ILoanCalculation
    {
        CalculatedResult Calculation(int loanAmount, IEnumerable<FileData> investments);
    }
}