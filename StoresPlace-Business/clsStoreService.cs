using StoresPlace_DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StoresPlace_DataAccess.clsStoreServiceData;

namespace StoresPlace_Business
{
    public class clsStoreService
    {
        public enum enMode { ADD = 1, UPDATE = 2 }
        public enMode Mode;

        public int? StoreServiceIID { get; set; }
        public int? ServiceID { get; set; }
        public int? StoreID { get; set; }
        public StoreServiceDTO SDTO
        {
            get { return (new StoreServiceDTO(this.StoreServiceIID, this.ServiceID, this.StoreID)); }
        }
        public clsStoreService(StoreServiceDTO SDTO, enMode cMode = enMode.ADD)
        {
            this.StoreServiceIID = SDTO.StoreServiceIID;
            this.ServiceID = SDTO.ServiceID;
            this.StoreID = SDTO.StoreID;

            Mode = cMode; ;
        }

        private bool _AddNewStoreService()
        {
            this.StoreServiceIID = clsStoreServiceData.AddNewStoreService(SDTO);

            return (this.StoreServiceIID != null);
        }

        private bool _UpdateStoreService()
        {
            return clsStoreServiceData.UpdateStoreService(SDTO);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.ADD:
                    if (_AddNewStoreService())
                    {
                        this.Mode = enMode.UPDATE;
                        return true;
                    }
                    else
                        return false;

                case enMode.UPDATE:
                    return _UpdateStoreService();

            }
            return false;
        }

        public static clsStoreService Find(int? StoreServiceIID)
        {
            StoreServiceDTO SDTO = clsStoreServiceData.FindStoreService(StoreServiceIID);
            if (SDTO != null)
                return new clsStoreService(SDTO, enMode.UPDATE);
            else
                return null;
        }
        public static bool IsExist(int? StoreServiceIID)
        {
            return clsStoreServiceData.IsStoreServiceExists(StoreServiceIID);
        }

        public static bool Delete(int? StoreServiceIID)
        {

            return clsStoreServiceData.DeleteStoreService(StoreServiceIID);
        }
        public static List<StoreServiceDTO> GetAll()
        {

            return clsStoreServiceData.GetAllStoreService();
        }

    }
}
