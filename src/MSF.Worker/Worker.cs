using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

// Worker Service Project
namespace MSF.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private HttpClient client;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            client = new HttpClient();
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            client.Dispose();
            _logger.LogInformation("------------Service has been stopped-----------");
            return base.StopAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                #region Comentarios

                // _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                // Console.WriteLine($"{result.StatusCode} - {result.Headers.Date} - {result.RequestMessage.RequestUri}");

                #endregion

                var result = await client.GetAsync("https://www.google.com");

                if (result.IsSuccessStatusCode)
                {
                    _logger.LogInformation($"Website is up. Status Code: {result.StatusCode}");
                }
                else
                {
                    _logger.LogError($"Website is down. Status Code: {result.StatusCode}");
                }

                await Task.Delay(3000, stoppingToken);
            }
        }

        // Publish de Worker
        // Open PowerShell as Adm
        // sc.exe create <ServiceName> binpath=<service.exe_path> start= auto
        // sc.exe create MSFService1 binpath= C:\Users\Falt_\Documentos\github\Publish\Worker\MSF.Worker.exe start= auto
        // sc.exe delete MSFService1

    }
}
