using System;
using System.Globalization;
using Unity;
using Zopa.Business;
using Zopa.Business.Interfaces;
using Zopa.Business.Repository;

namespace Zopa
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
            CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.DefaultThreadCurrentCulture;
            
            try
            {
                using (var container = DependencyContainer())
                {
                    var execute = container.Resolve<ZopaApplication>();
                    execute.LoanProgram(args);
                }
            }
            catch(Exception ex)
            {
                Log.Error(ex.Message);
                Console.WriteLine("Error: Incorrect number of parameters provided");
                Console.WriteLine("File Format: Market File should be a valid CSV file");
                Console.WriteLine("Suggestion: cmd > [application][market_file][loan_amount]");                
            }
        }

        private static UnityContainer DependencyContainer()
        {
            using (var container = new UnityContainer())
            {
                container.RegisterType<ICalculator, Calculator>();
                container.RegisterType<IRepository, Repository>();
                container.RegisterType<IValidateRequest, ValidateRequest>();
                container.RegisterType<ILoanCalculation, LoanCalculation>();
                return container;
            }
        }
    }
}