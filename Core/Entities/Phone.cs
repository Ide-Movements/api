using Core.Entities.Exceptions;
using Core.Primitives;
using PhoneNumbers;
using System.Text.RegularExpressions;

namespace Core.Entities;

public class Phone : ValueObject<string>
{
  public Phone (string value) : base(value)
  {
  }

  public string GetValue()
  {
    if (Value == null) throw new InvalidPhoneException();

    return Regex.Replace(Value, @"[^\d]", "");
  }

  protected override void Validate ()
  {
    string withoutAnyMask = $"+{Regex.Replace(Value, @"[^\d]", "")}";

    var phoneNumberUtil = PhoneNumberUtil.GetInstance();

    try
    {
      var numberProto = phoneNumberUtil.Parse(withoutAnyMask, "+55");
      var countryCode = phoneNumberUtil.GetRegionCodeForNumber(numberProto);
      var numberProtoWithCountryCode = phoneNumberUtil.Parse(withoutAnyMask, countryCode);
      var isValid = phoneNumberUtil.IsValidNumber(numberProtoWithCountryCode);

      if (isValid) return;

      throw new InvalidPhoneException();
    }
    catch (Exception e)
    {
      Console.WriteLine(e);
    }
  }
}
