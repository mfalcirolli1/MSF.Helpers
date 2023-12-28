using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace MSF.Util.WireMock
{
    public class WireMockDemo
    {
        public static string APICall()
        {
            var log = new StringBuilder();

            WireMockServer server = WireMockServer.Start();

            log.AppendLine($"Início o processamento da API Mockada\n");
            log.AppendLine($"URL: {server.Url}\n");

            server.Given(Request.Create()
                .WithPath("/home/texttest")
                .UsingGet())
                .RespondWith(Response.Create()
                .WithStatusCode(200)
                .WithHeader("Content-Type", "text/plain")
                .WithBody("Body da chamada"));

            server.Given(Request.Create()
                .WithPath("/home/jsontest")
                .UsingGet())
                .RespondWith(Response.Create()
                .WithStatusCode(200)
                .WithHeader("Content-Type", "application/json")
                .WithBodyAsJson(new WireMockModel { Id = 0, Name = "Matheus" }));

            server.Given(Request.Create()
                .WithPath("/home/posttest")
                .UsingPost())
                .RespondWith(Response.Create()
                .WithStatusCode(200)
                .WithHeader("Content-Type", "application/json")
                .WithBodyAsJson(new WireMockModel { Id = 0, Name = "Post Test", PhoneNumber = "(11) 99999-9999",
                    Address = new Address() { CEP = "00000-00", City = "Guarulhos", Country = "Brazil", Number = "110", Reference = "Não há", State = "SP", Street = "Rua rua" } }));

            Debug.WriteLine(log.ToString());

            Thread.Sleep(300000);

            server.Stop();

            return log.ToString();
        }
    }
}
