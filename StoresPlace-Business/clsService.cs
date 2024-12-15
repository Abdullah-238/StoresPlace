using StoresPlace_DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoresPlace_Business
{
    public class clsService
    {
        public enum enMode { ADD = 1, UPDATE = 2 }
        public enMode Mode;

        public int? ServiceID { get; set; }
        public string ServiceNameAr { get; set; }
        public string ServiceNameEn { get; set; }
        public ServiceDTO SDTO
        {
            get { return (new ServiceDTO(this.ServiceID, this.ServiceNameAr, this.ServiceNameEn)); }
        }
        public clsService(ServiceDTO SDTO, enMode cMode = enMode.ADD)
        {
            this.ServiceID = SDTO.ServiceID;
            this.ServiceNameAr = SDTO.ServiceNameAr;
            this.ServiceNameEn = SDTO.ServiceNameEn;

            Mode = cMode; ;
        }

        private bool _AddNewService()
        {
            this.ServiceID = clsServiceData.AddNewService(SDTO);

            return (this.ServiceID != null);
        }

        private bool _UpdateService()
        {
            return clsServiceData.UpdateService(SDTO);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.ADD:
                    if (_AddNewService())
                    {
                        this.Mode = enMode.UPDATE;
                        return true;
                    }
                    else
                        return false;

                case enMode.UPDATE:
                    return _UpdateService();

            }
            return false;
        }

        public static clsService Find(int? ServiceID)
        {
            ServiceDTO SDTO = clsServiceData.FindService(ServiceID);
            if (SDTO != null)
                return new clsService(SDTO, enMode.UPDATE);
            else
                return null;
        }
        public static bool IsExist(int? ServiceID)
        {
            return clsServiceData.IsServiceExists(ServiceID);
        }

        public static bool Delete(int? ServiceID)
        {

            return clsServiceData.DeleteService(ServiceID);
        }
        public static List<ServiceDTO> GetAll()
        {

            return clsServiceData.GetAllService();
        }

    }
}
