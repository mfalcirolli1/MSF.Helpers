using MSF.RestSharp.Model;
using MSF.RestSharp.Requests;
using Newtonsoft.Json;
using System;

namespace MSF.RestSharp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var viaCepService = new ViaCepRequestService();
            var retorno = viaCepService.CallViaCepRequest();

            Console.WriteLine(retorno);

            var endereco = JsonConvert.DeserializeObject<ViaCepModel>(retorno.Content);

            Console.WriteLine(endereco.Logradouro);
            Console.WriteLine(endereco.UF);
            Console.WriteLine(endereco.Bairro);
            Console.WriteLine(endereco.Localidade);
            Console.WriteLine(endereco.Cep);
            Console.WriteLine(endereco.Complemento);

            Console.ReadLine();
        }
    }
}
