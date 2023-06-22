using HtmlAgilityPack;
using MSF.Util.AggressiveInlining;
using MSF.Util.AppConfig;
using MSF.Util.Asynchronous;
using MSF.Util.Base64;
using MSF.Util.Binary;
using MSF.Util.Bogus;
using MSF.Util.Crawler;
using MSF.Util.Dapper;
using MSF.Util.Delegates;
using MSF.Util.Encrypt;
using MSF.Util.FluentValidation;
using MSF.Util.JWT;
using MSF.Util.LazyLoad;
using MSF.Util.Security;
using MSF.Util.StreamDemo;
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
            Debug.WriteLine($"Válido: {result.IsValid}\n Mensagem: {result.Errors.FirstOrDefault()}");

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
    }
}