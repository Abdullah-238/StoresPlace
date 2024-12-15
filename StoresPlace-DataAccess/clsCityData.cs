using Microsoft.Data.SqlClient;
using System.Data;


namespace StoresPlace_DataAccess
{
    public class CityDTO
    {
        public int? CityID { get; set; }
        public string CityNameAr { get; set; }
        public string CityNameEn { get; set; }
        public int? RegionID { get; set; }

        public CityDTO(int? cityid, string citynamear, string citynameen, int? regionid)
        {
            this.CityID = cityid;
            this.CityNameAr = citynamear;
            this.CityNameEn = citynameen;
            this.RegionID = regionid;

        }
    }
    public class clsCityData
    {
       

        public static Nullable<int> AddNewCity(CityDTO City)
        {
            Nullable<int> CityID = null;
            try
            {

                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_AddNewCity", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@CityNameAr", City.CityNameAr);
                    Command.Parameters.AddWithValue("@CityNameEn", City.CityNameEn);
                    Command.Parameters.AddWithValue("@RegionID", City.RegionID);

                    // Output parameter
                    SqlParameter outputParameter = new SqlParameter($"@NewCityID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    Command.Parameters.Add(outputParameter);

                    Connection.Open();
                    Command.ExecuteScalar();

                    CityID = (int)Command.Parameters[$"@NewCityID"].Value;

                }
            }
            catch (Exception ex)
            {
                clsUtil.WriteExceptionInLogFile(ex);
            }

            return CityID;
        }

        public static CityDTO FindCity(int? CityID)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_FindCity", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@CityID", CityID);


                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new CityDTO
                              (
                                 reader.GetInt32(reader.GetOrdinal("CityID")),
                                     reader.GetString(reader.GetOrdinal("CityNameAr")),
                                     reader.GetString(reader.GetOrdinal("CityNameEn")),
                                 reader.GetInt32(reader.GetOrdinal("RegionID"))
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

        public static bool UpdateCity(CityDTO City)
        {
            bool Updated = false;

            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_UpdateCity", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@CityID", City.CityID);
                    Command.Parameters.AddWithValue("@CityNameAr", City.CityNameAr);
                    Command.Parameters.AddWithValue("@CityNameEn", City.CityNameEn);
                    Command.Parameters.AddWithValue("@RegionID", City.RegionID);

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

        public static bool IsCityExists(int? CityID)
        {

            bool isFound = false;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_IsCityExists", Connection))
                {
                    Connection.Open();
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@CityID", CityID);

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

        public static bool DeleteCity(int? CityID)
        {
            bool Deleted = false;
            try
            {

                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_DeleteCity", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("@CityID", CityID);


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

        public static List<CityDTO> GetAllCity()
        {
            List<CityDTO> City = new List<CityDTO>();
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_GetAllCity", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            City.Add(new CityDTO
                              (
                                 reader.GetInt32(reader.GetOrdinal("CityID")),
                                 reader.GetString(reader.GetOrdinal("CityNameAr")),
                                 reader.GetString(reader.GetOrdinal("CityNameEn")),
                                 reader.GetInt32(reader.GetOrdinal("RegionID"))
                              ));
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                clsUtil.WriteExceptionInLogFile(ex);
            }

            return City;
        }

        public static List<string> GetAllCitiesByRegionNameEn(string RegionNameEn)
        {
            List<string> Citeies = new List<string>();
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("Sp_GetAllCitiesByRegionNameEn", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@RegionNameEn", RegionNameEn);

                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Citeies.Add(reader.GetString(reader.GetOrdinal("CityNameEn")));
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                clsUtil.WriteExceptionInLogFile(ex);
            }

            return Citeies;
        }

        public static List<string> GetAllCitiesByRegionNameAr(string RegionNameAr)
        {
            List<string> Citeies = new List<string>();
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("Sp_GetAllCitiesByRegionNameAr", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@RegionNameAr", RegionNameAr);

                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Citeies.Add(reader.GetString(reader.GetOrdinal("CityNameAr")));
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                clsUtil.WriteExceptionInLogFile(ex);
            }

            return Citeies;
        }


    }
}
