using StoresPlace_DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoresPlace_Business
{
    public class clsStore
    {
        public enum enMode { ADD = 1, UPDATE = 2 }
        public enMode Mode;

        public int? StoreID { get; set; }
        public string Name { get; set; }
        public string CommercialNumber { get; set; }
        public int? DistrictsID { get; set; }
        public string Website { get; set; }
        public string Address { get; set; }
        public int? CategoryID { get; set; }
        public int? TypeID { get; set; }
        public byte? Rating { get; set; }
        public decimal? NumberOfRates { get; set; }
        public byte? Status { get; set; }
        public decimal? NumbersOfClick { get; set; }
        public string Photo { get; set; }
        public int? PeronID { get; set; }

        public enum enStoresType
        {
            eLocal = 1 , eOnline = 2 , eProductiveFamilies = 3
        }

        public enum enStatus
        {
            eActive = 1, ePending = 2, eNotActive = 3
        }
        public StoreDTO SDTO
        {
            get { return (new StoreDTO(this.StoreID, this.Name, this.CommercialNumber, this.DistrictsID, this.Website, this.Address, this.CategoryID, this.TypeID, this.Rating, this.NumberOfRates, this.Status, this.NumbersOfClick, this.Photo, this.PeronID)); }
        }
        public clsStore(StoreDTO SDTO, enMode cMode = enMode.ADD)
        {
            this.StoreID = SDTO.StoreID;
            this.Name = SDTO.Name;
            this.CommercialNumber = SDTO.CommercialNumber;
            this.DistrictsID = SDTO.DistrictsID;
            this.Website = SDTO.Website;
            this.Address = SDTO.Address;
            this.CategoryID = SDTO.CategoryID;
            this.TypeID = SDTO.TypeID;
            this.Rating = SDTO.Rating;
            this.NumberOfRates = SDTO.NumberOfRates;
            this.Status = SDTO.Status;
            this.NumbersOfClick = SDTO.NumbersOfClick;
            this.Photo = SDTO.Photo;
            this.PeronID = SDTO.PeronID;

            Mode = cMode; ;
        }

        public clsStore()
        {
            this.StoreID = null;
            this.Name = "";
            this.CommercialNumber = "";
            this.DistrictsID = null;
            this.Website = "";
            this.Address = "";
            this.CategoryID = null;
            this.TypeID = null;
            this.Rating = null;
            this.NumberOfRates = null;
            this.Status = null;
            this.NumbersOfClick = null;
            this.Photo = "";
            this.PeronID = null;

            Mode = enMode.ADD; ;
        }

        private bool _AddNewStore()
        {
            this.StoreID = clsStoreData.AddNewStore(SDTO);

            return (this.StoreID != null);
        }

        private bool _UpdateStore()
        {
            return clsStoreData.UpdateStore(SDTO);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.ADD:
                    if (_AddNewStore())
                    {
                        this.Mode = enMode.UPDATE;
                        return true;
                    }
                    else
                        return false;

                case enMode.UPDATE:
                    return _UpdateStore();

            }
            return false;
        }

        public static clsStore Find(int? StoreID)
        {
            StoreDTO SDTO = clsStoreData.FindStore(StoreID);
            if (SDTO != null)
                return new clsStore(SDTO, enMode.UPDATE);
            else
                return null;
        }

        public static StoreDetailsDTO FindStoreAr(int? StoreID)
        {
            return clsStoreData.FindStoreAr(StoreID);
        }

        public static StoreDetailsDTO FindStoreEn(int? StoreID)
        {
            return clsStoreData.FindStoreEn(StoreID);
        }
        public static bool IsExist(int? StoreID)
        {
            return clsStoreData.IsStoreExists(StoreID);
        }

        public static bool Delete(int? StoreID)
        {

            return clsStoreData.DeleteStore(StoreID);
        }
        public static List<StoreDTO> GetAll()
        {

            return clsStoreData.GetAllStore();
        }

        public static List<StoreDetailsDTO> GetAllStoreByCategoryID(int? CategoryID)
        {
            return clsStoreData.GetAllStoreByCategoryID(CategoryID);
        }

        public static List<StoreDetailsDTO> GetAllStoreByCategoryNameAr(string CategoryNameAr, int? TypeID)
        {
            return clsStoreData.GetAllStoreByCategoryNameAr(CategoryNameAr, TypeID);
        }

        public static List<StoreDetailsDTO> GetAllStoreByCategoryNameEn(string CategoryNameEn, int? TypeID)
        {
            return clsStoreData.GetAllStoreByCategoryNameEn(CategoryNameEn, TypeID);
        }

        public static List<StoreDetailsDTO> GetAllStoreByCategoryNameArAndRegionName(string CategoryNameAr, string RegionNameAr, int? TypeID)
        {
            return clsStoreData.GetAllStoreByCategoryNameArAndRegionName(CategoryNameAr, RegionNameAr, TypeID);
        }

        public static List<StoreDetailsDTO> GetAllStoreByCategoryNameEnAndRegionName(string CategoryNameEn, string RegionNameEn, int? TypeID)
        {
            return clsStoreData.GetAllStoreByCategoryNameEnAndRegionName(CategoryNameEn, RegionNameEn, TypeID);
        }

        public static List<StoreDetailsDTO> GetAllStoreByCategoryNameEnAndCityNameEn(string CategoryNameEn, string CityNameEn, int? TypeID)
        {
            return clsStoreData.GetAllStoreByCategoryNameEnAndCityNameEn(CategoryNameEn , CityNameEn, TypeID);
        }

        public static List<StoreDetailsDTO> GetAllStoreByCategoryNameArAndCityNameAr(string CategoryNameAr, string CityNameAr, int? TypeID)
        {
            return clsStoreData.GetAllStoreByCategoryNameArAndCityNameAr(CategoryNameAr, CityNameAr, TypeID);
        }

        public static List<StoreDetailsDTO> GetAllStoreByCategoryNameArAndDistrictsNameAr(string CategoryNameAr, string DistrictsNameAr, int? TypeID)
        {
            return clsStoreData.GetAllStoreByCategoryNameArAndDistrictsNameAr(CategoryNameAr , DistrictsNameAr, TypeID);
        }

        public static List<StoreDetailsDTO> GetAllStoreByCategoryNameEnAndDistrictsNameEn(string CategoryNameEn, string DistrictsNameEn, int? TypeID)
        {
            return clsStoreData.GetAllStoreByCategoryNameEnAndDistrictsNameEn(CategoryNameEn, DistrictsNameEn, TypeID);
        }

        public static List<StoreDTO> GetAllStoresByPersonID(int? PersonID)
        {
            return clsStoreData.GetAllStoresByPersonID(PersonID);
        }

        public static List<StoreDetailsDTO> GetAllStoresInDetailsAr()
        {
            return clsStoreData.GetAllStoresInDetailsAr();
        }


        public static List<StoreDetailsDTO> GetAllStoresInDetailsByPersonIDAr(int? PersonID)
        {
            return clsStoreData.GetAllStoresInDetailsByPersonIDAr(PersonID);
        }

        public static List<StoreDetailsDTO> GetAllStoresInDetailsEn()
        {
            return clsStoreData.GetAllStoresInDetailsEn();
        }


        public static List<StoreDetailsDTO> GetAllStoresInDetailsByPersonIDEn(int? PersonID)
        {
            return clsStoreData.GetAllStoresInDetailsByPersonIDEn(PersonID);
        }


        public static bool UpdateStoreRating(byte? Rate, int? StoreID)
        {
            return clsStoreData.UpdateStoreRating(Rate,StoreID);
        }

        public static bool UpdateStoreRating(byte? Rate, byte? OldRate, int? StoreID)
        {
            return clsStoreData.UpdateStoreRating(Rate, OldRate, StoreID);
        }


    }
}
