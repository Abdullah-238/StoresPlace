using StoresPlace_DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StoresPlace_DataAccess.clsRegionData;

namespace StoresPlace_Business
{
    public class clsRegion
    {
        public enum enMode { ADD = 1, UPDATE = 2 }
        public enMode Mode;

        public int? RegionID { get; set; }
        public string Code { get; set; }
        public string RegionNameAr { get; set; }
        public string RegionNameEn { get; set; }
        public RegionDTO RDTO
        {
            get { return (new RegionDTO(this.RegionID, this.Code, this.RegionNameAr, this.RegionNameEn)); }
        }
        public clsRegion(RegionDTO RDTO, enMode cMode = enMode.ADD)
        {
            this.RegionID = RDTO.RegionID;
            this.Code = RDTO.Code;
            this.RegionNameAr = RDTO.RegionNameAr;
            this.RegionNameEn = RDTO.RegionNameEn;
            Mode = cMode; ;
        }

        private bool _AddNewRegion()
        {
            this.RegionID = clsRegionData.AddNewRegion(RDTO);

            return (this.RegionID != null);
        }

        private bool _UpdateRegion()
        {
            return clsRegionData.UpdateRegion(RDTO);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.ADD:
                    if (_AddNewRegion())
                    {
                        this.Mode = enMode.UPDATE;
                        return true;
                    }
                    else
                        return false;

                case enMode.UPDATE:
                    return _UpdateRegion();

            }
            return false;
        }

        public static clsRegion Find(int? RegionID)
        {
            RegionDTO RDTO = clsRegionData.FindRegion(RegionID);
            if (RDTO != null)
                return new clsRegion(RDTO, enMode.UPDATE);
            else
                return null;
        }
        public static bool IsExist(int? RegionID)
        {
            return clsRegionData.IsRegionExists(RegionID);
        }

        public static bool Delete(int? RegionID)
        {

            return clsRegionData.DeleteRegion(RegionID);
        }
        public static List<RegionDTO> GetAll()
        {

            return clsRegionData.GetAllRegion();
        }

        public static List<string> GetAllRegionsByNameAr()
        {
            return clsRegionData.GetAllRegionsByNameAr();
        }

        public static List<string> GetAllRegionsByNameEn()
        {
            return clsRegionData.GetAllRegionsByNameEn();
        }


        public static string GetRegionNameArByRegionID(int? RegionID)
        {
            return clsRegionData.GetRegionNameArByRegionID(RegionID);
        }

        public static string GetRegionNameEnByRegionID(int? RegionID)
        {
            return clsRegionData.GetCategoryNameEnByRegionID(RegionID);
        }


        public static string GetRegionNameArByDistrictsID(int? DistrictsID)
        {
            return clsRegionData.GetRegionNameArByDistrictsID(DistrictsID);
        }

        public static string GetRegionNameEnByDistrictsID(int? DistrictsID)
        {
            return clsRegionData.GetRegionNameEnByDistrictsID(DistrictsID);
        }



    }
}
