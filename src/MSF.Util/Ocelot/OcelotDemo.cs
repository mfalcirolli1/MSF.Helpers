using Microsoft.AspNetCore.Hosting;
using Ocelot.Configuration.File;
using Ocelot.DependencyInjection;
using System.Collections.Generic;

namespace MSF.Util.Ocelot
{
    public  class OcelotDemo
    {
        public void Executar()
        {
            var configuration = new FileConfiguration
            {
                Routes = new List<FileRoute>
                {
                    new FileRoute()
                    {
                        DownstreamPathTemplate = "/api/v1/values",
                        DownstreamScheme = "http",
                        DownstreamHostAndPorts = new List<FileHostAndPort>
                        {
                            new FileHostAndPort
                            {
                                Host = "localhost",
                                Port = 5010
                            }
                        },
                        UpstreamPathTemplate = "/values"
                    }
                }
            };

            var builder = new WebHostBuilder();

            builder.ConfigureServices(s =>
            {
                // s.AddOcelot(configuration);
            });

            builder.UseStartup<StartupBase>();

            var host = builder.Build();
            host.Run();
        }
    }
}
