using Microsoft.Data.SqlClient;
using System.Data;


namespace StoresPlace_DataAccess
{
    public class RateDTO
    {
        public int? RateID { get; set; }
        public int? StoreID { get; set; }
        public string Comment { get; set; }
        public byte? Rate { get; set; }
        public int? PersonID { get; set; }

        public RateDTO(int? rateid, int? storeid, string comment, byte? rate, int? personid)
        {
            this.RateID = rateid;
            this.StoreID = storeid;
            this.Comment = comment;
            this.Rate = rate;
            this.PersonID = personid;

        }


    }

    public class clsRateData
    {
      
        public static Nullable<int> AddNewRate(RateDTO rate)
        {
            Nullable<int> RateID = null;
            try
            {

                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_AddNewRate", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@StoreID", rate.StoreID);
                    Command.Parameters.AddWithValue("@Comment", rate.Comment);
                    Command.Parameters.AddWithValue("@Rate", rate.Rate);
                    Command.Parameters.AddWithValue("@PersonID", rate.PersonID);

                    // Output parameter
                    SqlParameter outputParameter = new SqlParameter($"@NewRateID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    Command.Parameters.Add(outputParameter);

                    Connection.Open();
                    Command.ExecuteScalar();

                    RateID = (int)Command.Parameters[$"@NewRateID"].Value;

                }
            }
            catch (Exception ex)
            {
                clsUtil.WriteExceptionInLogFile(ex);
            }

            return RateID;
        }

        public static RateDTO FindRate(int? RateID)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_FindRate", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@RateID", RateID);


                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new RateDTO
                              (
                                 reader.GetInt32(reader.GetOrdinal("RateID")),
                                 reader.GetInt32(reader.GetOrdinal("StoreID")),
                                     reader.GetString(reader.GetOrdinal("Comment")),
                                     reader.GetByte(reader.GetOrdinal("Rate")),
                                 reader.GetInt32(reader.GetOrdinal("PersonID"))
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

        public static RateDTO FindRate(int? StoreID, int? PersonID)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_FindRateByStoreIDAndPersonID", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@PersonID", PersonID);
                    Command.Parameters.AddWithValue("@StoreID", StoreID);

                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new RateDTO
                              (
                                 reader.GetInt32(reader.GetOrdinal("RateID")),
                                 reader.GetInt32(reader.GetOrdinal("StoreID")),
                                     reader.GetString(reader.GetOrdinal("Comment")),
                                     reader.GetByte(reader.GetOrdinal("Rate")),
                                 reader.GetInt32(reader.GetOrdinal("PersonID"))
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

        public static bool UpdateRate(RateDTO rate)
        {
            bool Updated = false;

            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_UpdateRate", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@RateID", rate.RateID);
                    Command.Parameters.AddWithValue("@StoreID", rate.StoreID);
                    Command.Parameters.AddWithValue("@Comment", rate.Comment);
                    Command.Parameters.AddWithValue("@Rate", rate.Rate);
                    Command.Parameters.AddWithValue("@PersonID", rate.PersonID);

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

        public static bool IsRateExists(int? RateID)
        {

            bool isFound = false;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_IsRateExists", Connection))
                {
                    Connection.Open();
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@RateID", RateID);

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

        public static bool DeleteRate(int? RateID)
        {
            bool Deleted = false;
            try
            {

                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_DeleteRate", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("@RateID", RateID);


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

        public static List<RateDTO> GetAllRate()
        {
            List<RateDTO> rate = new List<RateDTO>();
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_GetAllRate", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            rate.Add(new RateDTO
                              (
                                 reader.GetInt32(reader.GetOrdinal("RateID")),
                                 reader.GetInt32(reader.GetOrdinal("StoreID")),
                                     reader.GetString(reader.GetOrdinal("Comment")),
                                     reader.GetByte(reader.GetOrdinal("Rate")),
                                 reader.GetInt32(reader.GetOrdinal("PersonID"))
                              ));
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                clsUtil.WriteExceptionInLogFile(ex);
            }

            return rate;
        }

        public static bool IsRateExists(int? StoreID,int? PersonID)
        {

            bool isFound = false;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_IsRateExistsByStoreIDAndPersonID", Connection))
                {
                    Connection.Open();
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@PersonID", PersonID);
                    Command.Parameters.AddWithValue("@StoreID", StoreID);

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


    }
}
