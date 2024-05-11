using Core;

public partial class Program
{
  public static IHostBuilder CreateHostBuilder (string[] args)
  {
    using ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
    factory.CreateLogger<Program>();

    return Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
          webBuilder.UseStartup<Startup>()
                  .UseKestrel(opt =>
                {
                  opt.Limits.MaxRequestBodySize = null;
                });
        });
  }

  public static void Main (string[] args)
  {
    CreateHostBuilder(args).Build().Run();
  }
}