using Core.Entities.Exceptions;
using Core.Primitives;
using System.Text.RegularExpressions;

namespace Core.Entities;

public class Email : ValueObject<string>
{
  public Email (string value) : base(value)
  {
  }

  protected override void Validate ()
  {
    string pattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

    if (new Regex(pattern).IsMatch(Value)) return;

    throw new InvalidEmailException();
  }
}
