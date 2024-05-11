using Core.Entities;
using Core.Infra.Db;
using Core.Infra.Db.Models;
using Core.Infra.Repository.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Infra.Repository;

public class DevotionalRepository : Repository<Devotional, DevotionalModel>
{
  public DevotionalRepository (IdeCoreDbContext dbContext, DbSet<DevotionalModel> dbSet, IMediator mediator) : base(dbContext, dbSet, mediator)
  {
  }

  public override DevotionalModel FromEntity (Devotional entity)
  {
    return new DevotionalModel().FromEntity(entity);
  }
}
