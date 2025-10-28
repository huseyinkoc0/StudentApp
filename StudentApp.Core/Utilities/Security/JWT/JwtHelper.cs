using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.Core.Utilities.Security.JWT
{
    public class JwtHelper:ITokenHelper
    {
        private readonly TokenOptions _tokenOptions;
        public JwtHelper(IConfiguration configuration)
        {
            // appsettings'teki "TokenOptions" bölümünü okuyup _tokenOptions nesnesine bind et
            _tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>();
        }
        public AccessToken CreateToken(List<Claim> claims)
        {
            // 1. Gizli anahtarı al ve şifreleme anahtarı (SymmetricSecurityKey) oluştur
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.SecurityKey));

            // 2. Şifreleme algoritmasını (Credentials) belirle
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

            
            // 3. Token'ın son geçerlilik tarihini hesapla (örn: 10 dakika sonra)
            var expiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);

            // 4. Token'ı oluştur
            var token = new JwtSecurityToken(
                issuer: _tokenOptions.Issuer,
                
                audience: _tokenOptions.Audience,
                claims: claims,             // Kullanıcı bilgileri (Id, Email, Rol)
                expires: expiration,
                signingCredentials: credentials);

            // 5. Token'ı string formata çevir
            var tokenHandler = new JwtSecurityTokenHandler();
            string tokenString = tokenHandler.WriteToken(token);

            // 6. AccessToken nesnesini döndür
            return new AccessToken
            {
                Token = tokenString,
                Expiration = expiration
            };
        }
    }
}
