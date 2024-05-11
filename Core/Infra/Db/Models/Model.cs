using Core.Primitives;
using System.ComponentModel.DataAnnotations;

namespace Core.Infra.Db.Models;

public abstract class Model<IEntity> where IEntity : Entity
{
  [Key]
  public Guid Id { get; set; }

  public abstract IEntity ToEntity ();

  public abstract Model<IEntity> FromEntity (IEntity e);
}
