using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NETCore.Context;
using NETCore.Models;
using NETCore.Repository.Data;
using NETCore.ViewModel;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

    namespace NETCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController<Account, AccountRepository, string>
    {
        public IConfiguration _configuration;
        AccountRepository repository;
        Role role = new Role();
        public AccountController(IConfiguration config,AccountRepository account) : base(account)
        {
            _configuration = config;
            this.repository = account;
        }

        [HttpPost("ForgotPassword")]
        public ActionResult ForgotPassword(ForgotPasswordVM forgotPassword)
        {
            var result = repository.ForgotPassword(forgotPassword);
            if (result > 0)
            {
                return BadRequest(new
                {
                    status = HttpStatusCode.BadRequest,
                    message = "Email Tidak ditemukan"
                });
            }
            else
                return Ok(new
                {
                    status = HttpStatusCode.OK,
                    message = "Reset Password sudah dikirim ke email"
                });

        }

        [HttpPut("UpdatePassword")]
        public ActionResult Update(ChangePassword changepassword)
        {
            var result = repository.UpdatePassword(changepassword);
            try
            {
                if (result == 200)
                {
                    //return Ok("Data Berhasil di Update");
                    return BadRequest(new
                    {
                        status = HttpStatusCode.BadRequest,
                        message = "Password Salah"
                    });
                }
                else if (result == 100)
                {
                    return BadRequest(new
                    {
                        status = HttpStatusCode.BadRequest,
                       message = "Email Salah"
                   });
                }
                else if (result == 300)
                {
                    return BadRequest(new
                    {
                        status = HttpStatusCode.BadRequest,
                        message = "Password Baru Tidak Sama"
                    });
                }
            }
            catch
            {
                
            }
            return Ok(new
            {
                status = HttpStatusCode.OK,
                message = "Password berhasil di ubah"
            });
        }
        [HttpPost("Login")]
        public ActionResult Login(LoginVM loginVm)
        {
            var login = repository.Login(loginVm);

            if (login == 0)
            {
                return NotFound("Email Belum Terdaftar");
            }
            else if (login == 1)
            {
                return BadRequest("Password Salah");
            }
            else
            {
                string[] roles = repository.Roles(loginVm.Email);
                var claim = new List<Claim>();
                claim.Add(new Claim("email", loginVm.Email));
                foreach (string d in roles)
                {
                    claim.Add(new Claim("roles", d));
                }
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claim, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);

                return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                //return Ok(new
                //{
                //    status = HttpStatusCode.OK,
                //    message = "Login Berhasil"
                //});
            }
        }
        //private async Task<Account> GetUser(string email, string password)
        //{
        //    return await repository.Account.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        //}
    }
}
