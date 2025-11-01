using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiShop.IdentityServer.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace MultiShop.IdentityServer.Controllers
{
    [Authorize(LocalApi.PolicyName)]

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public UsersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUser()
        {
            var userClaim = User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub);
            var user = await _userManager.FindByIdAsync(userClaim.Value);
            return Ok(new
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                UserName = user.UserName,
                imageUrl = user.ProfileImageUrl,

                about = user.About,
                city = user.City,
                gender = user.Gender,
                lastLoginDate = user.LastLoginDate,
                registerDate = user.RegisterDate
            });
        }
        [HttpGet("GetAllUserList")]
        public async Task<IActionResult> GetAllUserList()
        {
           var users = await _userManager.Users.ToListAsync();
            return Ok(users);
        }
        // 🔹 ID'ye göre kullanıcıyı getir
        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetUserById(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest(new { message = "Geçersiz kullanıcı ID." });

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound(new { message = "Kullanıcı bulunamadı." });

            var dto = new
            {
                id = user.Id,
                userName = user.UserName,
                email = user.Email,
                phoneNumber = user.PhoneNumber,
                name = user.Name,
                surname = user.Surname,
                about = user.About,
                city = user.City,
                gender = user.Gender,
                lastLoginDate = user.LastLoginDate,
                registerDate = user.RegisterDate,
                profileImageUrl = user.ProfileImageUrl
            };

            return Ok(dto);
        }

        // 🔹 Kullanıcı güncelleme (PUT)
        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromForm] UpdateUserRequest dto)
        {
            var user = await _userManager.FindByIdAsync(dto.Id);
            if (user == null)
                return NotFound(new { message = "Kullanıcı bulunamadı." });

            user.Name = dto.Name;
            user.Surname = dto.Surname;
            user.Email = dto.Email;
            user.PhoneNumber = dto.PhoneNumber;
            user.City = dto.City;
            user.Gender = dto.Gender;
            user.About = dto.About;

            // Klasör garanti
            var root = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "profile-images");
            if (!Directory.Exists(root)) Directory.CreateDirectory(root);

            if (dto.ProfileImage != null && dto.ProfileImage.Length > 0)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(dto.ProfileImage.FileName);
                var path = Path.Combine(root, fileName);
                using var stream = new FileStream(path, FileMode.Create);
                await dto.ProfileImage.CopyToAsync(stream);
                user.ProfileImageUrl = "/profile-images/" + fileName;
            }

            // Şifre değişimi istenmişse
            if (!string.IsNullOrWhiteSpace(dto.NewPassword))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var passResult = await _userManager.ResetPasswordAsync(user, token, dto.NewPassword);
                if (!passResult.Succeeded)
                    return BadRequest(new { message = string.Join("; ", passResult.Errors.Select(e => e.Description)) });
            }

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return BadRequest(new { message = string.Join("; ", result.Errors.Select(e => e.Description)) });

            return Ok(new { success = true, message = "Kullanıcı bilgileri başarıyla güncellendi." });
        }


        public class UpdateUserRequest
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public string City { get; set; }
            public string Gender { get; set; }
            public string About { get; set; }
            public string? NewPassword { get; set; }
            public IFormFile? ProfileImage { get; set; }
        }

    }

}
