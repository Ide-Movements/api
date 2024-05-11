using Core.Primitives;

namespace Core.Entities.Exceptions;

public class InternalServerException : DomainException
{
  public InternalServerException (string message = "Internal Server Error", string code = "INTERNAL_SERVER_ERROR", int status = 500) : base(message, code, status)
  {
  }
}
