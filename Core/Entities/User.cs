using Core.Primitives;

namespace Core.Entities;

public class User : Entity
{
  public string Name { get; set; } = string.Empty;

  public Email? Email { get; set; }

  public Phone? Phone { get; set; }

  public string Picture { get; set; } = string.Empty;

  public string Password {  get; set; } = string.Empty;

  public User Named (string name)
  {
    Name = name;

    return this;
  }

  public User WithEmail (Email email) 
  {
    Email = email;

    return this;
  }

  public User WithPhone (Phone phone) 
  {
    Phone = phone;

    return this;
  }

  public User WithPicture (string picture) 
  {
    Picture = picture;

    return this;
  }

  public User WithPassword (string password)
  {
    Password = password;

    return this;
  }
}
