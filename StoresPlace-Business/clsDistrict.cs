using StoresPlace_DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StoresPlace_DataAccess.clsDistrictData;

namespace StoresPlace_Business
{
    public class clsDistrict
    {
        public enum enMode { ADD = 1, UPDATE = 2 }
        public enMode Mode;

        public int? DistrictsID { get; set; }
        public int? CityID { get; set; }
        public string DistrictsNameAr { get; set; }
        public string DistrictsNameEn { get; set; }
        public DistrictDTO DDTO
        {
            get { return (new DistrictDTO(this.DistrictsID, this.CityID, this.DistrictsNameAr, this.DistrictsNameEn)); }
        }
        public clsDistrict(DistrictDTO DDTO, enMode cMode = enMode.ADD)
        {
            this.DistrictsID = DDTO.DistrictsID;
            this.CityID = DDTO.CityID;
            this.DistrictsNameAr = DDTO.DistrictsNameAr;
            this.DistrictsNameEn = DDTO.DistrictsNameEn;
            Mode = cMode; ;
        }

        private bool _AddNewDistrict()
        {
            this.DistrictsID = clsDistrictData.AddNewDistrict(DDTO);

            return (this.DistrictsID != null);
        }

        private bool _UpdateDistrict()
        {
            return clsDistrictData.UpdateDistrict(DDTO);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.ADD:
                    if (_AddNewDistrict())
                    {
                        this.Mode = enMode.UPDATE;
                        return true;
                    }
                    else
                        return false;

                case enMode.UPDATE:
                    return _UpdateDistrict();

            }
            return false;
        }

        public static clsDistrict Find(int? DistrictsID)
        {
            DistrictDTO DDTO = clsDistrictData.FindDistrict(DistrictsID);
            if (DDTO != null)
                return new clsDistrict(DDTO, enMode.UPDATE);
            else
                return null;
        }

        public static clsDistrict FindDistrictsNameAr(string DistrictsNameAr)
        {
            DistrictDTO DDTO = clsDistrictData.FindDistrictsNameAr(DistrictsNameAr);
            if (DDTO != null)
                return new clsDistrict(DDTO, enMode.UPDATE);
            else
                return null;
        }

        public static clsDistrict FindDistrictsNameEn(string DistrictsNameEn)
        {
            DistrictDTO DDTO = clsDistrictData.FindDistrictsNameEn(DistrictsNameEn);
            if (DDTO != null)
                return new clsDistrict(DDTO, enMode.UPDATE);
            else
                return null;
        }
        public static bool IsExist(int? DistrictsID)
        {
            return clsDistrictData.IsDistrictExists(DistrictsID);
        }

        public static bool Delete(int? DistrictsID)
        {

            return clsDistrictData.DeleteDistrict(DistrictsID);
        }
        public static List<DistrictDTO> GetAll()
        {

            return clsDistrictData.GetAllDistrict();
        }

        //public static List<DistrictDTO> GetDistrictsByCity(int cityId)
        //{
        //    // Replace with actual logic to filter districts based on cityId
        //    return GetAllStoresForSaleAr().Where(d => d.CityID == cityId).ToList();
        //}

        public static List<string> GetAllDistrictByCityNameEn(string CityNameEn)
        {
            return clsDistrictData.GetAllDistrictByCityNameEn(CityNameEn);
        }

        public static List<string> GetAllDistrictByCityNameAr(string CityNameAr)
        {
            return clsDistrictData.GetAllDistrictByCityNameAr(CityNameAr);
        }



        public static string GetDistrictsNameArByDistrictsID(int? DistrictsID)
        {
            return clsDistrictData.GetDistrictsNameArByDistrictsID(DistrictsID);
        }


        public static string GetDistrictsNameEnByDistrictsID(int? DistrictsID)
        {
            return clsDistrictData.GetDistrictsNameEnByDistrictsID(DistrictsID);
        }






        public static int? GetDistrictsIDByDistrictNameAr(string DistrictNameAr)
        {
            return clsDistrictData.GetDistrictsIDByDistrictNameAr(DistrictNameAr);
        }

        public static int? GetDistrictsIDByDistrictNameEn(string DistrictNameEn)
        {
            return clsDistrictData.GetDistrictsIDByDistrictNameEn(DistrictNameEn);
        }
    }
}
