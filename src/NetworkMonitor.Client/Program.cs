using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetworkMonitor.Client.Models;
using NetworkMonitor.Client.Repositories;

namespace NetworkMonitor
{
  public class Program
  {
    public static void Main(string[] args)
    {
      CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
              services.AddHttpClient();
              services.AddHostedService<Worker>();

              var connString = new ConnectionString(hostContext.Configuration.GetConnectionString("DefaultConnection"));
              services.AddSingleton(connString);
              services.AddSingleton<INetMonitorRepo, NetMonitorRepo>();
            });
  }
}
