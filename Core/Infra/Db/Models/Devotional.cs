using Core.Entities;

namespace Core.Infra.Db.Models;

public class DevotionalModel : Model<Devotional>
{
  public string Title { get; set; }

  public string Description { get; set; }

  public string Image { get; set; }

  public DateTime CreatedAt { get; set; }

  public override DevotionalModel FromEntity (Devotional e)
  {
    DevotionalModel model = new DevotionalModel();

    model.Title = e.Title;
    model.Description = e.Description;
    model.Image = e.Image;
    model.CreatedAt = e.CreatedAt; 
    model.Id = e.Id;

    return model;
  }

  public override Devotional ToEntity ()
  {
    var devotional = new Devotional()
      .Entitled(Title)
      .WithDescription(Description)
      .WithImage(Image)
      .WithCreationTime(CreatedAt);

    devotional.Id = Id;

    return devotional;
  }
}
