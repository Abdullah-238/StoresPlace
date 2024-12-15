

using static StoresPlace_DataAccess.clsUserData;
using StoresPlace_DataAccess;

namespace StoresPlace_Business
{
 public class clsUser
    {
        public enum enMode { ADD = 1, UPDATE = 2 }
        public enMode Mode;

        public int? UserID { get; set; }
        public int? PersonID { get; set; }
        public string UserName { get; set; }
        public UserDTO UDTO
        {
            get { return (new UserDTO(this.UserID, this.PersonID, this.UserName)); }
        }
        public clsUser(UserDTO UDTO, enMode cMode = enMode.ADD)
        {
            this.UserID = UDTO.UserID;
            this.PersonID = UDTO.PersonID;
            this.UserName = UDTO.UserName;
            Mode = cMode; ;
        }

        private bool _AddNewUser()
        {
            this.UserID = clsUserData.AddNewUser(UDTO);

            return (this.UserID != null);
        }

        private bool _UpdateUser()
        {
            return clsUserData.UpdateUser(UDTO);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.ADD:
                    if (_AddNewUser())
                    {
                        this.Mode = enMode.UPDATE;
                        return true;
                    }
                    else
                        return false;

                case enMode.UPDATE:
                    return _UpdateUser();

            }
            return false;
        }

        public static clsUser Find(int? UserID)
        {
            UserDTO UDTO = clsUserData.FindUser(UserID);
            if (UDTO != null)
                return new clsUser(UDTO, enMode.UPDATE);
            else
                return null;
        }
        public static bool IsExist(int? UserID)
        {
            return clsUserData.IsUserExists(UserID);
        }

        public static bool Delete(int? UserID)
        {

            return clsUserData.DeleteUser(UserID);
        }
        public static List<UserDTO> GetAll()
        {

            return clsUserData.GetAllUser();
        }

    }
}
