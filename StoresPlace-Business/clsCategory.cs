using StoresPlace_DataAccess;
using System;
using System.Runtime.CompilerServices;
using static StoresPlace_DataAccess.clsCategoriesData;

namespace StoresPlace_Business
{
    public class clsCategories
    {
        public enum enMode { ADD = 1, UPDATE = 2 }
        public enMode Mode;

        public int? CategoryID { get; set; }
        public string CategoryNameAr { get; set; }
        public string CategoryNameEn { get; set; }
        public CategoryDTO CDTO
        {
            get { return (new CategoryDTO(this.CategoryID, this.CategoryNameAr, this.CategoryNameEn)); }
        }
        public clsCategories(CategoryDTO CDTO, enMode cMode = enMode.ADD)
        {
            this.CategoryID = CDTO.CategoryID;
            this.CategoryNameAr = CDTO.CategoryNameAr;
            this.CategoryNameEn = CDTO.CategoryNameEn;

            Mode = cMode; ;
        }

        private bool _AddNewCategories()
        {
            this.CategoryID = clsCategoriesData.AddNewCategory(CDTO);

            return (this.CategoryID != null);
        }

        private bool _UpdateCategories()
        {
            return clsCategoriesData.UpdateCategory(CDTO);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.ADD:
                    if (_AddNewCategories())
                    {
                        this.Mode = enMode.UPDATE;
                        return true;
                    }
                    else
                        return false;

                case enMode.UPDATE:
                    return _UpdateCategories();

            }
            return false;
        }

        public static clsCategories Find(int? CategoryID)
        {
            CategoryDTO CDTO = clsCategoriesData.FindCategory(CategoryID);
            if (CDTO != null)
                return new clsCategories(CDTO, enMode.UPDATE);
            else
                return null;
        }

        public static CategoryDTO FindCategoryByCategoryNameEn(string CategoryNameEn)
        {
            return clsCategoriesData.FindCategoryByCategoryNameEn(CategoryNameEn);
        }

        public static CategoryDTO FindCategoryByCategoryNameAr(string CategoryNameAr)
        {
            return clsCategoriesData.FindCategoryByCategoryNameAr(CategoryNameAr);
        }

        public static bool IsExist(int? CategoryID)
        {
            return clsCategoriesData.IsCategoryExists(CategoryID);
        }

        public static bool Delete(int? CategoryID)
        {

            return clsCategoriesData.DeleteCategory(CategoryID);
        }

        public static List<CategoryDTO> GetAll()
        {

            return clsCategoriesData.GetAllCategory();
        }

        public static List<string> GetAllCategoryAvailableByNameAr(int ? TypeID)
        {
            return clsCategoriesData.GetAllCategoryAvailableByNameAr(TypeID);
        }

        public static List<string> GetAllCategoryAvailableByNameEn(int? TypeID)
        {
            return clsCategoriesData.GetAllCategoryAvailableByNameEn(TypeID);
        }

        public static List<string> GetAllCategoryEn()
        {
            return clsCategoriesData.GetAllCategoryEn();
        }

        public static List<string> GetAllCategoryAr()
        {
            return clsCategoriesData.GetAllCategoryAr();
        }


        public static string GetCategoryNameArByCategoryID(int? CategoryID)
        {
            return clsCategoriesData.GetCategoryNameArByCategoryID(CategoryID);
        }

        public static string GetCategoryNameEnByCategoryID(int? CategoryID)
        {
            return clsCategoriesData.GetCategoryNameEnByCategoryID(CategoryID);
        }



        public static int? GetCategoryIdByCategoryNameAr(string CategoryNameAr)
        {
            return clsCategoriesData.GetCategoryIdByCategoryNameAr(CategoryNameAr);
        }

        public static int? GetCategoryIdByCategoryNameEn(string CategoryNameEn)
        {
            return clsCategoriesData.GetCategoryIdByCategoryNameEn(CategoryNameEn);
        }

    }
}
