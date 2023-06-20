using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BrainStormerBackend.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BrainStormerBackend.Controllers.v2
{

    [ApiController]
    [Route("api/v2/[controller]")]
    public class IdentityController : Controller
    {

        private const string Secret = "SecretSecretSecretSecretSecretSecretSecretSecretSecretSecret";

        private static readonly TimeSpan TokenLifetime = TimeSpan.FromHours(10);

        [HttpPost]
        [Route("token")]
        public IActionResult Token([FromBody] TokenRequest request)
        {
            if (request.Secret != Secret)
            {
                return Unauthorized();
            }

            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(Secret);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, request.Username),
                new Claim(ClaimTypes.Role, "User"),
            };


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(TokenLifetime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = handler.CreateToken(tokenDescriptor);

            return Ok(new
            {
                token = handler.WriteToken(token),
                expiration = token.ValidTo
            });

        }
    }
}
