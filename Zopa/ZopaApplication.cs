using System;
using System.Collections.Generic;
using Zopa.Business.Interfaces;
using Zopa.Business.Model;

namespace Zopa
{
    public class ZopaApplication
    {
        private readonly IValidateRequest _validateRequest;
        private readonly ILoanCalculation _loanCalculation;
        private readonly IRepository _repository;

        public ZopaApplication(IValidateRequest validateRequest,
            ILoanCalculation loanCalculation,
            IRepository repository)
        {
            _validateRequest = validateRequest;
            _loanCalculation = loanCalculation;
            _repository = repository;
        }

        public void LoanProgram(IList<string> values)
        {
            //Validate Request
            if (values[1] == null)
            {
                throw new ArgumentNullException();
            }

            if (values[0] == null)
            {
                throw new ArgumentNullException();
            }
            
            string csvFile = values[0];
            string strloanAmount = values[1];
            string result = this._validateRequest.IsValid(strloanAmount,
                out var loanAmount);

            if (!result.Equals("TRUE"))
            {
                Log.Fatel(result);
                Console.WriteLine($"Message: {result}");
                Console.WriteLine($"Loan Amount: {strloanAmount}");
                return;
            }

            //Calculation & Output
            PrintResult(this._loanCalculation.Calculation(loanAmount,
                this._repository.GetAllInvestment(csvFile)));
        }

        private void PrintResult(CalculatedResult result)
        {
            if (result != null)
            {
                Console.WriteLine($"Requested Amount: {result.RequestedAmount:c0}");
                Console.WriteLine($"Rate: {result.Rate:P1}");
                Console.WriteLine($"Monthly Repayment: {result.MonthlyRepayments:c2}");
                Console.WriteLine($"Total Repayment: {result.TotalRepayments:c2}");
            }
        }
    }
}