using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Steeltoe.Common.Hosting;
using Steeltoe.Discovery.Client;
using Steeltoe.Discovery.Kubernetes;
using Steeltoe.Extensions.Configuration.CloudFoundry;
using Steeltoe.Management.Endpoint;

namespace Fortune_Teller_UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.AddCloudFoundryConfiguration();
                    
                })
                .AddCloudFoundryConfiguration()
                .AddServiceDiscovery(builder => builder.UseKubernetes())
                .AddHealthActuator()
                .AddHypermediaActuator()
                .UseCloudHosting(5555);
    }
}
