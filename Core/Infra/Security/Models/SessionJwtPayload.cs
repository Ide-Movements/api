using Core.Primitives;

namespace Core.Infra.Security.Models;

public class SessionJwtPayload : Data
{
  public required string Id { get; set; }

  public string? Email { get; set; }

  public string? Name { get; set; }

  public string? Password { get; set; }
}
