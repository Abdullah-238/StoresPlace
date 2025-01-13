using Microsoft.Data.SqlClient;
using System.Data;

namespace StoresPlace_DataAccess
{
    public class ServiceDTO
    {
        public int? ServiceID { get; set; }
        public string ServiceNameAr { get; set; }
        public string ServiceNameEn { get; set; }

        public ServiceDTO(int? serviceid, string servicenamear, string servicenameen)
        {
            this.ServiceID = serviceid;
            this.ServiceNameAr = servicenamear;
            this.ServiceNameEn = servicenameen;

        }
    }


    public class clsServiceData
    {
        public static Nullable<int> AddNewService(ServiceDTO service)
        {
            Nullable<int> ServiceID = null;
            try
            {

                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_AddNewService", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@ServiceNameAr", service.ServiceNameAr);
                    Command.Parameters.AddWithValue("@ServiceNameEn", service.ServiceNameEn);

                    // Output parameter
                    SqlParameter outputParameter = new SqlParameter($"@NewServiceID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    Command.Parameters.Add(outputParameter);

                    Connection.Open();
                    Command.ExecuteScalar();

                    ServiceID = (int)Command.Parameters[$"@NewServiceID"].Value;

                }
            }
            catch (Exception ex)
            {
                clsUtil.WriteExceptionInLogFile(ex);
            }

            return ServiceID;
        }

        public static ServiceDTO FindService(int? ServiceID)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_FindService", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@ServiceID", ServiceID);


                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new ServiceDTO
                              (
                                 reader.GetInt32(reader.GetOrdinal("ServiceID")),
                                     reader.GetString(reader.GetOrdinal("ServiceNameAr")),
                                     reader.GetString(reader.GetOrdinal("ServiceNameEn"))
                              );
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

            return null;
        }

        public static bool UpdateService(ServiceDTO service)
        {
            bool Updated = false;

            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_UpdateService", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@ServiceID", service.ServiceID);
                    Command.Parameters.AddWithValue("@ServiceNameAr", service.ServiceNameAr);
                    Command.Parameters.AddWithValue("@ServiceNameEn", service.ServiceNameEn);

                    Connection.Open();
                    int RowsAffected = Command.ExecuteNonQuery();

                    Updated = (RowsAffected > 0);
                }
            }
            catch (Exception ex)
            {
                clsUtil.WriteExceptionInLogFile(ex);
            }

            return Updated;
        }

        public static bool IsServiceExists(int? ServiceID)
        {

            bool isFound = false;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_IsServiceExists", Connection))
                {
                    Connection.Open();
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@ServiceID", ServiceID);

                    SqlParameter returnParameter = new SqlParameter("@ReturnVal", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.ReturnValue

                    };
                    Command.Parameters.Add(returnParameter);
                    Command.ExecuteNonQuery();
                    isFound = (int)returnParameter.Value == 1;

                };

            }
            catch (Exception ex)
            {
                clsUtil.WriteExceptionInLogFile(ex);
            }
            return isFound;
        }

        public static bool DeleteService(int? ServiceID)
        {
            bool Deleted = false;
            try
            {

                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_DeleteService", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("@ServiceID", ServiceID);


                        int RowsAffected = Command.ExecuteNonQuery();

                        Deleted = (RowsAffected > 0);
                    }
                }
            }
            catch (Exception ex)
            {
                clsUtil.WriteExceptionInLogFile(ex);
            }
            return Deleted;
        }

        public static List<ServiceDTO> GetAllService()
        {
            List<ServiceDTO> service = new List<ServiceDTO>();
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_GetAllService", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            service.Add(new ServiceDTO
                              (
                                 reader.GetInt32(reader.GetOrdinal("ServiceID")),
                                     reader.GetString(reader.GetOrdinal("ServiceNameAr")),
                                     reader.GetString(reader.GetOrdinal("ServiceNameEn"))
                              ));
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                clsUtil.WriteExceptionInLogFile(ex);
            }

            return service;
        }


    }
}
