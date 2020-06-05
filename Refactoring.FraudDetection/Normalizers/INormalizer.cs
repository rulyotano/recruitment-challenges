namespace Refactoring.FraudDetection.Normalizers
{
    public interface INormalizer
    {
        string Normalize(string textToNormalize);
    }
}
