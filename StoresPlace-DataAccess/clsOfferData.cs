using Microsoft.Data.SqlClient;

using System.Data;


namespace StoresPlace_DataAccess
{
    public class OfferDTO
    {
        public int? OfferID { get; set; }
        public string Offer { get; set; }
        public int? StoreID { get; set; }

        public OfferDTO(int? offerid, string offer, int? storeid)
        {
            this.OfferID = offerid;
            this.Offer = offer;
            this.StoreID = storeid;

        }
    }

    public class clsOfferData
    {
       
        public static Nullable<int> AddNewOffer(OfferDTO offer)
        {
            Nullable<int> OfferID = null;
            try
            {

                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_AddNewOffer", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@Offer", offer.Offer);
                    Command.Parameters.AddWithValue("@StoreID", offer.StoreID);

                    // Output parameter
                    SqlParameter outputParameter = new SqlParameter($"@NewOfferID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    Command.Parameters.Add(outputParameter);

                    Connection.Open();
                    Command.ExecuteScalar();

                    OfferID = (int)Command.Parameters[$"@NewOfferID"].Value;

                }
            }
            catch (Exception ex)
            {
                clsUtil.WriteExceptionInLogFile(ex);
            }

            return OfferID;
        }

        public static OfferDTO FindOffer(int? OfferID)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_FindOffer", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@OfferID", OfferID);


                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new OfferDTO
                              (
                                 reader.GetInt32(reader.GetOrdinal("OfferID")),
                                     reader.GetString(reader.GetOrdinal("Offer")),
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

        public static bool UpdateOffer(OfferDTO offer)
        {
            bool Updated = false;

            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_UpdateOffer", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@OfferID", offer.OfferID);
                    Command.Parameters.AddWithValue("@Offer", offer.Offer);
                    Command.Parameters.AddWithValue("@StoreID", offer.StoreID);

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

        public static bool IsOfferExists(int? OfferID)
        {

            bool isFound = false;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_IsOfferExists", Connection))
                {
                    Connection.Open();
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@OfferID", OfferID);

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

        public static bool DeleteOffer(int? OfferID)
        {
            bool Deleted = false;
            try
            {

                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    Connection.Open();
                    using (SqlCommand Command = new SqlCommand("SP_DeleteOffer", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("@OfferID", OfferID);


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

        public static List<OfferDTO> GetAllOffer()
        {
            List<OfferDTO> offer = new List<OfferDTO>();
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_GetAllOffer", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            offer.Add(new OfferDTO
                              (
                                 reader.GetInt32(reader.GetOrdinal("OfferID")),
                                     reader.GetString(reader.GetOrdinal("Offer")),
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

            return offer;
        }

        public static List<string> GetAllOfferByStoreID(int ?StoreID)
        {
            List<string> offer = new List<string>();
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_GetAllOffersByStoreIDID", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@StoreID", StoreID);

                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            offer.Add( reader.GetString(reader.GetOrdinal("Offer")));
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                clsUtil.WriteExceptionInLogFile(ex);
            }

            return offer;
        }



    }
}
