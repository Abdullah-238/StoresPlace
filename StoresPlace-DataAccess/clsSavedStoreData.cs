using Microsoft.Data.SqlClient;
using System.Data;

namespace StoresPlace_DataAccess
{
    public class SavedStoreDTO
    {
        public int? StoreSavedId { get; set; }
        public int? StoreID { get; set; }
        public int? PersonID { get; set; }

        public SavedStoreDTO(int? storesavedid, int? storeid, int? personid)
        {
            this.StoreSavedId = storesavedid;
            this.StoreID = storeid;
            this.PersonID = personid;

        }
    }

    public class clsSavedStoreData
    {

        public static Nullable<int> AddNewSavedStore(SavedStoreDTO savedstore)
        {
            Nullable<int> StoreSavedId = null;
            try
            {

                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_AddNewSavedStore", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@StoreID", savedstore.StoreID);
                    Command.Parameters.AddWithValue("@PersonID", savedstore.PersonID);

                    // Output parameter
                    SqlParameter outputParameter = new SqlParameter($"@NewStoreSavedId", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    Command.Parameters.Add(outputParameter);

                    Connection.Open();
                    Command.ExecuteScalar();

                    StoreSavedId = (int)Command.Parameters[$"@NewStoreSavedId"].Value;

                }
            }
            catch (Exception ex)
            {
                clsUtil.WriteExceptionInLogFile(ex);
            }

            return StoreSavedId;
        }

