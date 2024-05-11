using Core.Infra.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace Core.Infra.Db;

public class IdeCoreDbContext : DbContext
{
  public DbSet<DevotionalModel> Devotional { get; set; }

  protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder.UseMySQL(
      Environment.GetEnvironmentVariable("MYSQL_CONNECTION_STRING")
    ).EnableSensitiveDataLogging();
  }
}
