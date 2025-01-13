using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoresPlace_DataAccess
{
    public class CouponDTO
    {
        public int? CouponID { get; set; }
        public string Coupon { get; set; }
        public int? StoreID { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsActive { get; set; }

        public CouponDTO(int? couponid, string coupon, int? storeid, DateTime expirydate, bool isactive)
        {
            this.CouponID = couponid;
            this.Coupon = coupon;
            this.StoreID = storeid;
            this.ExpiryDate = expirydate;
            this.IsActive = isactive;

        }
    }

    public class clsCouponData
    {  
       public static Nullable<int> AddNewCoupon(CouponDTO coupon)
        {
            Nullable<int> CouponID = null;
            try
            {

                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_AddNewCoupon", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@Coupon", coupon.Coupon);
                    Command.Parameters.AddWithValue("@StoreID", coupon.StoreID);
                    Command.Parameters.AddWithValue("@ExpiryDate", coupon.ExpiryDate);
                    Command.Parameters.AddWithValue("@IsActive", coupon.IsActive);

                    // Output parameter
                    SqlParameter outputParameter = new SqlParameter($"@NewCouponID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    Command.Parameters.Add(outputParameter);

                    Connection.Open();
                    Command.ExecuteScalar();

                    CouponID = (int)Command.Parameters[$"@NewCouponID"].Value;

                }
            }
            catch (Exception ex)
            {
                clsUtil.WriteExceptionInLogFile(ex);
            }

            return CouponID;
        }

        public static CouponDTO FindCoupon(int? CouponID)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_FindCoupon", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@CouponID", CouponID);


                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new CouponDTO
                              (
                                 reader.GetInt32(reader.GetOrdinal("CouponID")),
                                     reader.GetString(reader.GetOrdinal("Coupon")),
                                 reader.GetInt32(reader.GetOrdinal("StoreID")),
                                     reader.GetDateTime(reader.GetOrdinal("ExpiryDate")),
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

        public static bool UpdateCoupon(CouponDTO coupon)
        {
            bool Updated = false;

            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_UpdateCoupon", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@CouponID", coupon.CouponID);
                    Command.Parameters.AddWithValue("@Coupon", coupon.Coupon);
                    Command.Parameters.AddWithValue("@StoreID", coupon.StoreID);
                    Command.Parameters.AddWithValue("@ExpiryDate", coupon.ExpiryDate);
                    Command.Parameters.AddWithValue("@IsActive", coupon.IsActive);

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

        public static bool IsCouponExists(int? CouponID)
        {

            bool isFound = false;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_IsCouponExists", Connection))
                {
                    Connection.Open();
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@CouponID", CouponID);

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

        public static bool DeleteCoupon(int? CouponID)
        {
            bool Deleted = false;
            try
            {

                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_DeleteCoupon", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("@CouponID", CouponID);


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

        public static List<CouponDTO> GetAllCoupon()
        {
            List<CouponDTO> coupon = new List<CouponDTO>();
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_GetAllCoupon", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            coupon.Add(new CouponDTO
                              (
                                 reader.GetInt32(reader.GetOrdinal("CouponID")),
                                     reader.GetString(reader.GetOrdinal("Coupon")),
                                 reader.GetInt32(reader.GetOrdinal("StoreID")),
                                     reader.GetDateTime(reader.GetOrdinal("ExpiryDate")),
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

            return coupon;
        }

        public static List<string> GetAllCouponByStoreID(int ?StoreID)
        {
            List<string> coupons = new List<string>();
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_GetAllByStoreID", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@StoreID", StoreID);

                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            coupons.Add(reader.GetString(reader.GetOrdinal("Coupon")));
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                clsUtil.WriteExceptionInLogFile(ex);
            }

            return coupons;
        }

        public static List<CouponDTO> GetAllCoupon(int? StoreID)
        {
            List<CouponDTO> coupon = new List<CouponDTO>();
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_GetAllCouponInDetilesByStoreID", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@StoreID", StoreID);

                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            coupon.Add(new CouponDTO
                              (
                                 reader.GetInt32(reader.GetOrdinal("CouponID")),
                                     reader.GetString(reader.GetOrdinal("Coupon")),
                                 reader.GetInt32(reader.GetOrdinal("StoreID")),
                                     reader.GetDateTime(reader.GetOrdinal("ExpiryDate")),
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

            return coupon;
        }


    }
}
