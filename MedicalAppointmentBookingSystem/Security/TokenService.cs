using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MedicalAppointmentBookingSystem.Security
{
    public  class TokenService(JwtOptions jwtOptions)
    {
        public string GenerateToken(int userId , string role)
        {
            var Claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier , userId.ToString()) ,
                new Claim(ClaimTypes.Role , role)
            };
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Signingkey));
            var Singature = new SigningCredentials(Key , SecurityAlgorithms.HmacSha256);

            var Issuer = jwtOptions.Issuer;
            var Audience = jwtOptions.Audience;

            var token = new JwtSecurityToken(

                issuer: Issuer,
                claims: Claims,
                audience: Audience,
                signingCredentials: Singature,
                expires: DateTime.UtcNow.AddHours(1)

            );
            return new JwtSecurityTokenHandler().WriteToken(token);
           
             

        }
    }
}
