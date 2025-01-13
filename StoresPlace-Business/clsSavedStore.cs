using StoresPlace_DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoresPlace_Business
{
    public class clsSavedStore
    {
        public enum enMode { ADD = 1, UPDATE = 2 }
        public enMode Mode;

        public int? StoreSavedId { get; set; }
        public int? StoreID { get; set; }
        public int? PersonID { get; set; }
        public SavedStoreDTO SDTO
        {
            get { return (new SavedStoreDTO(this.StoreSavedId, this.StoreID, this.PersonID)); }
        }
        public clsSavedStore(SavedStoreDTO SDTO, enMode cMode = enMode.ADD)
        {
            this.StoreSavedId = SDTO.StoreSavedId;
            this.StoreID = SDTO.StoreID;
            this.PersonID = SDTO.PersonID;

            Mode = cMode; ;
        }

        private bool _AddNewSavedStore()
        {
            this.StoreSavedId = clsSavedStoreData.AddNewSavedStore(SDTO);

            return (this.StoreSavedId != null);
        }

        private bool _UpdateSavedStore()
        {
            return clsSavedStoreData.UpdateSavedStore(SDTO);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.ADD:
                    if (_AddNewSavedStore())
                    {
                        this.Mode = enMode.UPDATE;
                        return true;
                    }
                    else
                        return false;

                case enMode.UPDATE:
                    return _UpdateSavedStore();

            }
            return false;
        }

        public static clsSavedStore Find(int? StoreSavedId)
        {
            SavedStoreDTO SDTO = clsSavedStoreData.FindSavedStore(StoreSavedId);
            if (SDTO != null)
                return new clsSavedStore(SDTO, enMode.UPDATE);
            else
                return null;
        }
        public static bool IsExist(int? StoreSavedId)
        {
            return clsSavedStoreData.IsExist(StoreSavedId);
        }

        public static bool Delete(int? StoreSavedId)
        {

            return clsSavedStoreData.DeleteSavedStore(StoreSavedId);
        }

        public static bool DeleteSavedStoreByStoreID(int? StoreSavedId, int? PersonID)
        {
            return clsSavedStoreData.DeleteSavedStoreByStoreID(StoreSavedId, PersonID);
        }
      

        public static List<StoreDetailsDTO> GetAllSavedStoreByPersonIDAr(int ?PersonID, int? PageNumber)
        {
            return clsSavedStoreData.GetAllSavedStoreByPersonIDAr(PersonID,PageNumber);
        }

        public static List<StoreDetailsDTO> GetAllSavedStoreByPersonIDEn(int? PersonID ,int? PageNumber)
        {
            return clsSavedStoreData.GetAllSavedStoreByPersonIDEn(PersonID, PageNumber);
        }
        public static bool IsSavedStoreExists(int? StoreID, int? PersonID)
        {
            return clsSavedStoreData.IsSavedStoreExists(StoreID,PersonID);
        }


    }
}
