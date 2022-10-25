using BuisnessLayer.Interface;
using CommonLayer.User;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
namespace BuisnessLayer.Services
{
    public class UserBL : IUserBL
    {
        IUserRL userRL;
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }

        public void Register(UserRegisterModel RegisterModel)
        {
            try
            {
                this.userRL.Register(RegisterModel);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
