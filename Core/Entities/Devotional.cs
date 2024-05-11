using Core.Primitives;

namespace Core.Entities;

public class Devotional : Entity
{
  public string Title { get; set; } = String.Empty;
  
  public string Description { get; set; } = String.Empty;

  public string Image { get; set; } = String.Empty;

  public DateTime CreatedAt { get; set; } = DateTime.Now;

  public Devotional Entitled (string title)
  {
    Title = title;

    return this;
  }

  public Devotional WithDescription (string description) 
  {
    Description = description;

    return this;
  }

  public Devotional WithImage (string image)
  {
    Image = image;

    return this;
  }

  public Devotional WithCreationTime (DateTime creationTime)
  {
    CreatedAt = creationTime;

    return this;
  }
}
