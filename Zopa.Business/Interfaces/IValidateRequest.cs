namespace Zopa.Business.Interfaces
{
    public interface IValidateRequest
    {
        string IsValid(string value, out int loadamount);
    }
}