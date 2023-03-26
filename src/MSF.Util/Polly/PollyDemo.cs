using Newtonsoft.Json;
using Polly;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace MSF.Util.Polly
{
    // A .NET resilience and transient-fault-handling library
    // Retry, circuit breaker, timeout

    public static class PollyDemo
    {
        public static void PollyTest()
        {
            var policy = Policy
                .Handle<AggregateException>()
                .Or<HttpRequestException>()
                .Retry(3, (exception, retryCount) =>
                {
                    Console.WriteLine($"Exception: {exception.Message}. Retry attempt {retryCount}");
                });

            var client = new HttpClient();

            policy.Execute(() =>
            {
                var cep = "";
                var response = client.GetStringAsync($"https://viacep.com.br/ws/{cep}/json/").Result;
                var endereco = JsonConvert.DeserializeObject<CepModel>(response);

                //if (!response.IsCompletedSuccessfully)
                //{
                //    throw new HttpRequestException();
                //}
            });
        }
    }

    internal class CepModel
    {
        [JsonProperty("cep")]
        public string Cep { get; set; }

        [JsonProperty("logradouro")]
        public string Logradouro { get; set; }

        [JsonProperty("complemento")]
        public string Complemento { get; set; }

        [JsonProperty("bairro")]
        public string Bairro { get; set; }

        [JsonProperty("localidade")]
        public string Localidade { get; set; }

        [JsonProperty("uf")]
        public string UF { get; set; }
    }
}
