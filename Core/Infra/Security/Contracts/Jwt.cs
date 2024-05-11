using Core.Entities;
using Core.Infra.Security.Models;

namespace Core.Infra.Security.Contracts;

public interface IJwt
{
  Task<string> GenerateToken (User user);

  bool Verify (string token);

  SessionJwtPayload Decode (string token);
}
