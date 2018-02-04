using System;

namespace Zopa.Business
{
    public class Calculator : Interfaces.ICalculator
    {
        public decimal PerformCalculation<T>(T instance) where T : class
        {
            Object objectInstance = instance;
            try
            {
                if (instance.GetType().Name == nameof(Model.FinanceCharge))
                {
                    var fc = new Calculation.GetFinanceCharge<Model.FinanceCharge>();
                    return fc.Calculate((Model.FinanceCharge)objectInstance);
                }

                if (instance.GetType().Name == nameof(Model.MonthlyPayment))
                {
                    var mp = new Calculation.GetMonthlyPayment<Model.MonthlyPayment>();
                    return mp.Calculate((Model.MonthlyPayment)objectInstance);
                }

                if (instance.GetType().Name == nameof(Model.Rates))
                {
                    var fc = new Calculation.GetRates<Model.Rates>();
                    return fc.Calculate((Model.Rates)objectInstance);
                }
            }
            catch
            {
                throw new ArgumentNullException();
            }
            return default(decimal);
        }
    }
}