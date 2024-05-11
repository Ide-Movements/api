namespace Core.Primitives;

public abstract class ValueObject<T>
{
  public T? Value { get; set; }

  protected abstract void Validate ();

  public ValueObject (T value)
  {
    Value = value;

    Validate();
  }
}
