using CommonLayer.Admin;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuisnessLayer.Interface
{
    public interface IAdminBL
    {
        public string LoginAdmin(AdminModel adminModel);
    }
}
