using Core.Primitives;

namespace Core.Entities.Exceptions;

public class InvalidEmailException : DomainException
{
  public InvalidEmailException (string message = "Invalid Email", string code = "INVALID_EMAIL", int status = 400) : base(message, code, status)
  {
  }
}
