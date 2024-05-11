namespace Core.Primitives;

public interface IRepository<IEntity> where IEntity : Entity 
{
  Task<IEntity?> GetById (Guid id);

  Task<List<IEntity?>> GetAll();

  Task Save (IEntity entity);

  Task Update (IEntity entity);

  Task Delete (IEntity entity);

  Task UpdateMany (IEnumerable<IEntity> entities);

  Task DeleteMany (IEnumerable<IEntity> entities);
}
