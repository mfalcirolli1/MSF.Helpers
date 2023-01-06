using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
