using StoresPlace_DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoresPlace_Business
{
    public class clsRate
    {
        public enum enMode { ADD = 1, UPDATE = 2 }
        public enMode Mode;

        public int? RateID { get; set; }
        public int? StoreID { get; set; }
        public string Comment { get; set; }
        public byte? Rate { get; set; }
        public int? PersonID { get; set; }
        public RateDTO RDTO
        {
            get { return (new RateDTO(this.RateID, this.StoreID, this.Comment, this.Rate, this.PersonID)); }
        }
        public clsRate(RateDTO RDTO, enMode cMode = enMode.ADD)
        {
            this.RateID = RDTO.RateID;
            this.StoreID = RDTO.StoreID;
            this.Comment = RDTO.Comment;
            this.Rate = RDTO.Rate;
            this.PersonID = RDTO.PersonID;

            Mode = cMode; ;
        }

        private bool _AddNewRate()
        {
            this.RateID = clsRateData.AddNewRate(RDTO);

            return (this.RateID != null);
        }

        private bool _UpdateRate()
        {
            return clsRateData.UpdateRate(RDTO);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.ADD:
                    if (_AddNewRate())
                    {
                        this.Mode = enMode.UPDATE;
                        return true;
                    }
                    else
                        return false;

                case enMode.UPDATE:
                    return _UpdateRate();

            }
            return false;
        }

        public static clsRate Find(int? RateID)
        {
            RateDTO RDTO = clsRateData.FindRate(RateID);
            if (RDTO != null)
                return new clsRate(RDTO, enMode.UPDATE);
            else
                return null;
        }
        public static bool IsExist(int? RateID)
        {
            return clsRateData.IsRateExists(RateID);
        }

        public static bool Delete(int? RateID)
        {

            return clsRateData.DeleteRate(RateID);
        }
        public static List<RateDTO> GetAll()
        {

            return clsRateData.GetAllRate();
        }

    }
}
