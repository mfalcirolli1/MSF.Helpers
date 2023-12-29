using HtmlAgilityPack;
using Microsoft.Extensions.Caching.Memory;
using MSF.Util.AggressiveInlining;
using MSF.Util.AppConfig;
using MSF.Util.Asynchronous;
using MSF.Util.Base64;
using MSF.Util.Binary;
using MSF.Util.Bogus;
using MSF.Util.Cache;
using MSF.Util.Crawler;
using MSF.Util.Dapper;
using MSF.Util.Delegates;
using MSF.Util.DesignPatterns.Singleton;
using MSF.Util.Encrypt;
using MSF.Util.FluentValidation;
using MSF.Util.JWT;
using MSF.Util.LazyLoad;
using MSF.Util.Security;
using MSF.Util.SOLID.DIP___Dependency_Inversion_Principle;
using MSF.Util.StreamDemo;
using MSF.Util.Tuples;
using MSF.Util.WireMock;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace MSF.UnitTests
{
    public class UnitTests : WebHelper
    {
        // [Fact] attribute is used by xUnit in .NET which identifies the method for unit

        [Fact(DisplayName = "AES - Advanced Encryptino Standard")]
        public void EncyptDecrypt()
        {
            // Arrange
            byte[] chave = new byte[16];
            byte[] vetorInicializacao = new byte[16];

            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(chave);
                rng.GetBytes(vetorInicializacao);
            }

            // Act
            var senhaCriptografada = AES.Encrypt("Matheus", chave, vetorInicializacao);
            var senhaDescriptografada = AES.Decrypt(senhaCriptografada, chave, vetorInicializacao);

            Debug.WriteLine($"Criptografada: {Convert.ToBase64String(senhaCriptografada)} -> {Encoding.UTF8.GetString(senhaCriptografada)}");
            Debug.WriteLine($"Descriptografada: {senhaDescriptografada}");

            // Assert
            Assert.NotNull(senhaCriptografada);
            Assert.NotNull(senhaDescriptografada);
        }



        [Fact(DisplayName = "App Config Helper - Get & Update")]
        public void AppConfig()
        {
            // Arrange

            // Act
            var valor1 = AppConfigHelper.GetChaveAppConfig("chave2");
            AppConfigHelper.UpdateChaveAppConfig("chave2", "novo valor");
            var valor2 = AppConfigHelper.GetChaveAppConfig("chave2");


            // Assert
            Assert.NotEqual(valor1, valor2);
        }



        [Fact(DisplayName = "Lazy Loader")]
        public void Lazy()
        {
            // Arrange
            var lazy = new LazyDemo();

            // Act
            lazy.Loader(10, false);

            // Assert
            Assert.True(true);
        }



        [Fact(DisplayName = "JWT - Json Web Token")]
        public void CriarJWT()
        {
            // Arrange
            // In the arrange section we setup and declare some inputs and configuration variable

            // Act
            var token = JsonWebToken.CriarJWT();

            // Assert
            Assert.NotNull(token);
        }



        [Fact(DisplayName = "Crawler - Raspagem")]
        public void Raspar()
        {
            BeginStep("https://https://www.google.com.br", "GET");
            var paginaHtml = GetResponseContent();
            _cookies = _cookieContainer.GetCookies(_request.RequestUri);
            //var parameters = GetAll

            var docHtml = new HtmlDocument();
            docHtml.LoadHtml(paginaHtml);

            EndStep();
        }



        [Fact(DisplayName = "Bogus - Mock Data")]
        public void Bogus()
        {
            var customers = BogusDemo.GenerateCustomer();

            if(!customers.Any())
                Assert.Empty(customers);

            foreach (var customer in customers)
            {
                var props = customer.GetType().GetProperties();

                foreach (var property in props)
                {
                    Debug.WriteLine(property.GetValue(customer));
                }
            }
            Assert.NotNull(customers);
        }



        [Theory(DisplayName = "FluentValidation - Validação")]
        [InlineData(0)]
        [InlineData(1)]
        public void ValidarObjeto(int id)
        {
            // Arrange
            var customer = new CustomerModel
            {
                ID = id,
                Name = "Opa",
                Address = "Ruaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
            };

            var validator = new CustomerValidator();

            // Act
            var result = validator.Validate(customer);

            if (result.Errors.Any())
            {
                Debug.WriteLine($"Válido: {result.IsValid}\nMensagem: {result.Errors.FirstOrDefault()} = {customer.ID}");
                return;
            }

            Debug.WriteLine($"Objeto válido: {result.IsValid}");


            // Assert
            Assert.NotNull(result);
        }

        [Fact(DisplayName = "Dapper")]
        public void Dapper()
        {
            var dapper = new DapperDemo();

            dapper.Parameters();

            Assert.True(true);
        }

        [Fact(DisplayName = "Asynchronous")]
        public void Asynchronous()
        {
            var async = new AsyncDemo();

            //var result1 = async.ReadFileAsync(@"C:\Users\Falt_\Documentos\Teste.txt");
            //var result1 = async.WriteToFileAsync(@"C:\Users\Falt_\Documentos\Teste.txt", "Olar");
            var teste = async.Teste();
            Debug.WriteLine("Ainda não terminei de ler pow");

            //Assert.NotNull(result1);
        }

        [Fact(DisplayName = "Delegate")]
        public void Delegate()
        {
            var dl = new DelegateDemo();

            dl.GeneralMethod(DelegateDemo.ReferencedMethod);

            Assert.True(true);
        }

        [Fact(DisplayName = "Base64")]
        public void Base64()
        {
            var texto = "Qualquer coisa";

            var textoBase64 = texto.TransformTextToBase64();
            var textoString = textoBase64.TransformBase64ToText();

            Debug.WriteLine(textoBase64);
            Debug.WriteLine(textoString);
        }

        [Fact(DisplayName = "Aggressive Inlining")]
        public void AggressiveInlining()
        {
            var list = new List<int>()
            {
                1, 2, 3, 4, 5, 6, 7, 8, 9, 10
            };

            foreach (var item in list)
            {
                var agg = Aggressive.Add(item, item + 1);
                Debug.WriteLine(agg);
            }
        }

        [Fact(DisplayName = "Security Interview Questions")]
        public void SecurityInterview()
        {
            Security.HashCryptography();
            Security.SecureStringDemo();
        }

        [Fact(DisplayName = "Binário | Hexadecimal | Octal")]
        public void BinárioHexadecimalOctal()
        {
            var input = "100";

            Debug.WriteLine($"Texto original: {input}");

            Debug.WriteLine("-----Binário-----");
            Binary.Binario(input);
            Debug.WriteLine("-----Octal-----");
            Binary.Octal(input);
            Debug.WriteLine("-----Hexadecimal-----");
            Binary.Hexadecimal(input);
        }

        [Fact(DisplayName = "Stream")]
        public void Stream()
        {
            FileStreamDemo.WriteFile();
            FileStreamDemo.ReadFile();
        }

        [Fact(DisplayName = "Tuples")]
        public void Tuples()
        {
            TuplesDemo.DoSomething();
        }

        [Fact(DisplayName = "Singleton")]
        public void Singleton()
        {
            var obj = new SingletonDemo();
            var sgt = obj.SingleInstance;

            Assert.NotNull(sgt);
        }

        [Fact(DisplayName = "Memory Cache Demo")]
        public void CacheDemo()
        {
            // Arrange
            var options = new MemoryCacheOptions();
            IMemoryCache _memorycache = new MemoryCache(options);

            var memoryCacheDemo = new MemoryCacheDemo(_memorycache);

            // Act
            memoryCacheDemo.GetClientes();

            // Assert
            Assert.Null(null);
        }

        [Fact(DisplayName = "Wire Mock - Mocking")]
        public void WireMockTest()
        {
            // Arrange


            // Act
            var response = WireMockDemo.APICall();

            // Assert
            Assert.NotNull(response);
        }

        [Fact(DisplayName = "SOLID - DIP (Dependency Inversion Principle)")]
        public void SolidDIP()
        {
            // Arrange
            var execute = new Chore();

            // Act
            execute.Execute();

            // Assert
            Assert.Null(null);
        }
    }
}