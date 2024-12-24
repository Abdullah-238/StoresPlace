using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoresPlace_DataAccess
{
    public class StoresForSaleDTO
    {
        public int? StoreForSaleID { get; set; }
        public int? StoreID { get; set; }
        public decimal? Price { get; set; }
        public byte? Status { get; set; }

        public StoresForSaleDTO(int? storeforsaleid, int? storeid, decimal? price, byte? status)
        {
            this.StoreForSaleID = storeforsaleid;
            this.StoreID = storeid;
            this.Price = price;
            this.Status = status;

        }
    }

    public class StoresForSaleDetailsDTO
    {
        public string Name { get; set; }
        public string RegionName { get; set; }
        public string CityName { get; set; }
        public string DistrictsName { get; set; }
        public string CommercialNumber { get; set; }
        public string Website { get; set; }
        public string Address { get; set; }
        public string CategoryName { get; set; }
        public string TypeName { get; set; }
        public byte? Rating { get; set; }
        public decimal? NumberOfRates { get; set; }
        public string StoreStatus { get; set; }
        public decimal? NumbersOfClick { get; set; }
        public string Photo { get; set; }
        public string PersonName { get; set; }
        public int? StoreID { get; set; }

        public decimal? Price { get; set; }

        public string StoresForSaleStatus { get; set; }

        public StoresForSaleDetailsDTO(string name, string regionNameAr, string cityNameAr, string districtsNameAr, string commercialNumber, string website, string address,
            string categoryNameAr, string typeNameAr, byte? rating, decimal? numberOfRates, string storeStatus, decimal? numbersOfClick, string photo, string personName,
            int? storeid,decimal? price ,string storesForsalestatus)
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
            Price = price;
            StoresForSaleStatus = storesForsalestatus;
        }
    }


    public class clsStoresForSaleData
    {
      
        public static Nullable<int> AddNewStoresForSale(StoresForSaleDTO storesforsal)
        {
            Nullable<int> StoreForSaleID = null;
            try
            {

                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_AddNewStoresForSale", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@StoreID", storesforsal.StoreID);
                    Command.Parameters.AddWithValue("@Price", (storesforsal.Price == null ? DBNull.Value : storesforsal.Price));
                    Command.Parameters.AddWithValue("@Status", storesforsal.Status);


                    // Output parameter
                    SqlParameter outputParameter = new SqlParameter($"@NewStoreForSaleID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    Command.Parameters.Add(outputParameter);

                    Connection.Open();
                    Command.ExecuteScalar();

                    StoreForSaleID = (int)Command.Parameters[$"@NewStoreForSaleID"].Value;

                }
            }
            catch (Exception ex)
            {
                clsUtil.WriteExceptionInLogFile(ex);
            }

            return StoreForSaleID;
        }

        public static StoresForSaleDTO FindStoresForSale(int? @StoreID)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_FindStoresForSale", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@StoreID", @StoreID);


                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new StoresForSaleDTO
                              (
                                 reader.GetInt32(reader.GetOrdinal("StoreForSaleID")),
                                 reader.GetInt32(reader.GetOrdinal("StoreID")),
                                 (!reader.IsDBNull(reader.GetOrdinal("Price")) ? reader.GetDecimal("Price") : null),
                                 reader.GetByte(reader.GetOrdinal("Status"))
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

        public static bool UpdateStoresForSale(StoresForSaleDTO storesforsal)
        {
            bool Updated = false;

            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_UpdateStoresForSale", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@StoreForSaleID", storesforsal.StoreForSaleID);
                    Command.Parameters.AddWithValue("@StoreID", storesforsal.StoreID);
                    Command.Parameters.AddWithValue("@Price", (storesforsal.Price == null ? DBNull.Value : storesforsal.Price));
                    Command.Parameters.AddWithValue("@Status", storesforsal.Status);

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

        public static bool IsStoresForSaleExists(int? StoreID)
        {

            bool isFound = false;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_IsStoresForSaleExists", Connection))
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

        public static bool DeleteStoresForSale(int? StoreForSaleID)
        {
            bool Deleted = false;
            try
            {

                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_DeleteStoresForSale", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("@StoreForSaleID", StoreForSaleID);


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

        public static List<StoresForSaleDetailsDTO> GetAllStoresForSaleAr()
        {
            List<StoresForSaleDetailsDTO> storesforsal = new List<StoresForSaleDetailsDTO>();
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_GetAllStoresForSale", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            storesforsal.Add(new StoresForSaleDetailsDTO
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
                                    (!reader.IsDBNull(reader.GetOrdinal("Photo")) ? reader.GetString("Photo") : ""),
                                     (!reader.IsDBNull(reader.GetOrdinal("PersonName")) ? reader.GetString("PersonName") : ""),
                                 reader.GetInt32(reader.GetOrdinal("StoreID")),
                                 (!reader.IsDBNull(reader.GetOrdinal("Price")) ? reader.GetDecimal("Price") : null),
                                   reader.GetString(reader.GetOrdinal("StoresForSaleStatus"))



                              ));
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                clsUtil.WriteExceptionInLogFile(ex);
            }

            return storesforsal;
        }


        public static List<StoresForSaleDetailsDTO> GetAllStoresForSaleEn()
        {
            List<StoresForSaleDetailsDTO> storesforsal = new List<StoresForSaleDetailsDTO>();
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_GetAllStoresForSaleEn", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            storesforsal.Add(new StoresForSaleDetailsDTO
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
                                    (!reader.IsDBNull(reader.GetOrdinal("Photo")) ? reader.GetString("Photo") : ""),
                                   (!reader.IsDBNull(reader.GetOrdinal("PersonName")) ? reader.GetString("PersonName") : ""),
                                 reader.GetInt32(reader.GetOrdinal("StoreID")),
                                 (!reader.IsDBNull(reader.GetOrdinal("Price")) ? reader.GetDecimal("Price") : null),
                                   reader.GetString(reader.GetOrdinal("StoresForSaleStatus"))



                              ));
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                clsUtil.WriteExceptionInLogFile(ex);
            }

            return storesforsal;
        }

    }
}
