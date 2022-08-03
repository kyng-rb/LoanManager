using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using LoanManager.Application.Common.Interfaces.Authentication;
using LoanManager.Application.Common.Interfaces.Services;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LoanManager.Infrastructure.Authentication;

public class JWTTokenGenerator : IJWTTokenGenerator
{
    private readonly JWTSettings _settings;
    private readonly IDateTimeProvider _dateTimeProvider;

    public JWTTokenGenerator(IOptions<JWTSettings> settings, IDateTimeProvider dateTimeProvider)
    {
        _settings = settings.Value;
        _dateTimeProvider = dateTimeProvider;
    }

    public string Generate(Guid id, string firstName, string lastName)
    {
        var signinCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_settings.Secret)),
            SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, id.ToString()),
            new Claim(JwtRegisteredClaimNames.GivenName, firstName),
            new Claim(JwtRegisteredClaimNames.FamilyName, lastName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var securityToken = new JwtSecurityToken(
            issuer: _settings.Issuer,
            audience: _settings.Audience,
            expires: _dateTimeProvider.UtcNow.AddDays(_settings.ExpireTime),
            claims: claims,
            signingCredentials: signinCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}