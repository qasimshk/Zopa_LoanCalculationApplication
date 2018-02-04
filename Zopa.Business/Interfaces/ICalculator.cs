namespace Zopa.Business.Interfaces
{
    public interface ICalculator
    {
        decimal PerformCalculation<T>(T ints) where T : class;
    }
}