using BuisnessLayer.Interface;
using CommonLayer.Admin;
using CommonLayer.User;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuisnessLayer.Services
{
    public class AdminBL : IAdminBL
    {
        IAdminRL adminRL;
        public AdminBL(IAdminRL adminRL)
        {
            this.adminRL = adminRL;
        }
        public string LoginAdmin(AdminModel adminModel)
        {
            try
            {
                return this.adminRL.LoginAdmin(adminModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
