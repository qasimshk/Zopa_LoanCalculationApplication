using System.Collections.Generic;
using Xunit;
using Zopa.Business;
using Zopa.Business.Entity;
using Zopa.Business.Interfaces;
using Zopa.Business.Model;
using Zopa.Business.Repository;
using Moq;
using FluentAssertions;

namespace Zopa.Test
{
    public class UnitTest
    {
        private readonly ICalculator _calculator;
        private readonly IValidateRequest _validateRequest;
        private readonly IRepository _repository;
        private readonly ILoanCalculation _loanCalculation;

        public UnitTest()
        {
            _calculator = new Calculator();
            _validateRequest = new ValidateRequest();
            _repository = new Repository();
            _loanCalculation = new LoanCalculation(new Calculator());
        }
                
        #region Functional Testing

        [Theory]
        [InlineData(0.02334, 1000, 3)]
        [InlineData(int.MaxValue, 100, int.MinValue)]
        [InlineData(0.07004, 1600, 3)]
        [InlineData(0, 0, 0)]
        [InlineData(null, null, null)]
        public void TestFinanaceCharges_WhereReturnValueIsAlwaysAValue(
            decimal rate, int loan, int years)
        {
            var classobj = new FinanceCharge
            {
                Rate = rate,
                LoanAmount = loan,
                NumberOfYears = years
            };
            var result = this._calculator.PerformCalculation(classobj);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(2334, 1000, 3)]
        [InlineData(int.MaxValue, 100, int.MinValue)]
        [InlineData(7004, 1600, 3)]
        [InlineData(0, 0, 0)]
        [InlineData(null, null, null)]
        public void TestAnnualPercentageRate_WhereReturnValueIsAlwaysAValue(
            decimal totalInterest, int loan, int years)
        {
            var classobj = new Rates
            {
                LoanAmount = loan,
                NumberOfYears = years,
                TotalInterest = totalInterest
            };
            var result = this._calculator.PerformCalculation(classobj);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(70.02, 1000, 3)]
        [InlineData(int.MaxValue, 100, int.MinValue)]
        [InlineData(0.07004, 1600, 3)]
        [InlineData(0, 0, 0)]
        [InlineData(null, null, null)]
        public void TestMonthlyPayments_WhereReturnValueIsAlwaysAValue(
            decimal totalInterest, int loan, int years)
        {
            var classobj = new MonthlyPayment
            {
                LoanAmount = loan,
                NumberOfYears = years,
                TotalInterest = totalInterest
            };
            var result = this._calculator.PerformCalculation(classobj);
            Assert.NotNull(result);
        }

        #endregion Functional Testing

        #region Request Validation Testing

        [Theory]
        [InlineData(1100)]
        [InlineData(1400)]
        [InlineData(1200)]
        [InlineData(1500)]
        public void TestValidation_ProvidingValidValues(string value)
        {
            Assert.Same(this._validateRequest.IsValid(value, out _), "TRUE");
        }

        [Theory]
        [InlineData(null)]
        [InlineData(1630)]
        [InlineData(100)]
        [InlineData(1203)]
        [InlineData(int.MaxValue)]
        [InlineData(int.MinValue)]
        public void TestValidation_ProvidingInvalidAndNullValues(string value)
        {
            Assert.NotSame(this._validateRequest.IsValid(value, out _), "TRUE");
        }

        #endregion Request Validation Testing

        #region File Testing 

        [Fact]
        public void TestRepository_ReturnRowsGreaterThenZero()
        {
            Assert.NotEmpty(this._repository.GetAllInvestment("Market.csv"));
        }

        #endregion
        
        #region Console Application 

        [Fact]
        public void Testmock()
        {
            var MockCalculator = new Mock<ICalculator>();
            var loancalculatorTEST = new LoanCalculation(MockCalculator.Object);
            var output = loancalculatorTEST.Calculation(1000, TestFileData());
            loancalculatorTEST.Should().NotBeNull();            
        }


        [Theory]
        [InlineData(1000, 29.8)]
        [InlineData(1300, 38.72)]        
        public void TestLoanCalculator_WithValidAmounts(int loanamount, 
            string expectedmonthlypayment)
        {            
            var data = _loanCalculation.Calculation(loanamount, TestFileData());            
            Assert.Contains(expectedmonthlypayment, data.MonthlyRepayments.ToString("0.##"));
        }

        public static IEnumerable<FileData> TestFileData()
        {
            return new List<FileData> {
                new FileData {
                    Available = 640,
                    Rate = 0.075m,
                    Lender = "Bob"
                },
                new FileData
                {
                    Available = 480,
                    Rate = 0.069m,
                    Lender = "Jane"
                },
                new FileData
                {
                    Available = 520,
                    Rate = 0.071m,
                    Lender = "Fred"
                },
                new FileData
                {
                    Available = 170,
                    Rate = 0.104m,
                    Lender = "Mary"
                },
                new FileData
                {
                    Available = 320,
                    Rate = 0.081m,
                    Lender = "John"
                },
                new FileData
                {
                    Available = 140,
                    Rate = 0.074m,
                    Lender = "Dave"
                },
                new FileData
                {
                    Available = 60,
                    Rate = 0.071m,
                    Lender = "Angela"
                }
            };
        }

        #endregion
    }
}