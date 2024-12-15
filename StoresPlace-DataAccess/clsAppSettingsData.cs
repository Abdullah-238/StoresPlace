using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoresPlace_DataAccess
{
    public class clsAppSettingsData
    {
        public static string GetVersionString()
        {
            string AppVersion = "";
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))

                using (SqlCommand Command = new SqlCommand("SP_VersionTracking", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Connection.Open();

                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();

                            AppVersion = reader.GetString(reader.GetOrdinal("VersionString"));
                        }
                        else
                            return null;
                    }
                }
            }
            catch (Exception ex)
            {
                clsUtil.WriteExceptionInLogFile(ex);
            }

            return AppVersion;
        }


    }
}
