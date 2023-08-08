namespace LoanManager.Application.Common.Interfaces.Authentication;

public class JwtSettings
{
    public const string FieldName = "JWTSettings";

    public string Secret { get; init; } = null!;
    public int ExpireTime { get; init; }
    public string Issuer { get; init; } = null!;
    public string Audience { get; init; } = null!;
}