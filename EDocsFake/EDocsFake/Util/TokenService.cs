using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EDocsFake.Util
{
    public static class TokenService
    {

        #region Metodos

        /// <summary>
        /// GerarToken
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="secret"></param>
        /// <returns></returns>
        public static string GerarToken(Data.Entidades.Usuario usuario, string secret)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {

                Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, usuario.NomeUsuario)

            }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        #endregion

    }
}