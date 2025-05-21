namespace MarketPlace.Domain.Interfaces
{
    public interface IEncryptionService
    {
        string Encrypt(string value);
        bool Valid(string encryptedValue, string comparedValue);
    }
}
