using StoresPlace_DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoresPlace_Business
{
    public class clsAppSettings
    {
        public static string VersionString()
        {
            string VersionString = clsAppSettingsData.GetVersionString();

            if (VersionString != null)
                return VersionString;
            else
                return null;
        }
    }
}
