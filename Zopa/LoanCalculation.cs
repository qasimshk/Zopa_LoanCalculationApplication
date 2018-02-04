using System;
using System.Collections.Generic;
using System.Linq;
using Zopa.Business.Entity;
using Zopa.Business.Interfaces;
using Zopa.Business.Model;

namespace Zopa
{
    public class LoanCalculation : ILoanCalculation
    {
        private readonly ICalculator _calculator;
        private decimal Annualpercentagerate { get; set; }
        private decimal FinanceCharge { get; set; }
        private decimal MonthlyPayments { get; set; }
        private decimal TotalPayments { get; set; }
        private int NumberOfYears = 3;

        public LoanCalculation(ICalculator calculator)
        {
            _calculator = calculator;
        }

        public CalculatedResult Calculation(int loanAmount, IEnumerable<FileData> investments)
        {
            //Total Payments
            this.TotalPayments = CalculateTotalPayment(investments, loanAmount);

            if (this.TotalPayments > 0)
            {
                //Interest
                //Formula: I = P . R . T
                var rate = new Rates
                {
                    LoanAmount = loanAmount,
                    NumberOfYears = NumberOfYears,
                    TotalInterest = this.TotalPayments
                };
                this.Annualpercentagerate = this._calculator.PerformCalculation(rate);

                //Finanace Charges
                //Formula: FC = P x R x T
                var financecharges = new FinanceCharge
                {
                    Rate = this.Annualpercentagerate,
                    LoanAmount = loanAmount,
                    NumberOfYears = NumberOfYears
                };
                this.FinanceCharge = this._calculator.PerformCalculation(financecharges);

                ////Monthly Payments
                ////Formula: (P + FC) / T
                var monthlypayments = new MonthlyPayment
                {
                    LoanAmount = loanAmount,
                    NumberOfYears = NumberOfYears,
                    TotalInterest = this.TotalPayments
                };
                this.MonthlyPayments = this._calculator.PerformCalculation(monthlypayments);

                return new CalculatedResult
                {
                    RequestedAmount = loanAmount,
                    Rate = this.Annualpercentagerate,
                    MonthlyRepayments = this.MonthlyPayments,
                    TotalRepayments = this.TotalPayments
                };
            }
            return default;
        }

        private decimal CalculateTotalPayment(IEnumerable<FileData> investments, int LoanAmount)
        {
            decimal totalpayments = default;
            decimal creditedamount = default;
            decimal currentamount = default;

            if (investments.Sum(x => x.Available) < LoanAmount)
            {                
                Log.Fatel($"Failed to provide loan due to total investment available {investments.Sum(x => x.Available):c2}");
                Console.WriteLine("Message: Sorry!we won't be able to provide loan at the moment.");
            }
            else
            {
                foreach (var investment in investments)
                {
                    if (LoanAmount < (creditedamount + investment.Available))
                        currentamount = (LoanAmount - creditedamount);
                    else
                        currentamount = investment.Available;

                    totalpayments += currentamount + (currentamount * investment.Rate);

                    if ((creditedamount += currentamount) >= LoanAmount)
                    {
                        break;
                    }
                }
            }
            return totalpayments;
        }
    }
}