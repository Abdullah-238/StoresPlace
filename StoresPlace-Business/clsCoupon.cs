using StoresPlace_DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StoresPlace_DataAccess.clsCouponData;

namespace StoresPlace_Business
{
    public class clsCoupon
    {
        public enum enMode { ADD = 1, UPDATE = 2 }
        public enMode Mode;

        public int? CouponID { get; set; }
        public string Coupon { get; set; }
        public int? StoreID { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsActive { get; set; }
        public CouponDTO CDTO
        {
            get { return (new CouponDTO(this.CouponID, this.Coupon, this.StoreID, this.ExpiryDate, this.IsActive)); }
        }
        public clsCoupon(CouponDTO CDTO, enMode cMode = enMode.ADD)
        {
            this.CouponID = CDTO.CouponID;
            this.Coupon = CDTO.Coupon;
            this.StoreID = CDTO.StoreID;
            this.ExpiryDate = CDTO.ExpiryDate;
            this.IsActive = CDTO.IsActive;

            Mode = cMode; ;
        }

        private bool _AddNewCoupon()
        {
            this.CouponID = clsCouponData.AddNewCoupon(CDTO);

            return (this.CouponID != null);
        }

        private bool _UpdateCoupon()
        {
            return clsCouponData.UpdateCoupon(CDTO);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.ADD:
                    if (_AddNewCoupon())
                    {
                        this.Mode = enMode.UPDATE;
                        return true;
                    }
                    else
                        return false;

                case enMode.UPDATE:
                    return _UpdateCoupon();

            }
            return false;
        }

        public static clsCoupon Find(int? CouponID)
        {
            CouponDTO CDTO = clsCouponData.FindCoupon(CouponID);
            if (CDTO != null)
                return new clsCoupon(CDTO, enMode.UPDATE);
            else
                return null;
        }
        public static bool IsExist(int? CouponID)
        {
            return clsCouponData.IsCouponExists(CouponID);
        }

        public static bool Delete(int? CouponID)
        {

            return clsCouponData.DeleteCoupon(CouponID);
        }
        public static List<CouponDTO> GetAll()
        {

            return clsCouponData.GetAllCoupon();
        }

        public static List<string> GetAllCouponByStoreID(int ?StoreID)
        {

            return clsCouponData.GetAllCouponByStoreID(StoreID);
        }

    }
}
