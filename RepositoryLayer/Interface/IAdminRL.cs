﻿using CommonLayer.Admin;
using CommonLayer.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IAdminRL
    {
        public string LoginAdmin(AdminModel adminModel);
    }
}
