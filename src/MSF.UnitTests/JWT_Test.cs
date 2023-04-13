using MSF.Util.JWT;

namespace MSF.UnitTests
{
    public class JWTTest
    {
        // [Fact] attribute is used by xUnit in .NET which identifies the method for unit
        [Fact(DisplayName = "Criar JWT")]
        public void CriarJWT()
        {
            // Arrange
            // In the arrange section we setup and declare some inputs and configuration variable

            // Act
            var token = JsonWebToken.CriarJWT();

            // Assert
            Assert.NotNull(token);
        }
    }
}