namespace Core.Primitives;

public abstract class Command<IPayload>
{
  public IPayload? Payload { get; set; }

  public Command (IPayload payload)
  {
    Payload = payload;

    ValidatePayload();
  }

  public abstract void ValidatePayload ();
}
