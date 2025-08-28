using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MultiShop.IdentityServer.Tools
{
    public class JwtTokenGenerator
    {
        public static TokenResponseViewModel GenerateToken(GetCheckAppUserViewModel getCheckAppUserViewModel)
        {
            // Token oluşturma işlemleri burada yapılacak
            // Bu örnekte basit bir token ve geçerlilik süresi döndürüyoruz
            var claims = new List<Claim>(); //Token'a eklenecek claim'ler
            if (!string.IsNullOrWhiteSpace(getCheckAppUserViewModel.Role)) //Role boş değilse

                claims.Add(new Claim(ClaimTypes.Role, getCheckAppUserViewModel.Role)); //Role claim'i ekleniyor

            claims.Add(new Claim(ClaimTypes.NameIdentifier, getCheckAppUserViewModel.ID.ToString())); //ID claim'i ekleniyor


            if (!string.IsNullOrWhiteSpace(getCheckAppUserViewModel.UserName)) //Username boş değilse
                claims.Add(new Claim("Username", getCheckAppUserViewModel.UserName)); //Username claim'i ekleniyor

            var key=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokenDefaults.Key)); //Token'ı imzalamak için kullanılan anahtar
            var creds=new SigningCredentials(key,SecurityAlgorithms.HmacSha256); //İmzalama bilgileri oluşturuluyor 
            var expire=DateTime.UtcNow.AddDays(JwtTokenDefaults.Expire); //Token'ın geçerlilik süresi
            JwtSecurityToken token=new JwtSecurityToken(
                issuer:JwtTokenDefaults.ValidIssuer, //Token'ı veren
                audience:JwtTokenDefaults.ValidAudience, //Token'ın geçerli olduğu kitle
                claims:claims, //Token'a eklenecek claim'ler
                notBefore:DateTime.UtcNow, //Token'ın geçerli olmaya başlayacağı zaman
                expires:expire, //Token'ın geçerlilik süresi
                signingCredentials: creds //İmzalama bilgileri
                );
            JwtSecurityTokenHandler tokenHandler= new JwtSecurityTokenHandler(); //JWT token'ı işlemek için handler oluşturuluyor
            return new TokenResponseViewModel(tokenHandler.WriteToken(token), expire); //Token ve geçerlilik süresi döndürülüyor
        }
    }
}
