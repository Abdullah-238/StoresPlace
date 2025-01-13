using Microsoft.Data.SqlClient;
using System.Data;


namespace StoresPlace_DataAccess
{
    public class clsTypeData
    {
        public class TypeDTO
        {
            public int? TypeID { get; set; }
            public string TypeNameAr { get; set; }

            public string TypeNameEn { get; set; }

            public TypeDTO(int? typeid, string typenamear,string typenameen)
            {
                this.TypeID = typeid;
                this.TypeNameAr = typenamear;
                this.TypeNameEn = typenameen;

            }
        }

    
        public static TypeDTO FindType(int? TypeID)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_FindType", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@TypeID", TypeID);


                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new TypeDTO
                              (
                                 reader.GetInt32(reader.GetOrdinal("TypeID")),
                                     reader.GetString(reader.GetOrdinal("TypeNameAr")),
                                                                          reader.GetString(reader.GetOrdinal("TypeNameEn"))

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

        public static TypeDTO FindTypeEn(string TypeNameEn)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("Sp_GetTypesByTypeNameEn", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@TypeNameEn", TypeNameEn);


                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new TypeDTO
                              (
                                 reader.GetInt32(reader.GetOrdinal("TypeID")),
                                     reader.GetString(reader.GetOrdinal("TypeNameAr")),
                                                                          reader.GetString(reader.GetOrdinal("TypeNameEn"))

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

        public static TypeDTO FindTypeAr(string TypeNameAr)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("Sp_GetTypesByTypeNameAr", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@TypeNameAr", TypeNameAr);


                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new TypeDTO
                              (
                                 reader.GetInt32(reader.GetOrdinal("TypeID")),
                                     reader.GetString(reader.GetOrdinal("TypeNameAr")),
                                                                          reader.GetString(reader.GetOrdinal("TypeNameEn"))

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

        public static List<TypeDTO> GetAllType()
        {
            List<TypeDTO> type = new List<TypeDTO>();
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_GetAllType", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            type.Add(new TypeDTO
                              (
                                 reader.GetInt32(reader.GetOrdinal("TypeID")),
                                     reader.GetString(reader.GetOrdinal("TypeNameAr")),
                                                                          reader.GetString(reader.GetOrdinal("TypeNameEn"))
                              ));
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                clsUtil.WriteExceptionInLogFile(ex);
            }

            return type;
        }

        public static List<string> GetAllTypeEn()
        {
            List<string> Types = new List<string>();
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_GetAllTypeEn", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Types.Add(reader.GetString(reader.GetOrdinal("TypeNameEn")));
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                clsUtil.WriteExceptionInLogFile(ex);
            }

            return Types;
        }

        public static List<string> GetAllTypeAr()
        {
            List<string> Types = new List<string>();
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_GetAllTypeAr", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Types.Add(reader.GetString(reader.GetOrdinal("TypeNameAr")));
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                clsUtil.WriteExceptionInLogFile(ex);
            }

            return Types;
        }
    }

}
