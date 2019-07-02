using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Web.Api.Infrastructure.Environments;
using Web.Api.Modules.Auth.Domain;
using Web.Api.Modules.Auth.Dtos;
using Web.Api.src.Infrastructure.Guards;

namespace Web.Api.Infrastructure.Guards
{
    public class Token
    {
        public bool Authenticated { get; set; }
        public string Created { get; set; }
        public string Expiration { get; set; }
        public string AccessToken { get; set; }
        public string Message { get; set; }
    }

    public class TokenConfigurations
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int Seconds { get; set; }
    }

    public class Jwt
    {
        private TokenConfigurations _tokenConfigurations;

        string Base64Decode(string value)
        {
            return Encoding.Default.GetString(Convert.FromBase64String(value));
        }

        public JwtDto DecodeToken(string token)
        {

            // remove bearer
            token = token.Replace("Bearer", "").Trim();
            var pieces = token.Split('.').Take(2);
            // Pad them with equals signs to a length that is a multiple of four:
            var paddedPieces = pieces.Select(x => x.PadRight(x.Length + (x.Length % 4), '='));
            // Base64 decode the pieces:
            var decodedPieces = paddedPieces.Select(x => Base64Decode(x));
            // Join it all back into one string with .Aggregate:
            string decoded = decodedPieces.Aggregate((s1, s2) =>  s2);

            var payload = JsonConvert.DeserializeObject<JwtDto>(decoded);

            return payload;

        }

        public Jwt(TokenConfigurations tokenConfigurations)
        {
            _tokenConfigurations = tokenConfigurations;
        }
        public Token GenerateToken(User user)
        {
            ClaimsIdentity identity = new ClaimsIdentity(
                new GenericIdentity(user.Email, "Login"),
                new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.Email)
                }
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("ptg-challenge-accepted");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
              {
                 new Claim(ClaimTypes.Name, user.Email.ToString())
              }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new Token()
            {
                Authenticated = true,
                Created = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"),
                Expiration = DateTime.UtcNow.AddDays(7).ToString("yyyy-MM-dd HH:mm:ss"),
                AccessToken = tokenHandler.WriteToken(token),
                Message = "OK"
            };
        }
    }
}
