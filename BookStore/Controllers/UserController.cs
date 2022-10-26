using Microsoft.AspNetCore.Mvc;
using CommonLayer;
using BuisnessLayer.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RepositoryLayer.Interface;
using RepositoryLayer.Services;
using System;
using CommonLayer.User;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace BookStore.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        IUserBL userBL;
        private IConfiguration _config;
        public UserController(IUserBL userBL, IConfiguration config)
        {
            this.userBL = userBL;
            this._config = config;
        }
        [HttpPost("Register")]
        public IActionResult Register(UserRegisterModel userRegisterModel)
        {
            try
            {
                this.userBL.Register(userRegisterModel);
                return this.Ok(new { sucess = true, status = 200, message = $"Registartion successfull for {userRegisterModel.Email}" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost("LoginUser")]
        public IActionResult LoginUser(LoginModel loginModel)
        {
            try
            {
                string token = this.userBL.LoginUser(loginModel);
                if (token != null)
                {
                    return this.Ok(new { success = true, status = 200, Token = token, message = $"Login successful for {loginModel.Email}" });
                }
                return this.BadRequest(new { success = false, Token = token, message = $"Email not found...Register yourself" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost("ForgetPassword/{email}")]
        public IActionResult ForgetPassword(string email)
        {
            try
            {
                bool token = this.userBL.ForgetPassword(email);
                if (token != false)
                {
                    return this.Ok(new { success = true, status = 200, message = $"Reset Password link sent to the email id - {email}" });
                }
                return this.BadRequest(new { success = false, message = $"Reset password link not sent" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpPut("ResetPassword")]
        public IActionResult ResetPassword(string Email,PasswordModel passwordModel)
        {
            try
            {
                if (passwordModel.NewPassword != passwordModel.CPassword)
                {
                    return this.BadRequest(new { success = false, message = "New Password and Confirm Password are not equal." });
                }
                //Authorization, match email from token
                
                bool res = this.userBL.ResetPassword(Email, passwordModel);
                if (res == false)
                {
                    return this.BadRequest(new { success = false, message = "Password not updated" });
                }
                return this.Ok(new { success = true, status = 200, message = "Password changed successfully" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
