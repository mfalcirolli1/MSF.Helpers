using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace MSF.Util.JWT
{
    public static class JsonWebToken
    {
        public static string CriarJWT()
        {
            // Defina a chave secreta que será usada para assinar o token JWT
            string chaveSecreta = "minhaChaveSecreta123";

            // Defina as informações do usuário que serão incluídas no token JWT
            var claims = new[]
            {
                new Claim("nome", "João da Silva"),
                new Claim("email", "joao.silva@gmail.com"),
                new Claim("perfil", "administrador")
            };

            // Crie o token JWT com as informações do usuário e a chave secreta definida anteriormente
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chaveSecreta)),
                                                            SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            Debug.WriteLine($"Token: {tokenString}");

            return tokenString;
        }

        public static void ReadJWT(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);

            var item = jwtToken.Claims.Where(m => m.Type == "claim_key").FirstOrDefault().Value;
        }
    }
}
