using Microsoft.Data.SqlClient;
using System;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace StoresPlace_DataAccess
{
    public class StoreDTO
    {
        public int? StoreID { get; set; }
        public string Name { get; set; }
        public string? CommercialNumber { get; set; }
        public int? DistrictsID { get; set; }
        public string? Website { get; set; }
        public string? Address { get; set; }
        public int? CategoryID { get; set; }
        public int? TypeID { get; set; }
        public byte? Rating { get; set; }
        public decimal? NumberOfRates { get; set; }
        public byte? Status { get; set; }
        public decimal? NumbersOfClick { get; set; }
        public string? Photo { get; set; }
        public int? PeronID { get; set; }
        public string? Phone { get; set; }
        public int? CityID { get; set; }
        public string? Email { get; set; }

        public StoreDTO(int? storeid, string name, string? commercialnumber, int? districtsid, string? website, string? address, int? categoryid,
            int? typeid, byte? rating, decimal? numberofrates, byte? status, decimal? numbersofclick, string? photo, int? peronid,string? phone , int ?cityID ,string ?email)
        {
            this.StoreID = storeid;
            this.Name = name;
            this.CommercialNumber = commercialnumber;
            this.DistrictsID = districtsid;
            this.Website = website;
            this.Address = address;
            this.CategoryID = categoryid;
            this.TypeID = typeid;
            this.Rating = rating;
            this.NumberOfRates = numberofrates;
            this.Status = status;
            this.NumbersOfClick = numbersofclick;
            this.Photo = photo;
            this.PeronID = peronid;
            this.Phone = phone;
            this.CityID = cityID;
            this.Email = email;

        }


    }
     public class StoreDetailsDTO
    {
        public string Name { get; set; }
        public string? RegionName { get; set; }
        public string? CityName { get; set; }
        public string? DistrictsName { get; set; }
        public string? CommercialNumber { get; set; }
        public string? Website { get; set; }
        public string? Address { get; set; }
        public string? CategoryName { get; set; }
        public string? TypeName { get; set; }
        public byte? Rating { get; set; }
        public decimal? NumberOfRates { get; set; }
        public string? StoreStatus { get; set; }
        public decimal? NumbersOfClick { get; set; }
        public string? Photo { get; set; }
        public string? PersonName { get; set; }
        public int? StoreID { get; set; }

        public StoreDetailsDTO(string name, string? regionNameAr, string? cityNameAr, string? districtsNameAr, string? commercialNumber, string? website, string? address,
            string? categoryNameAr, string? typeNameAr, byte? rating, decimal? numberOfRates, string? storeStatus, decimal? numbersOfClick, string? photo, string? personName, int? storeid)
        {
            Name = name;
            RegionName = regionNameAr;
            CityName = cityNameAr;
            DistrictsName = districtsNameAr;
            CommercialNumber = commercialNumber;
            Website = website;
            Address = address;
            CategoryName = categoryNameAr;
            TypeName = typeNameAr;
            Rating = rating;
            NumberOfRates = numberOfRates;
            StoreStatus = storeStatus;
            NumbersOfClick = numbersOfClick;
            Photo = photo;
            PersonName = personName;
            StoreID = storeid;
        }
    }

    public class clsStoreData
    {
       
        public static Nullable<int> AddNewStore(StoreDTO store)
        {
            Nullable<int> StoreID = null;
            try
            {

                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_AddNewStore", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@Name", store.Name);
                    Command.Parameters.AddWithValue("@CommercialNumber", (store.CommercialNumber == null ? DBNull.Value : store.CommercialNumber));
                    Command.Parameters.AddWithValue("@DistrictsID", (store.DistrictsID == null ? DBNull.Value : store.DistrictsID));
                    Command.Parameters.AddWithValue("@Website", (store.Website == null ? DBNull.Value : store.Website));
                    Command.Parameters.AddWithValue("@Address", (store.Address == null ? DBNull.Value : store.Address));
                    Command.Parameters.AddWithValue("@CategoryID", store.CategoryID);
                    Command.Parameters.AddWithValue("@TypeID", store.TypeID);
                    Command.Parameters.AddWithValue("@Rating", (store.Rating == null ? DBNull.Value : store.Rating));
                    Command.Parameters.AddWithValue("@NumberOfRates", (store.NumberOfRates == null ? DBNull.Value : store.NumberOfRates));
                    Command.Parameters.AddWithValue("@Status", store.Status);
                    Command.Parameters.AddWithValue("@NumbersOfClick", (store.NumbersOfClick == null ? DBNull.Value : store.NumbersOfClick));
                    Command.Parameters.AddWithValue("@Photo", (store.Photo == null ? DBNull.Value : store.Photo));
                    Command.Parameters.AddWithValue("@Phone", (store.Phone == null ? DBNull.Value : store.Phone));
                    Command.Parameters.AddWithValue("@CityID", (store.CityID == null ? DBNull.Value : store.CityID));
                    Command.Parameters.AddWithValue("@Email", (store.Email == null ? DBNull.Value : store.Email));
                    Command.Parameters.AddWithValue("@PeronID", store.PeronID);

                    // Output parameter
                    SqlParameter outputParameter = new SqlParameter($"@NewStoreID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    Command.Parameters.Add(outputParameter);

                    Connection.Open();
                    Command.ExecuteScalar();

                    StoreID = (int)Command.Parameters[$"@NewStoreID"].Value;

                }
            }
            catch (Exception ex)
            {
                clsUtil.WriteExceptionInLogFile(ex);
            }

            return StoreID;
        }

        public static StoreDTO FindStore(int? StoreID)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_FindStore", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@StoreID", StoreID);


                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new StoreDTO
                              (
                                 reader.GetInt32(reader.GetOrdinal("StoreID")),
                                     reader.GetString(reader.GetOrdinal("Name")),
                                     (!reader.IsDBNull(reader.GetOrdinal("CommercialNumber")) ? reader.GetString("CommercialNumber") : null),
                                      (!reader.IsDBNull(reader.GetOrdinal("DistrictsID")) ? reader.GetInt32("DistrictsID") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("Website")) ? reader.GetString("Website") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("Address")) ? reader.GetString("Address") : null),
                                 reader.GetInt32(reader.GetOrdinal("CategoryID")),
                                 reader.GetInt32(reader.GetOrdinal("TypeID")),
                                        (!reader.IsDBNull(reader.GetOrdinal("Rating")) ? reader.GetByte("Rating") : null),
                                        (!reader.IsDBNull(reader.GetOrdinal("NumberOfRates")) ? reader.GetDecimal("NumberOfRates") : null),
                                     reader.GetByte(reader.GetOrdinal("Status")),
                                      (!reader.IsDBNull(reader.GetOrdinal("NumbersOfClick")) ? reader.GetDecimal("NumbersOfClick") : null),
                                    (!reader.IsDBNull(reader.GetOrdinal("Photo")) ? reader.GetString("Photo") : null),
                                 reader.GetInt32(reader.GetOrdinal("PeronID")),
                                    (!reader.IsDBNull(reader.GetOrdinal("Phone")) ? reader.GetString("Phone") : null),
                                    (!reader.IsDBNull(reader.GetOrdinal("CityID")) ? reader.GetInt32("CityID") : null),
                                    (!reader.IsDBNull(reader.GetOrdinal("Email")) ? reader.GetString("Email") : null)
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

        public static StoreDetailsDTO FindStoreAr(int? StoreID)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_FindStoreAr", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@StoreID", StoreID);


                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new StoreDetailsDTO
                              (
                                     reader.GetString(reader.GetOrdinal("Name")),
                                     (!reader.IsDBNull(reader.GetOrdinal("RegionName")) ? reader.GetString("RegionName") : null),
                                      (!reader.IsDBNull(reader.GetOrdinal("CityName")) ? reader.GetString("CityName") : null),
                                     (!reader.IsDBNull(reader.GetOrdinal("DistrictsName")) ? reader.GetString("DistrictsName") : null),
                                     (!reader.IsDBNull(reader.GetOrdinal("CommercialNumber")) ? reader.GetString("CommercialNumber") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("Website")) ? reader.GetString("Website") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("Address")) ? reader.GetString("Address") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("CategoryName")) ? reader.GetString("CategoryName") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("TypeName")) ? reader.GetString("TypeName") : null),
                                        (!reader.IsDBNull(reader.GetOrdinal("Rating")) ? reader.GetByte("Rating") : null),
                                        (!reader.IsDBNull(reader.GetOrdinal("NumberOfRates")) ? reader.GetDecimal("NumberOfRates") : null),
                                        (!reader.IsDBNull(reader.GetOrdinal("StoreStatus")) ? reader.GetString("StoreStatus") : null),
                                      (!reader.IsDBNull(reader.GetOrdinal("NumbersOfClick")) ? reader.GetDecimal("NumbersOfClick") : null),
                                    (!reader.IsDBNull(reader.GetOrdinal("Photo")) ? reader.GetString("Photo") : null),
                                                                           (!reader.IsDBNull(reader.GetOrdinal("PersonName")) ? reader.GetString("PersonName") : null),

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

        public static StoreDetailsDTO FindStoreEn(int? StoreID)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_FindStoreEn", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@StoreID", StoreID);


                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new StoreDetailsDTO
                              (
                                  reader.GetString(reader.GetOrdinal("Name")),
                                     (!reader.IsDBNull(reader.GetOrdinal("RegionName")) ? reader.GetString("RegionName") : null),
                                      (!reader.IsDBNull(reader.GetOrdinal("CityName")) ? reader.GetString("CityName") : null),
                                     (!reader.IsDBNull(reader.GetOrdinal("DistrictsName")) ? reader.GetString("DistrictsName") : null),
                                     (!reader.IsDBNull(reader.GetOrdinal("CommercialNumber")) ? reader.GetString("CommercialNumber") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("Website")) ? reader.GetString("Website") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("Address")) ? reader.GetString("Address") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("CategoryName")) ? reader.GetString("CategoryName") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("TypeName")) ? reader.GetString("TypeName") : null),
                                        (!reader.IsDBNull(reader.GetOrdinal("Rating")) ? reader.GetByte("Rating") : null),
                                        (!reader.IsDBNull(reader.GetOrdinal("NumberOfRates")) ? reader.GetDecimal("NumberOfRates") : null),
                                        (!reader.IsDBNull(reader.GetOrdinal("StoreStatus")) ? reader.GetString("StoreStatus") : null),
                                      (!reader.IsDBNull(reader.GetOrdinal("NumbersOfClick")) ? reader.GetDecimal("NumbersOfClick") : null),
                                    (!reader.IsDBNull(reader.GetOrdinal("Photo")) ? reader.GetString("Photo") : null),
                                                                           (!reader.IsDBNull(reader.GetOrdinal("PersonName")) ? reader.GetString("PersonName") : null),

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

        public static bool UpdateStore(StoreDTO store)
        {
            bool Updated = false;

            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_UpdateStore", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@StoreID", store.StoreID);
                    Command.Parameters.AddWithValue("@Name", store.Name);
                    Command.Parameters.AddWithValue("@CommercialNumber", (store.CommercialNumber == null ? DBNull.Value : store.CommercialNumber));
                    Command.Parameters.AddWithValue("@DistrictsID", (store.DistrictsID == null ? DBNull.Value : store.DistrictsID));
                    Command.Parameters.AddWithValue("@Website", (store.Website == null ? DBNull.Value : store.Website));
                    Command.Parameters.AddWithValue("@Address", (store.Address == null ? DBNull.Value : store.Address));
                    Command.Parameters.AddWithValue("@CategoryID", store.CategoryID);
                    Command.Parameters.AddWithValue("@TypeID", store.TypeID);
                    Command.Parameters.AddWithValue("@Rating", (store.Rating == null ? DBNull.Value : store.Rating));
                    Command.Parameters.AddWithValue("@NumberOfRates", (store.NumberOfRates == null ? DBNull.Value : store.NumberOfRates));
                    Command.Parameters.AddWithValue("@Status", store.Status);
                    Command.Parameters.AddWithValue("@NumbersOfClick", (store.NumbersOfClick == null ? DBNull.Value : store.NumbersOfClick));
                    Command.Parameters.AddWithValue("@Photo", (store.Photo == null ? DBNull.Value : store.Photo));
                    Command.Parameters.AddWithValue("@Phone", (store.Phone == null ? DBNull.Value : store.Phone));
                    Command.Parameters.AddWithValue("@CityID", (store.CityID == null ? DBNull.Value : store.CityID));
                    Command.Parameters.AddWithValue("@Email", (store.Email == null ? DBNull.Value : store.Email));
                    Command.Parameters.AddWithValue("@PeronID", store.PeronID);

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

        public static bool IsStoreExists(int? StoreID)
        {

            bool isFound = false;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_IsStoreExists", Connection))
                {
                    Connection.Open();
                    Command.CommandType = CommandType.StoredProcedure;

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

        public static bool DeleteStore(int? StoreID)
        {
            bool Deleted = false;
            try
            {

                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_DeleteStore", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("@StoreID", StoreID);


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

        public static List<StoreDTO> GetAllStore()
        {
            List<StoreDTO> store = new List<StoreDTO>();
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_GetAllStore", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            store.Add(new StoreDTO
                              (
                                  reader.GetInt32(reader.GetOrdinal("StoreID")),
                                     reader.GetString(reader.GetOrdinal("Name")),
                                     (!reader.IsDBNull(reader.GetOrdinal("CommercialNumber")) ? reader.GetString("CommercialNumber") : null),
                                      (!reader.IsDBNull(reader.GetOrdinal("DistrictsID")) ? reader.GetInt32("DistrictsID") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("Website")) ? reader.GetString("Website") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("Address")) ? reader.GetString("Address") : null),
                                 reader.GetInt32(reader.GetOrdinal("CategoryID")),
                                 reader.GetInt32(reader.GetOrdinal("TypeID")),
                                        (!reader.IsDBNull(reader.GetOrdinal("Rating")) ? reader.GetByte("Rating") : null),
                                        (!reader.IsDBNull(reader.GetOrdinal("NumberOfRates")) ? reader.GetDecimal("NumberOfRates") : null),
                                     reader.GetByte(reader.GetOrdinal("Status")),
                                      (!reader.IsDBNull(reader.GetOrdinal("NumbersOfClick")) ? reader.GetDecimal("NumbersOfClick") : null),
                                    (!reader.IsDBNull(reader.GetOrdinal("Photo")) ? reader.GetString("Photo") : null),
                                 reader.GetInt32(reader.GetOrdinal("PeronID")),
                                    (!reader.IsDBNull(reader.GetOrdinal("Phone")) ? reader.GetString("Phone") : null),
                                    (!reader.IsDBNull(reader.GetOrdinal("CityID")) ? reader.GetInt32("CityID") : null),
                                    (!reader.IsDBNull(reader.GetOrdinal("Email")) ? reader.GetString("Email") : null)
                              ));
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                clsUtil.WriteExceptionInLogFile(ex);
            }

            return store;
        }

        public static List<StoreDTO> GetAllStoresByPersonID(int? PersonID)
        {
            List<StoreDTO> store = new List<StoreDTO>();
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_GetAllStoresByPersonID", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@PersonID", PersonID);

                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            store.Add(new StoreDTO
                              (
                                  reader.GetInt32(reader.GetOrdinal("StoreID")),
                                     reader.GetString(reader.GetOrdinal("Name")),
                                     (!reader.IsDBNull(reader.GetOrdinal("CommercialNumber")) ? reader.GetString("CommercialNumber") : null),
                                      (!reader.IsDBNull(reader.GetOrdinal("DistrictsID")) ? reader.GetInt32("DistrictsID") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("Website")) ? reader.GetString("Website") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("Address")) ? reader.GetString("Address") : null),
                                 reader.GetInt32(reader.GetOrdinal("CategoryID")),
                                 reader.GetInt32(reader.GetOrdinal("TypeID")),
                                        (!reader.IsDBNull(reader.GetOrdinal("Rating")) ? reader.GetByte("Rating") : null),
                                        (!reader.IsDBNull(reader.GetOrdinal("NumberOfRates")) ? reader.GetDecimal("NumberOfRates") : null),
                                     reader.GetByte(reader.GetOrdinal("Status")),
                                      (!reader.IsDBNull(reader.GetOrdinal("NumbersOfClick")) ? reader.GetDecimal("NumbersOfClick") : null),
                                    (!reader.IsDBNull(reader.GetOrdinal("Photo")) ? reader.GetString("Photo") : null),
                                    reader.GetInt32(reader.GetOrdinal("PeronID")),
                                    (!reader.IsDBNull(reader.GetOrdinal("Phone")) ? reader.GetString("Phone") : null),
                                    (!reader.IsDBNull(reader.GetOrdinal("CityID")) ? reader.GetInt32("CityID") : null),
                                    (!reader.IsDBNull(reader.GetOrdinal("Email")) ? reader.GetString("Email") : null)
                              ));
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                clsUtil.WriteExceptionInLogFile(ex);
            }

            return store;
        }

        public static List<StoreDetailsDTO> GetAllStoreByCategoryID(int? CategoryID)
        {
            List<StoreDetailsDTO> store = new List<StoreDetailsDTO>();
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("Sp_GetCategoryByCategoryID", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@CategoryID", CategoryID);

                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            store.Add(new StoreDetailsDTO
                              (
                                      reader.GetString(reader.GetOrdinal("Name")),
                                     (!reader.IsDBNull(reader.GetOrdinal("RegionName")) ? reader.GetString("RegionName") : null),
                                      (!reader.IsDBNull(reader.GetOrdinal("CityName")) ? reader.GetString("CityName") : null),
                                     (!reader.IsDBNull(reader.GetOrdinal("DistrictsName")) ? reader.GetString("DistrictsName") : null),
                                     (!reader.IsDBNull(reader.GetOrdinal("CommercialNumber")) ? reader.GetString("CommercialNumber") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("Website")) ? reader.GetString("Website") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("Address")) ? reader.GetString("Address") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("CategoryName")) ? reader.GetString("CategoryName") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("TypeName")) ? reader.GetString("TypeName") : null),
                                        (!reader.IsDBNull(reader.GetOrdinal("Rating")) ? reader.GetByte("Rating") : null),
                                        (!reader.IsDBNull(reader.GetOrdinal("NumberOfRates")) ? reader.GetDecimal("NumberOfRates") : null),
                                        (!reader.IsDBNull(reader.GetOrdinal("StoreStatus")) ? reader.GetString("StoreStatus") : null),
                                      (!reader.IsDBNull(reader.GetOrdinal("NumbersOfClick")) ? reader.GetDecimal("NumbersOfClick") : null),
                                                                          (!reader.IsDBNull(reader.GetOrdinal("Photo")) ? reader.GetString("Photo") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("PersonName")) ? reader.GetString("PersonName") : null),
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

            return store;
        }



        public static List<StoreDetailsDTO> GetAllStoreByCategoryNameAr(string CategoryNameAr,int ? TypeID)
        {
            List<StoreDetailsDTO> store = new List<StoreDetailsDTO>();
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_GetAllStoresByCategoryNameAr", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@CategoryNameAr", CategoryNameAr);
                    Command.Parameters.AddWithValue("@TypeID", TypeID);

                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            store.Add(new StoreDetailsDTO
                              (
                                     reader.GetString(reader.GetOrdinal("Name")),
                                     (!reader.IsDBNull(reader.GetOrdinal("RegionName")) ? reader.GetString("RegionName") : null),
                                      (!reader.IsDBNull(reader.GetOrdinal("CityName")) ? reader.GetString("CityName") : null),
                                     (!reader.IsDBNull(reader.GetOrdinal("DistrictsName")) ? reader.GetString("DistrictsName") : null),
                                     (!reader.IsDBNull(reader.GetOrdinal("CommercialNumber")) ? reader.GetString("CommercialNumber") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("Website")) ? reader.GetString("Website") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("Address")) ? reader.GetString("Address") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("CategoryName")) ? reader.GetString("CategoryName") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("TypeName")) ? reader.GetString("TypeName") : null),
                                        (!reader.IsDBNull(reader.GetOrdinal("Rating")) ? reader.GetByte("Rating") : null),
                                        (!reader.IsDBNull(reader.GetOrdinal("NumberOfRates")) ? reader.GetDecimal("NumberOfRates") : null),
                                        (!reader.IsDBNull(reader.GetOrdinal("StoreStatus")) ? reader.GetString("StoreStatus") : null),
                                      (!reader.IsDBNull(reader.GetOrdinal("NumbersOfClick")) ? reader.GetDecimal("NumbersOfClick") : null),
                                    (!reader.IsDBNull(reader.GetOrdinal("Photo")) ? reader.GetString("Photo") : null),
                                                                           (!reader.IsDBNull(reader.GetOrdinal("PersonName")) ? reader.GetString("PersonName") : null),

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

            return store;
        }

        public static List<StoreDetailsDTO> GetAllStoreByCategoryNameEn(string CategoryNameEn, int? TypeID)
        {
            List<StoreDetailsDTO> store = new List<StoreDetailsDTO>();
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_GetAllStoresByCategoryNameEn", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@CategoryNameEn", CategoryNameEn);
                    Command.Parameters.AddWithValue("@TypeID", TypeID);

                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            store.Add(new StoreDetailsDTO
                              (
                                     reader.GetString(reader.GetOrdinal("Name")),
                                     (!reader.IsDBNull(reader.GetOrdinal("RegionName")) ? reader.GetString("RegionName") : null),
                                      (!reader.IsDBNull(reader.GetOrdinal("CityName")) ? reader.GetString("CityName") : null),
                                     (!reader.IsDBNull(reader.GetOrdinal("DistrictsName")) ? reader.GetString("DistrictsName") : null),
                                     (!reader.IsDBNull(reader.GetOrdinal("CommercialNumber")) ? reader.GetString("CommercialNumber") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("Website")) ? reader.GetString("Website") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("Address")) ? reader.GetString("Address") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("CategoryName")) ? reader.GetString("CategoryName") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("TypeName")) ? reader.GetString("TypeName") : null),
                                        (!reader.IsDBNull(reader.GetOrdinal("Rating")) ? reader.GetByte("Rating") : null),
                                        (!reader.IsDBNull(reader.GetOrdinal("NumberOfRates")) ? reader.GetDecimal("NumberOfRates") : null),
                                        (!reader.IsDBNull(reader.GetOrdinal("StoreStatus")) ? reader.GetString("StoreStatus") : null),
                                      (!reader.IsDBNull(reader.GetOrdinal("NumbersOfClick")) ? reader.GetDecimal("NumbersOfClick") : null),
                                    (!reader.IsDBNull(reader.GetOrdinal("Photo")) ? reader.GetString("Photo") : null),
                                                                           (!reader.IsDBNull(reader.GetOrdinal("PersonName")) ? reader.GetString("PersonName") : null),
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

            return store;
        }

        public static List<StoreDetailsDTO> GetAllStoreByCategoryNameEnAndRegionName(string CategoryNameEn,string RegionNameEn, int? TypeID)
        {
            List<StoreDetailsDTO> store = new List<StoreDetailsDTO>();
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("Sp_GetAllStoreByCategoryNameEnAndRegionNameEn", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@CategoryNameEn", CategoryNameEn);
                    Command.Parameters.AddWithValue("@RegionNameEn", RegionNameEn);
                    Command.Parameters.AddWithValue("@TypeID", TypeID);

                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            store.Add(new StoreDetailsDTO
                               (
                                      reader.GetString(reader.GetOrdinal("Name")),
                                     (!reader.IsDBNull(reader.GetOrdinal("RegionName")) ? reader.GetString("RegionName") : null),
                                      (!reader.IsDBNull(reader.GetOrdinal("CityName")) ? reader.GetString("CityName") : null),
                                     (!reader.IsDBNull(reader.GetOrdinal("DistrictsName")) ? reader.GetString("DistrictsName") : null),
                                     (!reader.IsDBNull(reader.GetOrdinal("CommercialNumber")) ? reader.GetString("CommercialNumber") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("Website")) ? reader.GetString("Website") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("Address")) ? reader.GetString("Address") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("CategoryName")) ? reader.GetString("CategoryName") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("TypeName")) ? reader.GetString("TypeName") : null),
                                        (!reader.IsDBNull(reader.GetOrdinal("Rating")) ? reader.GetByte("Rating") : null),
                                        (!reader.IsDBNull(reader.GetOrdinal("NumberOfRates")) ? reader.GetDecimal("NumberOfRates") : null),
                                        (!reader.IsDBNull(reader.GetOrdinal("StoreStatus")) ? reader.GetString("StoreStatus") : null),
                                      (!reader.IsDBNull(reader.GetOrdinal("NumbersOfClick")) ? reader.GetDecimal("NumbersOfClick") : null),
                                    (!reader.IsDBNull(reader.GetOrdinal("Photo")) ? reader.GetString("Photo") : null),
                                                                           (!reader.IsDBNull(reader.GetOrdinal("PersonName")) ? reader.GetString("PersonName") : null),
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

            return store;
        }

        public static List<StoreDetailsDTO> GetAllStoreByCategoryNameArAndRegionName(string CategoryNameAr, string RegionNameAr, int? TypeID)
        {
            List<StoreDetailsDTO> store = new List<StoreDetailsDTO>();
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("Sp_GetAllStoreByCategoryNameArAndRegionNameAr", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@CategoryNameAr", CategoryNameAr);
                    Command.Parameters.AddWithValue("@RegionNameAr", RegionNameAr);
                    Command.Parameters.AddWithValue("@TypeID", TypeID);

                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            store.Add(new StoreDetailsDTO
                               (
                                     reader.GetString(reader.GetOrdinal("Name")),
                                     (!reader.IsDBNull(reader.GetOrdinal("RegionName")) ? reader.GetString("RegionName") : null),
                                      (!reader.IsDBNull(reader.GetOrdinal("CityName")) ? reader.GetString("CityName") : null),
                                     (!reader.IsDBNull(reader.GetOrdinal("DistrictsName")) ? reader.GetString("DistrictsName") : null),
                                     (!reader.IsDBNull(reader.GetOrdinal("CommercialNumber")) ? reader.GetString("CommercialNumber") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("Website")) ? reader.GetString("Website") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("Address")) ? reader.GetString("Address") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("CategoryName")) ? reader.GetString("CategoryName") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("TypeName")) ? reader.GetString("TypeName") : null),
                                        (!reader.IsDBNull(reader.GetOrdinal("Rating")) ? reader.GetByte("Rating") : null),
                                        (!reader.IsDBNull(reader.GetOrdinal("NumberOfRates")) ? reader.GetDecimal("NumberOfRates") : null),
                                        (!reader.IsDBNull(reader.GetOrdinal("StoreStatus")) ? reader.GetString("StoreStatus") : null),
                                      (!reader.IsDBNull(reader.GetOrdinal("NumbersOfClick")) ? reader.GetDecimal("NumbersOfClick") : null),
                                    (!reader.IsDBNull(reader.GetOrdinal("Photo")) ? reader.GetString("Photo") : null),
                                                                           (!reader.IsDBNull(reader.GetOrdinal("PersonName")) ? reader.GetString("PersonName") : null),
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

            return store;
        }

        public static List<StoreDetailsDTO> GetAllStoreByCategoryNameEnAndCityNameEn(string CategoryNameEn, string CityNameEn, int? TypeID)
        {
            List<StoreDetailsDTO> store = new List<StoreDetailsDTO>();
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("Sp_GetAllStoreByCategoryNameEnAndCityNameEn", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@CategoryNameEn", CategoryNameEn);
                    Command.Parameters.AddWithValue("@CityNameEn", CityNameEn);
                    Command.Parameters.AddWithValue("@TypeID", TypeID);

                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            store.Add(new StoreDetailsDTO
                              (
                                    reader.GetString(reader.GetOrdinal("Name")),
                                     (!reader.IsDBNull(reader.GetOrdinal("RegionName")) ? reader.GetString("RegionName") : null),
                                      (!reader.IsDBNull(reader.GetOrdinal("CityName")) ? reader.GetString("CityName") : null),
                                     (!reader.IsDBNull(reader.GetOrdinal("DistrictsName")) ? reader.GetString("DistrictsName") : null),
                                     (!reader.IsDBNull(reader.GetOrdinal("CommercialNumber")) ? reader.GetString("CommercialNumber") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("Website")) ? reader.GetString("Website") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("Address")) ? reader.GetString("Address") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("CategoryName")) ? reader.GetString("CategoryName") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("TypeName")) ? reader.GetString("TypeName") : null),
                                        (!reader.IsDBNull(reader.GetOrdinal("Rating")) ? reader.GetByte("Rating") : null),
                                        (!reader.IsDBNull(reader.GetOrdinal("NumberOfRates")) ? reader.GetDecimal("NumberOfRates") : null),
                                        (!reader.IsDBNull(reader.GetOrdinal("StoreStatus")) ? reader.GetString("StoreStatus") : null),
                                      (!reader.IsDBNull(reader.GetOrdinal("NumbersOfClick")) ? reader.GetDecimal("NumbersOfClick") : null),
                                    (!reader.IsDBNull(reader.GetOrdinal("Photo")) ? reader.GetString("Photo") : null),
                                                                           (!reader.IsDBNull(reader.GetOrdinal("PersonName")) ? reader.GetString("PersonName") : null),

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

            return store;
        }

        public static List<StoreDetailsDTO> GetAllStoreByCategoryNameArAndCityNameAr(string CategoryNameAr, string CityNameAr, int? TypeID)
        {
            List<StoreDetailsDTO> store = new List<StoreDetailsDTO>();
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("Sp_GetAllStoreByCategoryNameArAndCityNameAr", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@CategoryNameAr", CategoryNameAr);
                    Command.Parameters.AddWithValue("@CityNameAr", CityNameAr);
                    Command.Parameters.AddWithValue("@TypeID", TypeID);

                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            store.Add(new StoreDetailsDTO
                              (
                                    reader.GetString(reader.GetOrdinal("Name")),
                                     (!reader.IsDBNull(reader.GetOrdinal("RegionName")) ? reader.GetString("RegionName") : null),
                                      (!reader.IsDBNull(reader.GetOrdinal("CityName")) ? reader.GetString("CityName") : null),
                                     (!reader.IsDBNull(reader.GetOrdinal("DistrictsName")) ? reader.GetString("DistrictsName") : null),
                                     (!reader.IsDBNull(reader.GetOrdinal("CommercialNumber")) ? reader.GetString("CommercialNumber") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("Website")) ? reader.GetString("Website") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("Address")) ? reader.GetString("Address") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("CategoryName")) ? reader.GetString("CategoryName") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("TypeName")) ? reader.GetString("TypeName") : null),
                                        (!reader.IsDBNull(reader.GetOrdinal("Rating")) ? reader.GetByte("Rating") : null),
                                        (!reader.IsDBNull(reader.GetOrdinal("NumberOfRates")) ? reader.GetDecimal("NumberOfRates") : null),
                                        (!reader.IsDBNull(reader.GetOrdinal("StoreStatus")) ? reader.GetString("StoreStatus") : null),
                                      (!reader.IsDBNull(reader.GetOrdinal("NumbersOfClick")) ? reader.GetDecimal("NumbersOfClick") : null),
                                    (!reader.IsDBNull(reader.GetOrdinal("Photo")) ? reader.GetString("Photo") : null),
                                                                           (!reader.IsDBNull(reader.GetOrdinal("PersonName")) ? reader.GetString("PersonName") : null),

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

            return store;
        }

        public static List<StoreDetailsDTO> GetAllStoreByCategoryNameEnAndDistrictsNameEn(string CategoryNameEn, string DistrictsNameEn, int? TypeID)
        {
            List<StoreDetailsDTO> store = new List<StoreDetailsDTO>();
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("Sp_GetAllStoreByCategoryNameEnAndDistrictsNameEn", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@CategoryNameEn", CategoryNameEn);
                    Command.Parameters.AddWithValue("@DistrictsNameEn", DistrictsNameEn);
                    Command.Parameters.AddWithValue("@TypeID", TypeID);

                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            store.Add(new StoreDetailsDTO
                              (
                                    reader.GetString(reader.GetOrdinal("Name")),
                                     (!reader.IsDBNull(reader.GetOrdinal("RegionName")) ? reader.GetString("RegionName") : null),
                                      (!reader.IsDBNull(reader.GetOrdinal("CityName")) ? reader.GetString("CityName") : null),
                                     (!reader.IsDBNull(reader.GetOrdinal("DistrictsName")) ? reader.GetString("DistrictsName") : null),
                                     (!reader.IsDBNull(reader.GetOrdinal("CommercialNumber")) ? reader.GetString("CommercialNumber") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("Website")) ? reader.GetString("Website") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("Address")) ? reader.GetString("Address") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("CategoryName")) ? reader.GetString("CategoryName") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("TypeName")) ? reader.GetString("TypeName") : null),
                                        (!reader.IsDBNull(reader.GetOrdinal("Rating")) ? reader.GetByte("Rating") : null),
                                        (!reader.IsDBNull(reader.GetOrdinal("NumberOfRates")) ? reader.GetDecimal("NumberOfRates") : null),
                                        (!reader.IsDBNull(reader.GetOrdinal("StoreStatus")) ? reader.GetString("StoreStatus") : null),
                                      (!reader.IsDBNull(reader.GetOrdinal("NumbersOfClick")) ? reader.GetDecimal("NumbersOfClick") : null),
                                    (!reader.IsDBNull(reader.GetOrdinal("Photo")) ? reader.GetString("Photo") : null),
                                                                           (!reader.IsDBNull(reader.GetOrdinal("PersonName")) ? reader.GetString("PersonName") : null),

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

            return store;
        }

        public static List<StoreDetailsDTO> GetAllStoreByCategoryNameArAndDistrictsNameAr(string CategoryNameAr, string DistrictsNameAr, int? TypeID)
        {
            List<StoreDetailsDTO> store = new List<StoreDetailsDTO>();
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("Sp_GetAllStoreByCategoryNameArAndDistrictsNameAr", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@CategoryNameAr", CategoryNameAr);
                    Command.Parameters.AddWithValue("@DistrictsNameAr", DistrictsNameAr);
                    Command.Parameters.AddWithValue("@TypeID", TypeID);

                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            store.Add(new StoreDetailsDTO
                               (
                                      reader.GetString(reader.GetOrdinal("Name")),
                                     (!reader.IsDBNull(reader.GetOrdinal("RegionName")) ? reader.GetString("RegionName") : null),
                                      (!reader.IsDBNull(reader.GetOrdinal("CityName")) ? reader.GetString("CityName") : null),
                                     (!reader.IsDBNull(reader.GetOrdinal("DistrictsName")) ? reader.GetString("DistrictsName") : null),
                                     (!reader.IsDBNull(reader.GetOrdinal("CommercialNumber")) ? reader.GetString("CommercialNumber") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("Website")) ? reader.GetString("Website") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("Address")) ? reader.GetString("Address") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("CategoryName")) ? reader.GetString("CategoryName") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("TypeName")) ? reader.GetString("TypeName") : null),
                                        (!reader.IsDBNull(reader.GetOrdinal("Rating")) ? reader.GetByte("Rating") : null),
                                        (!reader.IsDBNull(reader.GetOrdinal("NumberOfRates")) ? reader.GetDecimal("NumberOfRates") : null),
                                        (!reader.IsDBNull(reader.GetOrdinal("StoreStatus")) ? reader.GetString("StoreStatus") : null),
                                      (!reader.IsDBNull(reader.GetOrdinal("NumbersOfClick")) ? reader.GetDecimal("NumbersOfClick") : null),
                                    (!reader.IsDBNull(reader.GetOrdinal("Photo")) ? reader.GetString("Photo") : null),
                                                                           (!reader.IsDBNull(reader.GetOrdinal("PersonName")) ? reader.GetString("PersonName") : null),
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

            return store;
        }




        public static List<StoreDetailsDTO> GetAllStoresInDetailsAr()
        {
            List<StoreDetailsDTO> store = new List<StoreDetailsDTO>();
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("Sp_GetAllStoresInDetailsAR", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            store.Add(new StoreDetailsDTO
                              (
                                     reader.GetString(reader.GetOrdinal("Name")),
                                     (!reader.IsDBNull(reader.GetOrdinal("RegionName")) ? reader.GetString("RegionName") : null),
                                      (!reader.IsDBNull(reader.GetOrdinal("CityName")) ? reader.GetString("CityName") : null),
                                     (!reader.IsDBNull(reader.GetOrdinal("DistrictsName")) ? reader.GetString("DistrictsName") : null),
                                     (!reader.IsDBNull(reader.GetOrdinal("CommercialNumber")) ? reader.GetString("CommercialNumber") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("Website")) ? reader.GetString("Website") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("Address")) ? reader.GetString("Address") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("CategoryName")) ? reader.GetString("CategoryName") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("TypeName")) ? reader.GetString("TypeName") : null),
                                        (!reader.IsDBNull(reader.GetOrdinal("Rating")) ? reader.GetByte("Rating") : null),
                                        (!reader.IsDBNull(reader.GetOrdinal("NumberOfRates")) ? reader.GetDecimal("NumberOfRates") : null),
                                        (!reader.IsDBNull(reader.GetOrdinal("StoreStatus")) ? reader.GetString("StoreStatus") : null),
                                      (!reader.IsDBNull(reader.GetOrdinal("NumbersOfClick")) ? reader.GetDecimal("NumbersOfClick") : null),
                                    (!reader.IsDBNull(reader.GetOrdinal("Photo")) ? reader.GetString("Photo") : null),
                                                                           (!reader.IsDBNull(reader.GetOrdinal("PersonName")) ? reader.GetString("PersonName") : null),

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

            return store;
        }

        public static List<StoreDetailsDTO> GetAllStoresInDetailsByPersonIDAr (int? PersonID)
        {
            List<StoreDetailsDTO> store = new List<StoreDetailsDTO>();
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("Sp_GetAllStoresInDetailsByPersonIDAr", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@PersonID", PersonID);

                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            store.Add(new StoreDetailsDTO
                              (
                                      reader.GetString(reader.GetOrdinal("Name")),
                                     (!reader.IsDBNull(reader.GetOrdinal("RegionName")) ? reader.GetString("RegionName") : null),
                                      (!reader.IsDBNull(reader.GetOrdinal("CityName")) ? reader.GetString("CityName") : null),
                                     (!reader.IsDBNull(reader.GetOrdinal("DistrictsName")) ? reader.GetString("DistrictsName") : null),
                                     (!reader.IsDBNull(reader.GetOrdinal("CommercialNumber")) ? reader.GetString("CommercialNumber") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("Website")) ? reader.GetString("Website") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("Address")) ? reader.GetString("Address") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("CategoryName")) ? reader.GetString("CategoryName") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("TypeName")) ? reader.GetString("TypeName") : null),
                                        (!reader.IsDBNull(reader.GetOrdinal("Rating")) ? reader.GetByte("Rating") : null),
                                        (!reader.IsDBNull(reader.GetOrdinal("NumberOfRates")) ? reader.GetDecimal("NumberOfRates") : null),
                                        (!reader.IsDBNull(reader.GetOrdinal("StoreStatus")) ? reader.GetString("StoreStatus") : null),
                                      (!reader.IsDBNull(reader.GetOrdinal("NumbersOfClick")) ? reader.GetDecimal("NumbersOfClick") : null),
                                    (!reader.IsDBNull(reader.GetOrdinal("Photo")) ? reader.GetString("Photo") : null),
                                      (!reader.IsDBNull(reader.GetOrdinal("PersonName")) ? reader.GetString("PersonName") : null),
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

            return store;
        }

        public static List<StoreDetailsDTO> GetAllStoresInDetailsEn()
        {
            List<StoreDetailsDTO> store = new List<StoreDetailsDTO>();
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("Sp_GetAllStoresInDetailsEn", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            store.Add(new StoreDetailsDTO
                              (
                                   reader.GetString(reader.GetOrdinal("Name")),
                                     (!reader.IsDBNull(reader.GetOrdinal("RegionName")) ? reader.GetString("RegionName") : null),
                                      (!reader.IsDBNull(reader.GetOrdinal("CityName")) ? reader.GetString("CityName") : null),
                                     (!reader.IsDBNull(reader.GetOrdinal("DistrictsName")) ? reader.GetString("DistrictsName") : null),
                                     (!reader.IsDBNull(reader.GetOrdinal("CommercialNumber")) ? reader.GetString("CommercialNumber") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("Website")) ? reader.GetString("Website") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("Address")) ? reader.GetString("Address") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("CategoryName")) ? reader.GetString("CategoryName") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("TypeName")) ? reader.GetString("TypeName") : null),
                                        (!reader.IsDBNull(reader.GetOrdinal("Rating")) ? reader.GetByte("Rating") : null),
                                        (!reader.IsDBNull(reader.GetOrdinal("NumberOfRates")) ? reader.GetDecimal("NumberOfRates") : null),
                                        (!reader.IsDBNull(reader.GetOrdinal("StoreStatus")) ? reader.GetString("StoreStatus") : null),
                                      (!reader.IsDBNull(reader.GetOrdinal("NumbersOfClick")) ? reader.GetDecimal("NumbersOfClick") : null),
                                    (!reader.IsDBNull(reader.GetOrdinal("Photo")) ? reader.GetString("Photo") : null),
                                                                           (!reader.IsDBNull(reader.GetOrdinal("PersonName")) ? reader.GetString("PersonName") : null),

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

            return store;
        }

        public static List<StoreDetailsDTO> GetAllStoresInDetailsByPersonIDEn(int? PersonID)
        {
            List<StoreDetailsDTO> store = new List<StoreDetailsDTO>();
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("Sp_GetAllStoresInDetailsByPersonIDEn", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@PersonID", PersonID);

                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            store.Add(new StoreDetailsDTO
                              (
                                    reader.GetString(reader.GetOrdinal("Name")),
                                     (!reader.IsDBNull(reader.GetOrdinal("RegionName")) ? reader.GetString("RegionName") : null),
                                      (!reader.IsDBNull(reader.GetOrdinal("CityName")) ? reader.GetString("CityName") : null),
                                     (!reader.IsDBNull(reader.GetOrdinal("DistrictsName")) ? reader.GetString("DistrictsName") : null),
                                     (!reader.IsDBNull(reader.GetOrdinal("CommercialNumber")) ? reader.GetString("CommercialNumber") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("Website")) ? reader.GetString("Website") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("Address")) ? reader.GetString("Address") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("CategoryName")) ? reader.GetString("CategoryName") : null),
                                       (!reader.IsDBNull(reader.GetOrdinal("TypeName")) ? reader.GetString("TypeName") : null),
                                        (!reader.IsDBNull(reader.GetOrdinal("Rating")) ? reader.GetByte("Rating") : null),
                                        (!reader.IsDBNull(reader.GetOrdinal("NumberOfRates")) ? reader.GetDecimal("NumberOfRates") : null),
                                        (!reader.IsDBNull(reader.GetOrdinal("StoreStatus")) ? reader.GetString("StoreStatus") : null),
                                      (!reader.IsDBNull(reader.GetOrdinal("NumbersOfClick")) ? reader.GetDecimal("NumbersOfClick") : null),
                                    (!reader.IsDBNull(reader.GetOrdinal("Photo")) ? reader.GetString("Photo") : null),
                                    (!reader.IsDBNull(reader.GetOrdinal("PersonName")) ? reader.GetString("PersonName") : null),
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

            return store;
        }


        public static bool UpdateStoreRating(byte ? Rate, int ? StoreID)
        {
            bool Updated = false;

            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_UpdateStoreRating", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;


                    Command.Parameters.AddWithValue("@Rate", Rate);
                    Command.Parameters.AddWithValue("@StoreID", StoreID);

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

        public static bool UpdateStoreRating(byte? Rate,byte ? OldRate ,  int? StoreID)
        {
            bool Updated = false;

            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_UpdateOldStoreRating", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;


                    Command.Parameters.AddWithValue("@Rate", Rate);
                    Command.Parameters.AddWithValue("@OldRate", OldRate);
                    Command.Parameters.AddWithValue("@StoreID", StoreID);

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
