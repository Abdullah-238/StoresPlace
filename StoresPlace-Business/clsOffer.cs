using StoresPlace_DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoresPlace_Business
{
    public class clsOffer
    {
        public enum enMode { ADD = 1, UPDATE = 2 }
        public enMode Mode;

        public int? OfferID { get; set; }
        public string Offer { get; set; }
        public int? StoreID { get; set; }
        public OfferDTO ODTO
        {
            get { return (new OfferDTO(this.OfferID, this.Offer, this.StoreID)); }
        }
        public clsOffer(OfferDTO ODTO, enMode cMode = enMode.ADD)
        {
            this.OfferID = ODTO.OfferID;
            this.Offer = ODTO.Offer;
            this.StoreID = ODTO.StoreID;

            Mode = cMode; ;
        }

        private bool _AddNewOffer()
        {
            this.OfferID = clsOfferData.AddNewOffer(ODTO);

            return (this.OfferID != null);
        }

        private bool _UpdateOffer()
        {
            return clsOfferData.UpdateOffer(ODTO);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.ADD:
                    if (_AddNewOffer())
                    {
                        this.Mode = enMode.UPDATE;
                        return true;
                    }
                    else
                        return false;

                case enMode.UPDATE:
                    return _UpdateOffer();

            }
            return false;
        }

        public static clsOffer Find(int? OfferID)
        {
            OfferDTO ODTO = clsOfferData.FindOffer(OfferID);
            if (ODTO != null)
                return new clsOffer(ODTO, enMode.UPDATE);
            else
                return null;
        }
        public static bool IsExist(int? OfferID)
        {
            return clsOfferData.IsOfferExists(OfferID);
        }

        public static bool Delete(int? OfferID)
        {

            return clsOfferData.DeleteOffer(OfferID);
        }
        public static List<OfferDTO> GetAll()
        {

            return clsOfferData.GetAllOffer();
        }

        public static List<string> GetAllOfferByStoreID(int ? StoreID)
        {
            return clsOfferData.GetAllOfferByStoreID(StoreID);
        }
    }
}
