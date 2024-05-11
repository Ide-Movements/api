using Core.Infra.Db;
using Core.Infra.Db.Models;
using Core.Primitives;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Infra.Repository;

public abstract class Repository<IEntity, IModel> : IRepository<IEntity> where IEntity : Entity
  where IModel : Model<IEntity>, new ()
{
  protected readonly IdeCoreDbContext _dbContext;

  protected readonly DbSet<IModel> _dbSet;

  private readonly IMediator _mediator;

  public Repository(IdeCoreDbContext dbContext, DbSet<IModel> dbSet, IMediator mediator)
  {
    _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    _dbSet = dbSet;
    _mediator = mediator;
  }

  public Task<IEntity?> GetById (Guid id)
  {
    var e = _dbSet.AsNoTracking().Where(e => e.Id == id).ToList().FirstOrDefault();
    return Task.FromResult(e?.ToEntity());
  }

  public async Task<List<IEntity?>> GetAll ()
  {
    var entities = await _dbSet.Where(e => e != null).AsNoTracking().ToListAsync();
    return entities.Select(e => e?.ToEntity()).ToList();
  }

  public async Task Save (IEntity entity)
  {
    var model = FromEntity(entity);
    _dbSet.Add(model);
    await _dbContext.SaveChangesAsync();
  }

  public async Task Update (IEntity entity)
  {
    var model = FromEntity(entity);
    _dbSet.Update(model);
    await _dbContext.SaveChangesAsync();
  }

  public async Task Delete (IEntity entity)
  {
    var model = FromEntity(entity);
    _dbSet.Remove(model);
    await _dbContext.SaveChangesAsync();
  }

  public async Task UpdateMany (IEnumerable<IEntity> entities)
  {
    var models = entities.Select(entity => FromEntity(entity));
    _dbSet.UpdateRange(models);
    await _dbContext.SaveChangesAsync();
  }

  public async Task DeleteMany (IEnumerable<IEntity> entities)
  {
    var models = entities.Select(entity => FromEntity(entity));
    _dbSet.RemoveRange(models);
    await _dbContext.SaveChangesAsync();
  }

  public abstract IModel FromEntity (IEntity entity);

}
