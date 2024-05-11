namespace Core.Primitives;

public class DomainException : Exception
{
  public string Code { get; set; }

  public int Status { get; set; }

  public DomainException (string message, string code, int status) : base(message)
  {
    Code = code;

    Status = status;
  }
}