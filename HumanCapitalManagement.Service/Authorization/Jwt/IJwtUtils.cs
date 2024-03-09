namespace HumanCapitalManagement.Service.Authorization.Jwt;

public interface IJwtUtils
{
    Task<int?> ValidateTokenProduction(string token);
    int? ValidateToken(string token);
}