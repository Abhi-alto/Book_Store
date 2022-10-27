using BuisnessLayer.Interface;
using CommonLayer.Admin;
using CommonLayer.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        IAdminBL adminBL;
        private IConfiguration _config;
        public AdminController(IAdminBL adminBL, IConfiguration config)
        {
            this.adminBL = adminBL;
            this._config = config;
        }
        [HttpPost("LoginAdmin")]
        public IActionResult LoginAdmin(AdminModel adminModel)
        {
            try
            {
                string token = this.adminBL.LoginAdmin(adminModel);
                if (token != null)
                {
                    return this.Ok(new { success = true, status = 200, Token = token, message = $"Login successful for {adminModel.Email}" });
                }
                return this.BadRequest(new { success = false, Token = token, message = $"Email not found...Register yourself" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
