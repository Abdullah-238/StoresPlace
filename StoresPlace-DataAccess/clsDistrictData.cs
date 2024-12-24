using Microsoft.Data.SqlClient;

using System.Data;


namespace StoresPlace_DataAccess
{
    public class DistrictDTO
    {
        public int? DistrictsID { get; set; }
        public int? CityID { get; set; }
        public string DistrictsNameAr { get; set; }
        public string DistrictsNameEn { get; set; }

        public DistrictDTO(int? districtsid, int? cityid, string districtsnamear, string districtsnameen)
        {
            this.DistrictsID = districtsid;
            this.CityID = cityid;
            this.DistrictsNameAr = districtsnamear;
            this.DistrictsNameEn = districtsnameen;

        }
    }
    public class clsDistrictData
    {
       

        public static Nullable<int> AddNewDistrict(DistrictDTO district)
        {
            Nullable<int> DistrictsID = null;
            try
            {

                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_AddNewDistrict", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@CityID", district.CityID);
                    Command.Parameters.AddWithValue("@DistrictsNameAr", district.DistrictsNameAr);
                    Command.Parameters.AddWithValue("@DistrictsNameEn", district.DistrictsNameEn);

                    // Output parameter
                    SqlParameter outputParameter = new SqlParameter($"@NewDistrictsID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    Command.Parameters.Add(outputParameter);

                    Connection.Open();
                    Command.ExecuteScalar();

                    DistrictsID = (int)Command.Parameters[$"@NewDistrictsID"].Value;

                }
            }
            catch (Exception ex)
            {
                clsUtil.WriteExceptionInLogFile(ex);
            }

            return DistrictsID;
        }

        public static DistrictDTO FindDistrict(int? DistrictsID)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_FindDistrict", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@DistrictsID", DistrictsID);


                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new DistrictDTO
                              (
                                 reader.GetInt32(reader.GetOrdinal("DistrictsID")),
                                 reader.GetInt32(reader.GetOrdinal("CityID")),
                                     reader.GetString(reader.GetOrdinal("DistrictsNameAr")),
                                     reader.GetString(reader.GetOrdinal("DistrictsNameEn"))
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

        public static DistrictDTO FindDistrictsNameEn(string DistrictsNameEn)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("Sp_FindCategoryByDistrictsNameEn", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@DistrictsNameEn", DistrictsNameEn);


                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new DistrictDTO
                              (
                                 reader.GetInt32(reader.GetOrdinal("DistrictsID")),
                                 reader.GetInt32(reader.GetOrdinal("CityID")),
                                     reader.GetString(reader.GetOrdinal("DistrictsNameAr")),
                                     reader.GetString(reader.GetOrdinal("DistrictsNameEn"))
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

        public static DistrictDTO FindDistrictsNameAr(string DistrictsNameAr)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("Sp_FindCategoryByDistrictsNameAr", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@DistrictsNameAr", DistrictsNameAr);


                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new DistrictDTO
                              (
                                 reader.GetInt32(reader.GetOrdinal("DistrictsID")),
                                 reader.GetInt32(reader.GetOrdinal("CityID")),
                                     reader.GetString(reader.GetOrdinal("DistrictsNameAr")),
                                     reader.GetString(reader.GetOrdinal("DistrictsNameEn"))
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

        public static bool UpdateDistrict(DistrictDTO district)
        {
            bool Updated = false;

            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_UpdateDistrict", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@DistrictsID", district.DistrictsID);
                    Command.Parameters.AddWithValue("@CityID", district.CityID);
                    Command.Parameters.AddWithValue("@DistrictsNameAr", district.DistrictsNameAr);
                    Command.Parameters.AddWithValue("@DistrictsNameEn", district.DistrictsNameEn);

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

        public static bool IsDistrictExists(int? DistrictsID)
        {

            bool isFound = false;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_IsDistrictExists", Connection))
                {
                    Connection.Open();
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@DistrictsID", DistrictsID);

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

        public static bool DeleteDistrict(int? DistrictsID)
        {
            bool Deleted = false;
            try
            {

                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_DeleteDistrict", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("@DistrictsID", DistrictsID);


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

        public static List<DistrictDTO> GetAllDistrict()
        {
            List<DistrictDTO> district = new List<DistrictDTO>();
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_GetAllDistrict", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            district.Add(new DistrictDTO
                              (
                                 reader.GetInt32(reader.GetOrdinal("DistrictsID")),
                                 reader.GetInt32(reader.GetOrdinal("CityID")),
                                     reader.GetString(reader.GetOrdinal("DistrictsNameAr")),
                                     reader.GetString(reader.GetOrdinal("DistrictsNameEn"))
                              ));
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                clsUtil.WriteExceptionInLogFile(ex);
            }

            return district;
        }

        public static List<string> GetAllDistrictByCityNameEn(string CityNameEn)
        {
            List<string> region = new List<string>();
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("Sp_GetAllDistrictsNameEn", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@CityNameEn", CityNameEn);

                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            region.Add(reader.GetString(reader.GetOrdinal("DistrictsNameEn")));
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

        public static List<string> GetAllDistrictByCityNameAr(string CityNameAr)
        {
            List<string> region = new List<string>();
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("Sp_GetAllDistrictsNameAr", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@CityNameAr", CityNameAr);

                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            region.Add(reader.GetString(reader.GetOrdinal("DistrictsNameAr")));
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


        public static string GetDistrictsNameEnByDistrictsID(int? DistrictsID)
        {
            string CategoryNameAr = null;

            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("Sp_GetDistrictsNameEnByDistrictsID", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@DistrictsID", DistrictsID);

                    Connection.Open();

                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            CategoryNameAr = reader.GetString(reader.GetOrdinal("DistrictsNameEn"));
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                clsUtil.WriteExceptionInLogFile(ex);
            }

            return CategoryNameAr;
        }

        public static string GetDistrictsNameArByDistrictsID(int? DistrictsID)
        {
            string CategoryNameAr = null;

            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("Sp_GetDistrictsNameArByDistrictsID", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@DistrictsID", DistrictsID);

                    Connection.Open();

                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            CategoryNameAr = reader.GetString(reader.GetOrdinal("DistrictsNameAr"));
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                clsUtil.WriteExceptionInLogFile(ex);
            }

            return CategoryNameAr;
        }




        
        public static int? GetDistrictsIDByDistrictNameEn(string DistrictNameEn)
        {
            int? DistrictsID = null;

            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("Sp_GetDistrictsIDByDistrictsNameEn", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@DistrictsNameEn", DistrictNameEn);

                    Connection.Open();

                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            DistrictsID = reader.GetInt32(reader.GetOrdinal("DistrictsID"));
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                clsUtil.WriteExceptionInLogFile(ex);
            }

            return DistrictsID;
        }

        public static int? GetDistrictsIDByDistrictNameAr(string DistrictNameAr)
        {
            int? DistrictsID = null;

            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("Sp_GetDistrictsIDByDistrictsNameAr", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@DistrictsNameAr", DistrictNameAr);

                    Connection.Open();

                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            DistrictsID = reader.GetInt32(reader.GetOrdinal("DistrictsID"));
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                clsUtil.WriteExceptionInLogFile(ex);
            }

            return DistrictsID;
        }



    }
}
