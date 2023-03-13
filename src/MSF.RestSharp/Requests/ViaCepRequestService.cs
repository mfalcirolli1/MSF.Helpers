using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MSF.RestSharp.Requests
{
    internal class ViaCepRequestService
    {
        public RestResponse CallViaCepRequest()
        {
            Console.WriteLine("Digite um CEP: ");
            var cep = Console.ReadLine();

            while(cep.Length != 8)
            {
                Console.WriteLine("Erro. Digite um CEP válido");
                Console.WriteLine("Digite o CEP novamente: ");
                cep = Console.ReadLine();
            }

            var client = new RestClient($"https://viacep.com.br/ws/{cep}/json/");
            var request = new RestRequest("", Method.Get);
            var response = client.Execute(request);

            return response;
        }

        public async Task<TReturn> PostAsync<T, TReturn>(T model, string pathUrl = "")
        {
            var httpClient = new HttpClient();

            var stringPayload = JsonConvert.SerializeObject(model);
            var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("baseURL/" + pathUrl, content);

            // SetStatus(response)

            TReturn returnType = default;

            if (response.Content != null)
            {
                try
                {
                    returnType = JsonConvert.DeserializeObject<TReturn>(await response.Content.ReadAsStringAsync());
                }
                catch (Exception)
                {
                }
            }

            // ReturnType = returnType
            return returnType;
        }
    }
}
