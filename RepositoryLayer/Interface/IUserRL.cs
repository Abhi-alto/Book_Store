using CommonLayer.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IUserRL
    {
        void Register(UserRegisterModel userRegisterModel);
        public string LoginUser(LoginModel loginModel);
        public bool ForgetPassword(string email);
        public bool ResetPassword(string email, PasswordModel passwordModel);
    }
}
