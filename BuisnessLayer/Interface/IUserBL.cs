using CommonLayer.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuisnessLayer.Interface
{
    public interface IUserBL
    {
        void Register(UserRegisterModel userRegisterModel);
    }
}
