using Core.Primitives;

namespace Core.Entities.Exceptions;

public class InvalidPhoneException : DomainException
{
  public InvalidPhoneException (string message = "Invalid Phone", string code = "INVALID_PHONE", int status = 400) : base(message, code, status)
  {
  }
}
