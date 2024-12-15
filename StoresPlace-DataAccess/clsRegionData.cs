using Microsoft.Data.SqlClient;
using System.Data;


namespace StoresPlace_DataAccess
{
    public class RegionDTO
    {
        public int? RegionID { get; set; }
        public string Code { get; set; }
        public string RegionNameAr { get; set; }
        public string RegionNameEn { get; set; }

        public RegionDTO(int? regionid, string code, string regionnamear, string regionnameen)
        {
            this.RegionID = regionid;
            this.Code = code;
            this.RegionNameAr = regionnamear;
            this.RegionNameEn = regionnameen;

        }
    }

    public class clsRegionData
    {
  
        public static Nullable<int> AddNewRegion(RegionDTO region)
        {
            Nullable<int> RegionID = null;
            try
            {

                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_AddNewRegion", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@Code", region.Code);
                    Command.Parameters.AddWithValue("@RegionNameAr", region.RegionNameAr);
                    Command.Parameters.AddWithValue("@RegionNameEn", region.RegionNameEn);

                    // Output parameter
                    SqlParameter outputParameter = new SqlParameter($"@NewRegionID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    Command.Parameters.Add(outputParameter);

                    Connection.Open();
                    Command.ExecuteScalar();

                    RegionID = (int)Command.Parameters[$"@NewRegionID"].Value;

                }
            }
            catch (Exception ex)
            {
                clsUtil.WriteExceptionInLogFile(ex);
            }

            return RegionID;
        }

        public static RegionDTO FindRegion(int? RegionID)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_FindRegion", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@RegionID", RegionID);


                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new RegionDTO
                              (
                                 reader.GetInt32(reader.GetOrdinal("RegionID")),
                                     reader.GetString(reader.GetOrdinal("Code")),
                                     reader.GetString(reader.GetOrdinal("RegionNameAr")),
                                     reader.GetString(reader.GetOrdinal("RegionNameEn"))
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

        public static bool UpdateRegion(RegionDTO region)
        {
            bool Updated = false;

            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_UpdateRegion", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@RegionID", region.RegionID);
                    Command.Parameters.AddWithValue("@Code", region.Code);
                    Command.Parameters.AddWithValue("@RegionNameAr", region.RegionNameAr);
                    Command.Parameters.AddWithValue("@RegionNameEn", region.RegionNameEn);

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

        public static bool IsRegionExists(int? RegionID)
        {

            bool isFound = false;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_IsRegionExists", Connection))
                {
                    Connection.Open();
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@RegionID", RegionID);

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

        public static bool DeleteRegion(int? RegionID)
        {
            bool Deleted = false;
            try
            {

                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_DeleteRegion", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("@RegionID", RegionID);


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

        public static List<RegionDTO> GetAllRegion()
        {
            List<RegionDTO> region = new List<RegionDTO>();
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_GetAllRegion", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            region.Add(new RegionDTO
                              (
                                 reader.GetInt32(reader.GetOrdinal("RegionID")),
                                     reader.GetString(reader.GetOrdinal("Code")),
                                     reader.GetString(reader.GetOrdinal("RegionNameAr")),
                                     reader.GetString(reader.GetOrdinal("RegionNameEn"))
                              ));
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                clsUtil.WriteExceptionInLogFile(ex);
            }

            return region;
        }

        public static List<string> GetAllRegionsByNameAr()
        {
            List<string> region = new List<string>();
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("Sp_GetAllRegionsByNameAr", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            region.Add(reader.GetString(reader.GetOrdinal("RegionNameAr")));
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                clsUtil.WriteExceptionInLogFile(ex);
            }

            return region;
        }

        public static List<string> GetAllRegionsByNameEn()
        {
            List<string> region = new List<string>();
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("Sp_GetAllRegionsByNameEn", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            region.Add(reader.GetString(reader.GetOrdinal("RegionNameEn")));
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                clsUtil.WriteExceptionInLogFile(ex);
            }

            return region;
        }
    }
}
