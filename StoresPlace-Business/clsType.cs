using Microsoft.Data.SqlClient;
using StoresPlace_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StoresPlace_DataAccess.clsTypeData;

namespace StoresPlace_Business
{
    public class clsType
    {
        public enum enMode { ADD = 1, UPDATE = 2 }
        public enMode Mode;

        public int? TypeID { get; set; }
        public string TypeNameAr { get; set; }

        public string TypeNameEn { get; set; }

        public TypeDTO TDTO
        {
            get { return (new TypeDTO(this.TypeID, this.TypeNameAr,this.TypeNameEn)); }
        }
        public clsType(TypeDTO TDTO, enMode cMode = enMode.ADD)
        {
            this.TypeID = TDTO.TypeID;
            this.TypeNameAr = TDTO.TypeNameAr;
            this.TypeNameEn = TDTO.TypeNameEn;

            Mode = cMode; ;
        }

        //private bool _AddNewType()
        //{
        //    this.TypeID = clsTypeData.AddNewType(TDTO);

        //    return (this.TypeID != null);
        //}

        //private bool _UpdateType()
        //{
        //    return clsTypeData.UpdateType(TDTO);
        //}

        //public bool Save()
        //{
        //    switch (Mode)
        //    {
        //        case enMode.ADD:
        //            if (_AddNewType())
        //            {
        //                this.Mode = enMode.UPDATE;
        //                return true;
        //            }
        //            else
        //                return false;

        //        case enMode.UPDATE:
        //            return _UpdateType();

        //    }
        //    return false;
        //}

        public static clsType Find(int? TypeID)
        {
            TypeDTO TDTO = clsTypeData.FindType(TypeID);
            if (TDTO != null)
                return new clsType(TDTO, enMode.UPDATE);
            else
                return null;
        }
        //public static bool IsExist(int? TypeID)
        //{
        //    return clsTypeData.IsTypeExists(TypeID);
        //}

        //public static bool Delete(int? TypeID)
        //{

        //    return clsTypeData.DeleteType(TypeID);
        //}
        public static List<TypeDTO> GetAll()
        {

            return clsTypeData.GetAllType();
        }


      

        public static List<string> GetAllTypeAr()
        {
            return clsTypeData.GetAllTypeAr();
        }

        public static List<string> GetAllTypeEn()
        {
            return clsTypeData.GetAllTypeEn();
        }

        public static TypeDTO FindTypeAr(string TypeNameAr)
        {
            return clsTypeData.FindTypeAr(TypeNameAr);
        }

        public static TypeDTO FindTypeEn(string TypeNameEn)
        {
            return clsTypeData.FindTypeEn(TypeNameEn);
        }

    }
}
