using StoresPlace_DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace StoresPlace_Business
{
    public class clsPerson
    {
        public enum enMode { ADD = 1, UPDATE = 2 }
        public enMode Mode;

        public int? PersonID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public PersonDTO PDTO
        {
            get { return (new PersonDTO(this.PersonID, this.Name, this.Phone, this.Email, this.Password, this.IsActive)); }
        }
        public clsPerson(PersonDTO PDTO, enMode cMode = enMode.ADD)
        {
            this.PersonID = PDTO.PersonID;
            this.Name = PDTO.Name;
            this.Phone = PDTO.Phone;
            this.Email = PDTO.Email;
            this.Password = PDTO.Password;
            this.IsActive = PDTO.IsActive;

            Mode = cMode; ;
        }

        private bool _AddNewPerson()
        {
            this.PersonID = clsPersonData.AddNewPerson(PDTO);

            return (this.PersonID != null);
        }

        private bool _UpdatePerson()
        {
            return clsPersonData.UpdatePerson(PDTO);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.ADD:
                    if (_AddNewPerson())
                    {
                        this.Mode = enMode.UPDATE;
                        return true;
                    }
                    else
                        return false;

                case enMode.UPDATE:
                    return _UpdatePerson();

            }
            return false;
        }

        public static clsPerson Find(int? PersonID)
        {
            PersonDTO PDTO = clsPersonData.FindPerson(PersonID);
            if (PDTO != null)
                return new clsPerson(PDTO, enMode.UPDATE);
            else
                return null;
        }
        public static bool IsExist(int? PersonID)
        {
            return clsPersonData.IsPersonExists(PersonID);
        }

        public static bool IsPersonExistsByEmail(string Email)
        {
            return clsPersonData.IsPersonExistsByEmail(Email);
        }

        public static bool IsPersonActiveByEmail(string Email)
        {
            return clsPersonData.IsPersonActiveByEmail(Email);
        }

        public static bool IsPersonExistsByPhone(string Phone)
        {
            return clsPersonData.IsPersonExistsByPhone(Phone);
        }

        public static bool IsPersonActive(int? PersonID)
        {
            return clsPersonData.IsPersonActive(PersonID);
        }
        public static bool Delete(int? PersonID)
        {

            return clsPersonData.DeletePerson(PersonID);
        }
        public static List<PersonDTO> GetAll()
        {

            return clsPersonData.GetAllPerson();
        }

        public static clsPerson FindPersonByEmailAndPassword(string Email, string Password)
        {
            PersonDTO PDTO = clsPersonData.FindPersonByEmailAndPassword(Email, Password);
            if (PDTO != null)
                return new clsPerson(PDTO, enMode.UPDATE);
            else
                return null;
        }


        public static bool UpdatePassword(string Email, string Password)
        {
            return clsPersonData.UpdatePassword(Email,Password);
        }



    }
}
