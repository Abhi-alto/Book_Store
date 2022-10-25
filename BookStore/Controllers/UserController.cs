using Microsoft.AspNetCore.Mvc;
using CommonLayer;
using BuisnessLayer.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RepositoryLayer.Interface;
using RepositoryLayer.Services;
using System;
using CommonLayer.User;

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
    }
}
