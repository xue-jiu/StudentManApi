using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StudentManApi.Dtos;
using StudentManApi.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace StudentManApi.Controllers
{
    [Route("Api/User")]
    [ApiController]
    public class AuthenticationController:ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<Teacher> _userManager;
        private readonly SignInManager<Teacher> _signInManager;
        public AuthenticationController(IConfiguration configuration, UserManager<Teacher> userManager, SignInManager<Teacher> signInManager)
        {
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> UserLoginAsync([FromBody] TeacherLoginDto teacherLoginDto)
        {
            // 1 验证用户名密码
            var loginResult = await _signInManager.PasswordSignInAsync(teacherLoginDto.Email, teacherLoginDto.Password, false, false);
            if (!loginResult.Succeeded)
            {
                return BadRequest();
            }
            var user = await _userManager.FindByNameAsync(teacherLoginDto.Email);
            // 2 创建jwt
            // header
            var signingAlgorithm = SecurityAlgorithms.HmacSha256;
            // payload
            var claims = new List<Claim>
            {
                // sub
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(ClaimTypes.Role,"admin")
            };
            var roleNames = await _userManager.GetRolesAsync(user);
            foreach (var roleName in roleNames)
            {
                var roleClaim = new Claim(ClaimTypes.Role, roleName);
                claims.Add(roleClaim);
            }
            // signiture
            var secretByte = Encoding.UTF8.GetBytes(_configuration["Authentication:SecretKey"]);
            var signingKey = new SymmetricSecurityKey(secretByte);//加密
            var signingCredentials = new SigningCredentials(signingKey, signingAlgorithm);//生成signiture

            var token = new JwtSecurityToken(
                issuer: _configuration["Authentication:Issuer"],//也是Claim,但是这两个可能需要频繁改动,所以写在这里
                audience: _configuration["Authentication:Audience"],
                claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials
            );

            var tokenStr = new JwtSecurityTokenHandler().WriteToken(token);

            // 3 return 200 ok + jwt
            return Ok(tokenStr);
        }

        [HttpPost("register")]
        public async Task<IActionResult> UserRegisterAsync([FromBody] TeacherRegisterDto teacherRegisterDto)
        {
            var user = new Teacher()
            {
                UserName = teacherRegisterDto.Email,
                Email = teacherRegisterDto.Email,
                Address= teacherRegisterDto.Address,
                Profession= teacherRegisterDto.Profession
            };
            var result = await _userManager.CreateAsync(user, teacherRegisterDto.Password);
            if (!result.Succeeded)
            {
                return BadRequest();
            }
            return Ok();
        }



    }
}
