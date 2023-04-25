using Backend.Application.Abstractions;
using Backend.Application.Dtos;
using Backend.Domain.EntityModels;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Backend.Infuructures.Concrete
{
    public class TokenManager : ITokenManager
    {
        private IConfiguration _Config;

        public TokenManager(IConfiguration config)
        {
            _Config = config;
        }

        public TokenDto GetAccessToken(int minutes,Employe User,IList<string> Roles)
        {
            //Sifrelenmiş kimliğimn symetriği alınmakta
            SymmetricSecurityKey SecurityKey = new(Encoding.UTF8.GetBytes(_Config["Jwt:SecurityKey"]));
            //Sifrelenmiş kimlik oluşturulmakta
            SigningCredentials signingCredentials = new(SecurityKey, SecurityAlgorithms.HmacSha256);

            // Oluşturulacak YToken yaralamaları Yapılmakta
            var claimList = new List<Claim> { new(ClaimTypes.Name, User.UserName) };
            
            foreach (var userRole in Roles)
            {
                claimList.Add(new Claim(ClaimTypes.Role, userRole));
            }

            JwtSecurityToken securityToken = new(
                //doğrullanacak kısım
                issuer: _Config["Jwt:Issuer"],
                expires: DateTime.Now.AddMinutes(minutes),
                //bu kısımda ne zaman devreye gireceği
                notBefore: DateTime.Now,
                //dogrulanacak key
                signingCredentials: signingCredentials,
                //kime gidip geleceği
                audience: _Config["jwt:Audience"],
                
                //bu token hangi kullanıcıya ait
                claims : claimList
                );
            // token yazma işlemleri
            JwtSecurityTokenHandler tokenHandler = new();
            var model = new TokenDto();
            model.ExpiresDate = DateTime.Now.AddMinutes(minutes);
            model.Token = tokenHandler.WriteToken(securityToken);
            model.AccessToken = getRefreshToken();
            return model;
        }

        public string getRefreshToken()
        {
            byte[] number = new byte[32];
            using RandomNumberGenerator random = RandomNumberGenerator.Create();
            random.GetBytes(number);
            return Convert.ToBase64String(number);
        }
    }
}
