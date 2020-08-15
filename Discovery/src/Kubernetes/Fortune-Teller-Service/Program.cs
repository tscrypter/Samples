using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Steeltoe.Common.Hosting;
using Steeltoe.Discovery.Client;
using Steeltoe.Discovery.Kubernetes;
using Steeltoe.Extensions.Configuration.CloudFoundry;
using Steeltoe.Extensions.Configuration.Placeholder;
using Steeltoe.Management.Endpoint;

namespace FortuneTellerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildHost(args).Run();
        }

        public static IHost BuildHost(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(host => host.UseStartup<Startup>())
                .AddCloudFoundryConfiguration()
                .AddHealthActuator()
                .AddHypermediaActuator()
                .AddPlaceholderResolver()
                .AddServiceDiscovery(options => options.UseKubernetes())
                .UseCloudHosting(5000)
                .Build();

    }
     
}
