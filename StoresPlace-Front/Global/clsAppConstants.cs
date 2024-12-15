using StoresPlace_DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoresPlace_Front.Global
{
    public class clsAppConstants
    {
        static public string Email = clsUtil.ComputeHash("Email");

        static public string Password = clsUtil.ComputeHash("Password");

        static public string ItemsLastUpdated = clsUtil.ComputeHash("ItemsLastUpdated");

    }
}
