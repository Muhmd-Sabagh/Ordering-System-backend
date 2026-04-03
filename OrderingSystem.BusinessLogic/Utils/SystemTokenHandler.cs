using Microsoft.IdentityModel.Tokens;
using OrderingSystem.Global.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OrderingSystem.BusinessLogic.Utils
{
    public class SystemTokenHandler
    {
        private static readonly string _secretKey = "C3k0EINrjzlfR6BD0a/5O/kkG5+02rPRz0cOn2EM7IiwJ2iKchP+zKHuNKn3cbhgmhR5S9AdHGwnGfNnFh6aHw==";

        private static string PrivateGenerateToken(Customer user, int expireDays = 1)
        {
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new(JwtRegisteredClaimNames.UniqueName, user.Username),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var creds = new SigningCredentials(
                    key: GetSecurityKey(),
                    algorithm: SecurityAlgorithms.HmacSha256
                    );

            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: creds,
                expires: DateTime.UtcNow.AddDays(expireDays)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static string GenerateToken(Customer user, int expireDays = 1) => PrivateGenerateToken(user, expireDays);

        public static SecurityKey GetSecurityKey() => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
    }
}
