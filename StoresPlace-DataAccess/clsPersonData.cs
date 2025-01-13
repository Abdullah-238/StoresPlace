using Microsoft.Data.SqlClient;
using System.Data;

namespace StoresPlace_DataAccess
{

    public class PersonDTO
    {
        public int? PersonID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        public PersonDTO(int? personid, string name, string phone, string email, string password, bool isactive)
        {
            this.PersonID = personid;
            this.Name = name;
            this.Phone = phone;
            this.Email = email;
            this.Password = password;
            this.IsActive = isactive;

        }
    }

    public class clsPersonData
    {

        public static Nullable<int> AddNewPerson(PersonDTO person)
        {
            Nullable<int> PersonID = null;
            try
            {

                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_AddNewPerson", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@Name", person.Name);
                    Command.Parameters.AddWithValue("@Phone", person.Phone);
                    Command.Parameters.AddWithValue("@Email", (person.Email == null ? DBNull.Value : person.Email));
                    Command.Parameters.AddWithValue("@Password", person.Password);
                    Command.Parameters.AddWithValue("@IsActive", person.IsActive);

                    // Output parameter
                    SqlParameter outputParameter = new SqlParameter($"@NewPersonID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    Command.Parameters.Add(outputParameter);

                    Connection.Open();
                    Command.ExecuteScalar();

                    PersonID = (int)Command.Parameters[$"@NewPersonID"].Value;

                }
            }
            catch (Exception ex)
            {
                clsUtil.WriteExceptionInLogFile(ex);
            }

            return PersonID;
        }

        public static PersonDTO FindPerson(int? PersonID)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_FindPerson", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@PersonID", PersonID);


                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new PersonDTO
                              (
                                  reader.GetInt32(reader.GetOrdinal("PersonID")),
                                     reader.GetString(reader.GetOrdinal("Name")),
                                     reader.GetString(reader.GetOrdinal("Phone")),
                                     (!reader.IsDBNull(reader.GetOrdinal("Email")) ? reader.GetString("Email") : ""),
                                     reader.GetString(reader.GetOrdinal("Password")),
                                     reader.GetBoolean(reader.GetOrdinal("IsActive"))
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

        public static bool UpdatePerson(PersonDTO person)
        {
            bool Updated = false;

            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_UpdatePerson", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@PersonID", person.PersonID);
                    Command.Parameters.AddWithValue("@Name", person.Name);
                    Command.Parameters.AddWithValue("@Phone", person.Phone);
                    Command.Parameters.AddWithValue("@Email", (person.Email == null ? DBNull.Value : person.Email));
                    Command.Parameters.AddWithValue("@Password", person.Password);
                    Command.Parameters.AddWithValue("@IsActive", person.IsActive);

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

        public static bool IsPersonExists(int? PersonID)
        {

            bool isFound = false;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_IsPersonExists", Connection))
                {
                    Connection.Open();
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@PersonID", PersonID);

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


        public static bool IsPersonActive(int? PersonID)
        {

            bool isFound = false;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_IsPersonActive", Connection))
                {
                    Connection.Open();
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@PersonID", PersonID);

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

        public static bool IsPersonExistsByPhone(string Phone)
        {

            bool isFound = false;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_IsPersonExistsByPhone", Connection))
                {
                    Connection.Open();
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@Phone", Phone);

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

        public static bool IsPersonExistsByEmail(string Email)
        {

            bool isFound = false;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_IsPersonExistsByEmail", Connection))
                {
                    Connection.Open();
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@Email", Email);

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

        public static bool IsPersonActiveByEmail(string Email)
        {
            bool IsActive = false;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_IsPersonActiveByEmail", Connection))
                {
                    Connection.Open();
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@Email", Email);

                    var result = Command.ExecuteScalar();

                    if (result != null)
                    {
                        IsActive = Convert.ToByte(result) == 1;  
                    }
                }
            }
            catch (Exception ex)
            {
                clsUtil.WriteExceptionInLogFile(ex);
            }

            return IsActive;
        }


        public static bool DeletePerson(int? PersonID)
        {
            bool Deleted = false;
            try
            {

                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_DeletePerson", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("@PersonID", PersonID);


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

        public static List<PersonDTO> GetAllPerson()
        {
            List<PersonDTO> person = new List<PersonDTO>();
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_GetAllPerson", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            person.Add(new PersonDTO
                              (
                                  reader.GetInt32(reader.GetOrdinal("PersonID")),
                                     reader.GetString(reader.GetOrdinal("Name")),
                                     reader.GetString(reader.GetOrdinal("Phone")),
                                     (!reader.IsDBNull(reader.GetOrdinal("Email")) ? reader.GetString("Email") : ""),
                                     reader.GetString(reader.GetOrdinal("Password")),
                                     reader.GetBoolean(reader.GetOrdinal("IsActive"))
                              ));
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                clsUtil.WriteExceptionInLogFile(ex);
            }

            return person;
        }

        public static PersonDTO FindPersonByEmailAndPassword(string Email, string Password)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_FindPersonByEmailAndPassword", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@Email", Email);
                    Command.Parameters.AddWithValue("@Password", Password);


                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new PersonDTO
                              (
                                     reader.GetInt32(reader.GetOrdinal("PersonID")),
                                     reader.GetString(reader.GetOrdinal("Name")),
                                     reader.GetString(reader.GetOrdinal("Phone")),
                                     (!reader.IsDBNull(reader.GetOrdinal("Email")) ? reader.GetString("Email") : ""),
                                     reader.GetString(reader.GetOrdinal("Password")),
                                     reader.GetBoolean(reader.GetOrdinal("IsActive"))
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

        public static bool UpdatePassword(string Email, string Password)
        {
            bool Updated = false;

            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_ChangePassword", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@Email", Email);
                    Command.Parameters.AddWithValue("@Password", Password);

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

    }
}
