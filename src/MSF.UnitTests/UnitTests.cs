using MSF.Util.Bogus;
using MSF.Util.Crawler;
using MSF.Util.FluentValidation;
using MSF.Util.JWT;
using System.Diagnostics;

namespace MSF.UnitTests
{
    public class UnitTests : WebHelper
    {
        // [Fact] attribute is used by xUnit in .NET which identifies the method for unit
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
            BeginStep("", "GET", "");

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
    }
}