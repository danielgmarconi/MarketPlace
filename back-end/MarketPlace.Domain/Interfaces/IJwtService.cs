namespace MarketPlace.Domain.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(int userid, string email);
    }
}
