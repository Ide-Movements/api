using Core.Commands.Devotional;
using Core.Infra.Db;
using Core.Infra.Repository;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.FileProviders;
using System.Reflection;

namespace Core;

public class Startup
{
  public IConfiguration Configuration { get; set; }

  public IWebHostEnvironment Environment { get; set; }

  public Startup (IConfiguration configuration, IWebHostEnvironment environment)
  {
    Configuration = configuration;
    Environment = environment;
  } 

  public void ConfigureServices (IServiceCollection services)
  {
    services.AddDbContext<IdeCoreDbContext>();

    services.AddControllers();
    services.AddSwaggerGen();
    services.AddEndpointsApiExplorer();

    services.AddTransient<DevotionalRepository>();

    services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    services.AddTransient<IPipelineBehavior<CreateDevotionalCommand, Unit>, DevotionalRealmHandler>();

    services.AddCors(
      options =>
      {
        options.AddDefaultPolicy(
          policy =>
          {
            policy
              .AllowAnyHeader()
              .AllowAnyOrigin()
              .AllowAnyMethod();
          }
        );
      }
    );
  }

  public void Configure (IApplicationBuilder app, IWebHostEnvironment env)
  {
    if (env.IsDevelopment())
    {
      app.UseDeveloperExceptionPage();
    }

    if (!Environment.IsEnvironment("Tests"))
    {
      var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
      var dbContext = scope.ServiceProvider.GetRequiredService<IdeCoreDbContext>();
    }

    app.UseCors();
    app.UseMiddleware<AuthorizationMiddleware>();
    app.UseSwagger();
    app.UseSwaggerUI(config =>
    {
      config.SwaggerEndpoint("/swagger/v1/swagger.json", "Authentication API");
      config.RoutePrefix = "docs";
      config.InjectStylesheet("/wwwroot/swagger-ui/SwaggerDark.css");
    });

    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseEndpoints(endpoints =>
    {
      endpoints.MapControllers();
    });

    app.UseHttpsRedirection();

    if (!env.IsEnvironment("Tests"))
    {
      app.UseStaticFiles(new StaticFileOptions
      {
        FileProvider = new PhysicalFileProvider(
            Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")),
        RequestPath = "/wwwroot"
      });
    }
  }
}
