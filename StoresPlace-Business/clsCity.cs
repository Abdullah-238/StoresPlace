using StoresPlace_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StoresPlace_DataAccess.clsCityData;

namespace StoresPlace_Business
{
    public class clsCity
    {
        public enum enMode { ADD = 1, UPDATE = 2 }
        public enMode Mode;

        public int? CityID { get; set; }
        public string CityNameAr { get; set; }
        public string CityNameEn { get; set; }
        public int? RegionID { get; set; }
        public CityDTO CDTO
        {
            get { return (new CityDTO(this.CityID, this.CityNameAr, this.CityNameEn, this.RegionID)); }
        }
        public clsCity(CityDTO CDTO, enMode cMode = enMode.ADD)
        {
            this.CityID = CDTO.CityID;
            this.CityNameAr = CDTO.CityNameAr;
            this.CityNameEn = CDTO.CityNameEn;
            this.RegionID = CDTO.RegionID;
            Mode = cMode; ;
        }

        private bool _AddNewCitie()
        {
            this.CityID = clsCityData.AddNewCity(CDTO);

            return (this.CityID != null);
        }

        private bool _UpdateCitie()
        {
            return clsCityData.UpdateCity(CDTO);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.ADD:
                    if (_AddNewCitie())
                    {
                        this.Mode = enMode.UPDATE;
                        return true;
                    }
                    else
                        return false;

                case enMode.UPDATE:
                    return _UpdateCitie();

            }
            return false;
        }

        public static clsCity Find(int? CityID)
        {
            CityDTO CDTO = clsCityData.FindCity(CityID);
            if (CDTO != null)
                return new clsCity(CDTO, enMode.UPDATE);
            else
                return null;
        }
        public static bool IsExist(int? CityID)
        {
            return clsCityData.IsCityExists(CityID);
        }

        public static bool Delete(int? CityID)
        {

            return clsCityData.DeleteCity(CityID);
        }
        public static List<CityDTO> GetAll()
        {

            return clsCityData.GetAllCity();
        }

        public static List<CityDTO> GetCitiesByRegion(int regionId)
        {
            // Replace with actual logic to filter cities based on regionId
            return GetAll().Where(c => c.RegionID == regionId).ToList();
        }

        public static List<string> GetAllCitiesByRegionNameEn(string RegionNameEn)
        {
            return clsCityData.GetAllCitiesByRegionNameEn(RegionNameEn);
        }

        public static List<string> GetAllCitiesByRegionNameAr(string RegionNameAr)
        {
            return clsCityData.GetAllCitiesByRegionNameAr(RegionNameAr);
        }


    }
}
