using Core.Entities;
using Core.Entities.Exceptions;
using Core.Infra.Security.Contracts;
using Core.Infra.Security.Errors;
using Core.Infra.Security.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Core.Infra.Security;

public class Jwt : IJwt
{
  public SessionJwtPayload Decode (string token)
  {
    string? secret = Environment.GetEnvironmentVariable("JWT_SECRET");

    if (string.IsNullOrEmpty(secret))
    {
      throw new InternalServerException("Secret not found");
    }

    var tokenValidationParams = new TokenValidationParameters
    {
      ValidateAudience = false,

      ValidateIssuer = false,

      ValidateIssuerSigningKey = true,

      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),

      ValidateLifetime = false
    };

    var tokenHandler = new JwtSecurityTokenHandler();
    var principal = tokenHandler.ValidateToken(token, tokenValidationParams, out var securityToken);
    if (
      securityToken is not JwtSecurityToken jwtSecurityToken ||

      !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase)
    )
    {
      throw new InvalidTokenException();
    }


    JwtSecurityToken jwtToken = tokenHandler.ReadJwtToken(token);

    var id = jwtToken.Claims.First(c => c.Type == "id").Value;
    var email = jwtToken.Claims.First(c => c.Type == "email").Value;
    var password = jwtToken.Claims.First(c => c.Type == "password").Value;
    var name = jwtToken.Claims.First(c => c.Type == "name").Value;

    return new SessionJwtPayload
    {
      Id = id,

      Email = email,

      Password = password,

      Name = name,
    };
  }

  public async Task<string> GenerateToken (User user)
  {
    string? secret = Environment.GetEnvironmentVariable("JWT_SECRET");

    if (string.IsNullOrEmpty(secret))
    {
      throw new InternalServerException("Secret not found");
    }

    var tokenHandler = new JwtSecurityTokenHandler();
    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
    var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    var claims = new Claim[]
    {
      new Claim("id", user.Id.ToString()),

      new Claim("email", user.Email?.Value ?? "null"),

      new Claim("name", user.Name),

      new Claim("password", user.Password)
    };

    var tokenDescriptor = new SecurityTokenDescriptor
    {
      Subject = new ClaimsIdentity(claims),

      Expires = DateTime.UtcNow.AddYears(1),

      SigningCredentials = credentials
    };

    var token = tokenHandler.CreateToken(tokenDescriptor);
    return tokenHandler.WriteToken(token);
  }

  public bool Verify (string token)
  {
    string? secret = Environment.GetEnvironmentVariable("JWT_SECRET");

    if (string.IsNullOrEmpty(secret))
    {
      throw new InternalServerException("Secret not found");
    }

    var tokenValidationParams = new TokenValidationParameters
    {
      ValidateAudience = false,

      ValidateIssuer = false,

      ValidateIssuerSigningKey = true,

      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),

      ValidateLifetime = false
    };

    var tokenHandler = new JwtSecurityTokenHandler();
    try
    {
      tokenHandler.ValidateToken(token, tokenValidationParams, out var securityToken);

      if (
        securityToken is not JwtSecurityToken jwtSecurityToken ||

        !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase)
      )
      {
        return false;
      }

      return true;
    }
    catch (Exception)
    {
      return false;
    }
  }
}
