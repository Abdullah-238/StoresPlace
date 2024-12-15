using StoresPlace_DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoresPlace_Business
{
    public class clsStoresForSale
    {
        public enum enMode { ADD = 1, UPDATE = 2 }
        public enMode Mode;

        public int? StoreForSaleID { get; set; }
        public int? StoreID { get; set; }
        public decimal? Price { get; set; }
        public byte? Status { get; set; }
        public StoresForSaleDTO SDTO
        {
            get { return (new StoresForSaleDTO(this.StoreForSaleID, this.StoreID, this.Price, this.Status)); }
        }
        public clsStoresForSale(StoresForSaleDTO SDTO, enMode cMode = enMode.ADD)
        {
            this.StoreForSaleID = SDTO.StoreForSaleID;
            this.StoreID = SDTO.StoreID;
            this.Price = SDTO.Price;
            this.Status = SDTO.Status;

            Mode = cMode; ;
        }

        private bool _AddNewStoresForSale()
        {
            this.StoreForSaleID = clsStoresForSaleData.AddNewStoresForSale(SDTO);

            return (this.StoreForSaleID != null);
        }

        private bool _UpdateStoresForSale()
        {
            return clsStoresForSaleData.UpdateStoresForSale(SDTO);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.ADD:
                    if (_AddNewStoresForSale())
                    {
                        this.Mode = enMode.UPDATE;
                        return true;
                    }
                    else
                        return false;

                case enMode.UPDATE:
                    return _UpdateStoresForSale();

            }
            return false;
        }

        public static clsStoresForSale Find(int? StoreForSaleID)
        {
            StoresForSaleDTO SDTO = clsStoresForSaleData.FindStoresForSale(StoreForSaleID);
            if (SDTO != null)
                return new clsStoresForSale(SDTO, enMode.UPDATE);
            else
                return null;
        }
        public static bool IsExist(int? StoreForSaleID)
        {
            return clsStoresForSaleData.IsStoresForSaleExists(StoreForSaleID);
        }

        public static bool Delete(int? StoreForSaleID)
        {

            return clsStoresForSaleData.DeleteStoresForSale(StoreForSaleID);
        }
        public static List<StoresForSaleDetailsDTO> GetAllStoresForSaleAr()
        {

            return clsStoresForSaleData.GetAllStoresForSaleAr();
        }

        public static List<StoresForSaleDetailsDTO> GetAllStoresForSaleEn()
        {

            return clsStoresForSaleData.GetAllStoresForSaleEn();
        }

    }
}
