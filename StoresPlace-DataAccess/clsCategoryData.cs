using Microsoft.Data.SqlClient;
using System.Data;


namespace StoresPlace_DataAccess
{
    public class CategoryDTO
    {
        public int? CategoryID { get; set; }
        public string CategoryNameAr { get; set; }
        public string CategoryNameEn { get; set; }

        public CategoryDTO(int? categoryid, string categorynamear, string categorynameen)
        {
            this.CategoryID = categoryid;
            this.CategoryNameAr = categorynamear;
            this.CategoryNameEn = categorynameen;

        }
    }

    public class clsCategoriesData
    {
        
        public static Nullable<int> AddNewCategory(CategoryDTO Category)
        {
            Nullable<int> CategoryID = null;
            try
            {

                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_AddNewCategory", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@CategoryNameAr", Category.CategoryNameAr);
                    Command.Parameters.AddWithValue("@CategoryNameEn", Category.CategoryNameEn);

                    // Output parameter
                    SqlParameter outputParameter = new SqlParameter($"@NewCategoryID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    Command.Parameters.Add(outputParameter);

                    Connection.Open();
                    Command.ExecuteScalar();

                    CategoryID = (int)Command.Parameters[$"@NewCategoryID"].Value;

                }
            }
            catch (Exception ex)
            {
                clsUtil.WriteExceptionInLogFile(ex);
            }

            return CategoryID;
        }

        public static CategoryDTO FindCategory(int? CategoryID)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_FindCategory", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@CategoryID", CategoryID);


                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new CategoryDTO
                              (
                                 reader.GetInt32(reader.GetOrdinal("CategoryID")),
                                     reader.GetString(reader.GetOrdinal("CategoryNameAr")),
                                     reader.GetString(reader.GetOrdinal("CategoryNameEn"))
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

        public static CategoryDTO FindCategoryByCategoryNameAr(string CategoryNameAr)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("Sp_FindCategoryByCategoryNameAr", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@CategoryNameAr", CategoryNameAr);


                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new CategoryDTO
                              (
                                 reader.GetInt32(reader.GetOrdinal("CategoryID")),
                                     reader.GetString(reader.GetOrdinal("CategoryNameAr")),
                                     reader.GetString(reader.GetOrdinal("CategoryNameEn"))
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

        public static CategoryDTO FindCategoryByCategoryNameEn(string CategoryNameEn)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("Sp_FindCategoryByCategoryNameEn", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@CategoryNameEn", CategoryNameEn);


                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new CategoryDTO
                              (
                                 reader.GetInt32(reader.GetOrdinal("CategoryID")),
                                     reader.GetString(reader.GetOrdinal("CategoryNameAr")),
                                     reader.GetString(reader.GetOrdinal("CategoryNameEn"))
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

        public static bool UpdateCategory(CategoryDTO Category)
        {
            bool Updated = false;

            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_UpdateCategory", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@CategoryID", Category.CategoryID);
                    Command.Parameters.AddWithValue("@CategoryNameAr", Category.CategoryNameAr);
                    Command.Parameters.AddWithValue("@CategoryNameEn", Category.CategoryNameEn);

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

        public static bool IsCategoryExists(int? CategoryID)
        {

            bool isFound = false;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_IsCategoryExists", Connection))
                {
                    Connection.Open();
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@CategoryID", CategoryID);

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

        public static bool DeleteCategory(int? CategoryID)
        {
            bool Deleted = false;
            try
            {

                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_DeleteCategory", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("@CategoryID", CategoryID);


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

        public static List<CategoryDTO> GetAllCategory()
        {
            List<CategoryDTO> Category = new List<CategoryDTO>();
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_GetAllCategory", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Category.Add(new CategoryDTO
                              (
                                 reader.GetInt32(reader.GetOrdinal("CategoryID")),
                                     reader.GetString(reader.GetOrdinal("CategoryNameAr")),
                                     reader.GetString(reader.GetOrdinal("CategoryNameEn"))
                              ));
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                clsUtil.WriteExceptionInLogFile(ex);
            }

            return Category;
        }

        public static List<string> GetAllCategoryAvailableByNameEn(int? TypeID)
        {
            List<string> Category = new List<string>();
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_GetAllCategoryAvailableByNameEn", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@TypeID", TypeID);

                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Category.Add(reader.GetString(reader.GetOrdinal("CategoryNameEn")));
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                clsUtil.WriteExceptionInLogFile(ex);
            }

            return Category;
        }

        public static List<string> GetAllCategoryAvailableByNameAr(int? TypeID)
        {
            List<string> Category = new List<string>();
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_GetAllCategoryAvailableByNameAr", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.Parameters.AddWithValue("@TypeID", TypeID);

                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Category.Add(reader.GetString(reader.GetOrdinal("CategoryNameAr")));
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                clsUtil.WriteExceptionInLogFile(ex);
            }

            return Category;
        }

        public static List<string> GetAllCategoryEn()
        {
            List<string> Category = new List<string>();
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_GetAllCategoryEn", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Category.Add(reader.GetString(reader.GetOrdinal("CategoryNameEn")));
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                clsUtil.WriteExceptionInLogFile(ex);
            }

            return Category;
        }

        public static List<string> GetAllCategoryAr()
        {
            List<string> Category = new List<string>();
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_GetAllCategoryAr", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Category.Add(reader.GetString(reader.GetOrdinal("CategoryNameAr")));
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                clsUtil.WriteExceptionInLogFile(ex);
            }

            return Category;
        }

        public static string GetCategoryNameArByCategoryID(int? CategoryID)
        {
            string CategoryNameAr = null;

            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("Sp_GetCategoryNameArByCategoryID", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@CategoryID", CategoryID);

                    Connection.Open();

                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            CategoryNameAr =  reader.GetString(reader.GetOrdinal("CategoryNameAr"));
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

        public static string GetCategoryNameEnByCategoryID(int? CategoryID)
        {
            string CategoryNameAr = null;

            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("Sp_GetCategoryNameEnByCategoryID", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@CategoryID", CategoryID);

                    Connection.Open();

                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            CategoryNameAr = reader.GetString(reader.GetOrdinal("CategoryNameEn"));
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



        public static int? GetCategoryIdByCategoryNameEn(string CategoryNameEn)
        {
            int? Category = null;

            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("Sp_GetCategoryidByCategoryNameEn", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@CategoryNameEn", CategoryNameEn);

                    Connection.Open();

                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Category = reader.GetInt32(reader.GetOrdinal("Categoryid"));
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                clsUtil.WriteExceptionInLogFile(ex);
            }

            return Category;
        }

        public static int? GetCategoryIdByCategoryNameAr(string CategoryNameAr)
        {
            int? Category = null;

            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("Sp_GetCategoryidByCategoryNameAr", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@CategoryNameAr", CategoryNameAr);

                    Connection.Open();

                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Category = reader.GetInt32(reader.GetOrdinal("Categoryid"));
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                clsUtil.WriteExceptionInLogFile(ex);
            }

            return Category;
        }


    }

}
