namespace Refactoring.FraudDetection.Models
{
    public interface IOrderParser
    {
        Order Parse(string s);
    }
}
