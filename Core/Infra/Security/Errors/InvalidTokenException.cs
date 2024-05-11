using Core.Primitives;

namespace Core.Infra.Security.Errors;

public class InvalidTokenException : DomainException
{
  public InvalidTokenException (string message = "Invalid Token", string code = "INVALID_TOKEN", int status = 403) : base(message, code, status)
  {
  }
}
