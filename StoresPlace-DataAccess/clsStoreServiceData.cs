using Microsoft.Data.SqlClient;
using System.Data;


namespace StoresPlace_DataAccess
{
    public class clsStoreServiceData
    {
        public class StoreServiceDTO
        {
            public int? StoreServiceIID { get; set; }
            public int? ServiceID { get; set; }
            public int? StoreID { get; set; }

            public StoreServiceDTO(int? storeserviceiid, int? serviceid, int? storeid)
            {
                this.StoreServiceIID = storeserviceiid;
                this.ServiceID = serviceid;
                this.StoreID = storeid;

            }
        }

        public static Nullable<int> AddNewStoreService(StoreServiceDTO storeservic)
        {
            Nullable<int> StoreServiceIID = null;
            try
            {

                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_AddNewStoreService", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@ServiceID", storeservic.ServiceID);
                    Command.Parameters.AddWithValue("@StoreID", storeservic.StoreID);

                    // Output parameter
                    SqlParameter outputParameter = new SqlParameter($"@NewStoreServiceIID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    Command.Parameters.Add(outputParameter);

                    Connection.Open();
                    Command.ExecuteScalar();

                    StoreServiceIID = (int)Command.Parameters[$"@NewStoreServiceIID"].Value;

                }
            }
            catch (Exception ex)
            {
                clsUtil.WriteExceptionInLogFile(ex);
            }

            return StoreServiceIID;
        }

        public static StoreServiceDTO FindStoreService(int? StoreServiceIID)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_FindStoreService", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@StoreServiceIID", StoreServiceIID);


                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new StoreServiceDTO
                              (
                                 reader.GetInt32(reader.GetOrdinal("StoreServiceIID")),
                                 reader.GetInt32(reader.GetOrdinal("ServiceID")),
                                 reader.GetInt32(reader.GetOrdinal("StoreID"))
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

        public static bool UpdateStoreService(StoreServiceDTO storeservic)
        {
            bool Updated = false;

            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_UpdateStoreService", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@StoreServiceIID", storeservic.StoreServiceIID);
                    Command.Parameters.AddWithValue("@ServiceID", storeservic.ServiceID);
                    Command.Parameters.AddWithValue("@StoreID", storeservic.StoreID);

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

        public static bool IsStoreServiceExists(int? StoreServiceIID)
        {

            bool isFound = false;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_IsStoreServiceExists", Connection))
                {
                    Connection.Open();
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@StoreServiceIID", StoreServiceIID);

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

        public static bool DeleteStoreService(int? StoreServiceIID)
        {
            bool Deleted = false;
            try
            {

                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_DeleteStoreService", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("@StoreServiceIID", StoreServiceIID);


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

        public static List<StoreServiceDTO> GetAllStoreService()
        {
            List<StoreServiceDTO> storeservic = new List<StoreServiceDTO>();
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_GetAllStoreService", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            storeservic.Add(new StoreServiceDTO
                              (
                                 reader.GetInt32(reader.GetOrdinal("StoreServiceIID")),
                                 reader.GetInt32(reader.GetOrdinal("ServiceID")),
                                 reader.GetInt32(reader.GetOrdinal("StoreID"))
                              ));
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                clsUtil.WriteExceptionInLogFile(ex);
            }

            return storeservic;
        }


    }
}