        public static SavedStoreDTO FindSavedStore(int? StoreSavedId)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_FindSavedStore", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@StoreSavedId", StoreSavedId);


                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new SavedStoreDTO
                              (
                                 reader.GetInt32(reader.GetOrdinal("StoreSavedId")),
                                 reader.GetInt32(reader.GetOrdinal("StoreID")),
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

        public static bool UpdateSavedStore(SavedStoreDTO savedstore)
        {
            bool Updated = false;

            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_UpdateSavedStore", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@StoreSavedId", savedstore.StoreSavedId);
                    Command.Parameters.AddWithValue("@StoreID", savedstore.StoreID);
                    Command.Parameters.AddWithValue("@PersonID", savedstore.PersonID);

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

        public static bool IsExist(int? StoreSavedId)
        {

            bool isFound = false;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_IsSavedStoreExists", Connection))
                {
                    Connection.Open();
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@StoreSavedId", StoreSavedId);

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

        public static bool DeleteSavedStore(int? StoreSavedId)
        {
            bool Deleted = false;
            try
            {

                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_DeleteSavedStore", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("@StoreSavedId", StoreSavedId);


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
        public static bool DeleteSavedStoreByStoreID(int? StoreID,int ? PersonID)
        {
            bool Deleted = false;
            try
            {

                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_DeleteSavedStoreByStoreID", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("@StoreID", StoreID);
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

        public static List<StoreDetailsDTO> GetAllSavedStoreByPersonIDAr(int? PersonID, int? PageNumber)
        {
            List<StoreDetailsDTO> savedstore = new List<StoreDetailsDTO>();
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_GetAllSavedStoreByPersonIDAr", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@PersonID", PersonID);
                    Command.Parameters.AddWithValue("@PageNumber", PageNumber);

                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            savedstore.Add(new StoreDetailsDTO
                             (
                                   reader.GetString(reader.GetOrdinal("Name")),
                                    (!reader.IsDBNull(reader.GetOrdinal("RegionName")) ? reader.GetString("RegionName") : ""),
                                     (!reader.IsDBNull(reader.GetOrdinal("CityName")) ? reader.GetString("CityName") : ""),
                                    (!reader.IsDBNull(reader.GetOrdinal("DistrictsName")) ? reader.GetString("DistrictsName") : ""),
                                    (!reader.IsDBNull(reader.GetOrdinal("CommercialNumber")) ? reader.GetString("CommercialNumber") : ""),
                                      (!reader.IsDBNull(reader.GetOrdinal("Website")) ? reader.GetString("Website") : ""),
                                      (!reader.IsDBNull(reader.GetOrdinal("Address")) ? reader.GetString("Address") : ""),
                                      (!reader.IsDBNull(reader.GetOrdinal("CategoryName")) ? reader.GetString("CategoryName") : ""),
                                      (!reader.IsDBNull(reader.GetOrdinal("TypeName")) ? reader.GetString("TypeName") : ""),
                                       (!reader.IsDBNull(reader.GetOrdinal("Rating")) ? reader.GetByte("Rating") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("NumberOfRates")) ? reader.GetDecimal("NumberOfRates") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("StoreStatus")) ? reader.GetString("StoreStatus") : ""),
                                     (!reader.IsDBNull(reader.GetOrdinal("NumbersOfClick")) ? reader.GetDecimal("NumbersOfClick") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("Photo")) ? reader.GetString("Photo") : null),
                                      (!reader.IsDBNull(reader.GetOrdinal("PersonName")) ? reader.GetString("PersonName") : ""),
                                       (!reader.IsDBNull(reader.GetOrdinal("Email")) ? reader.GetString("Email") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("Phone")) ? reader.GetString("Phone") : null),
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

            return savedstore;
        }

        public static List<StoreDetailsDTO> GetAllSavedStoreByPersonIDEn(int? PersonID, int? PageNumber)
        {
            List<StoreDetailsDTO> savedstore = new List<StoreDetailsDTO>();
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_GetAllSavedStoreByPersonIDEn", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@PersonID", PersonID);
                    Command.Parameters.AddWithValue("@PageNumber", PageNumber);

                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            savedstore.Add(new StoreDetailsDTO
                             (
                                   reader.GetString(reader.GetOrdinal("Name")),
                                    (!reader.IsDBNull(reader.GetOrdinal("RegionName")) ? reader.GetString("RegionName") : ""),
                                     (!reader.IsDBNull(reader.GetOrdinal("CityName")) ? reader.GetString("CityName") : ""),
                                    (!reader.IsDBNull(reader.GetOrdinal("DistrictsName")) ? reader.GetString("DistrictsName") : ""),
                                    (!reader.IsDBNull(reader.GetOrdinal("CommercialNumber")) ? reader.GetString("CommercialNumber") : ""),
                                      (!reader.IsDBNull(reader.GetOrdinal("Website")) ? reader.GetString("Website") : ""),
                                      (!reader.IsDBNull(reader.GetOrdinal("Address")) ? reader.GetString("Address") : ""),
                                      (!reader.IsDBNull(reader.GetOrdinal("CategoryName")) ? reader.GetString("CategoryName") : ""),
                                      (!reader.IsDBNull(reader.GetOrdinal("TypeName")) ? reader.GetString("TypeName") : ""),
                                       (!reader.IsDBNull(reader.GetOrdinal("Rating")) ? reader.GetByte("Rating") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("NumberOfRates")) ? reader.GetDecimal("NumberOfRates") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("StoreStatus")) ? reader.GetString("StoreStatus") : ""),
                                     (!reader.IsDBNull(reader.GetOrdinal("NumbersOfClick")) ? reader.GetDecimal("NumbersOfClick") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("Photo")) ? reader.GetString("Photo") : null),
                                      (!reader.IsDBNull(reader.GetOrdinal("PersonName")) ? reader.GetString("PersonName") : ""),
                                       (!reader.IsDBNull(reader.GetOrdinal("Email")) ? reader.GetString("Email") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("Phone")) ? reader.GetString("Phone") : null),
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

            return savedstore;
        }

        public static bool IsSavedStoreExists(int? StoreID, int? PersonID)
        {

            bool isFound = false;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_IsStoreSavedByPersonID", Connection))
                {
                    Connection.Open();
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@StoreID", StoreID);
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

    }

}
