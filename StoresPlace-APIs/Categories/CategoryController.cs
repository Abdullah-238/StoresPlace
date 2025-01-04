using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoresPlace_Business;
using StoresPlace_DataAccess;
using System;


namespace StoresPlace_API.Categories
{
    [Route("api/Category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        [HttpGet("GetCategory", Name = "GetCategory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<CategoryDTO> GetCategory(int? CategoryID)
        {
            if (CategoryID == null || CategoryID < 1)
            {
                return BadRequest($"Invalid Category ID : {CategoryID}");
            }


            var category = clsCategories.Find(CategoryID);

            if (category == null)
            {
                return NotFound($"category with ID {CategoryID} not found.");
            }

                return Ok(category.CDTO);
      
        }

        [HttpGet("GetAllCategories", Name = "GetAllCategories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<List<CategoryDTO>> GetAllCategories()
        {
            List<CategoryDTO> categories = clsCategories.GetAll();

            if ( categories.Count == 0)
            {
                return NotFound("No Categories Found!");
            }

            return Ok(categories);

        }

        [HttpGet("Exists/{CategoryID}", Name = "IsCategoryExists")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<bool> IsCategoryExists(int? CategoryID)
        {
            if ( CategoryID < 1)
            {
                return BadRequest($"Invalid Category ID : {CategoryID}");
            }

            bool exists = clsCategories.IsExist(CategoryID);

            if (!exists)
            {
                return NotFound("No Categories Found!");
            }

            return Ok(exists);
        }

        [HttpGet("GetAllCategoryAvailableByNameAr/{TypeID}", Name = "GetAllCategoryAvailableByNameAr")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<List<string>> GetAllCategoryAvailableByNameAr(int? TypeID)
        {

            if (TypeID < 1)
            {
                return BadRequest($"Invalid Type ID : {TypeID}");
            }

            var categories = clsCategories.GetAllCategoryAvailableByNameAr(TypeID);

            if (categories.Count == 0)
            {
                return NotFound("No Categories Found!");
            }

            return Ok(categories);
        }

        [HttpGet("GetAllCategoryAvailableByNameEn/{TypeID}", Name = "GetAllCategoryAvailableByNameEn")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<string>> GetAllCategoryAvailableByNameEn( int? TypeID)
        {

            if (TypeID < 1)
            {
                return BadRequest($"Invalid Type ID : {TypeID}");
            }

            var categories = clsCategories.GetAllCategoryAvailableByNameEn(TypeID);

            if (categories.Count == 0)
            {
                return NotFound("No Categories Found!");
            }

            return Ok(categories);

        }

        [HttpGet("GetAllCategoryEn", Name = "GetAllCategoryEn")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<string>> GetAllCategoryEn()
        {

      
            var categories = clsCategories.GetAllCategoryEn();

            if (categories.Count == 0)
            {
                return NotFound("No Categories Found!");
            }


            return Ok(categories);
        }

        [HttpGet("GetAllCategoryAr", Name = "GetAllCategoryAr")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<List<string>> GetAllCategoryAr()
        {
            var categories = clsCategories.GetAllCategoryAr();

            if (categories.Count == 0)
            {
                return NotFound("No Categories Found!");
            }

            return Ok(categories);
        }

        [HttpGet("GetCategoryNameArByCategoryID/{CategoryID}", Name = "GetCategoryNameArByCategoryID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult<string> GetCategoryNameArByCategoryID(int CategoryID)
        {


            if (CategoryID < 1)
            {
                return BadRequest($"Invalid Category ID : {CategoryID}");
            }

            string category = clsCategories.GetCategoryNameArByCategoryID(CategoryID);

            if (category != null)
            {
                return Ok(category);
            }
            else
            {
                return NotFound("Category not found.");
            }
        }


        [HttpGet("GetCategoryNameEnByCategoryID/{CategoryID}", Name = "GetCategoryNameEnByCategoryID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult<string> GetCategoryNameEnByCategoryID(int CategoryID)
        {

            if (CategoryID < 1)
            {
                return BadRequest($"Invalid Category ID : {CategoryID}");
            }

            string category = clsCategories.GetCategoryNameEnByCategoryID(CategoryID);

            if (category != null)
            {
                return Ok(category);
            }
            else
            {
                return NotFound("Category not found.");
            }
        }



        [HttpGet("GetCategoryIDByCategoryNameEn/{CategoryNameEn}", Name = "GetCategoryIDByCategoryNameEn")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult<int> GetCategoryIDByCategoryNameEn(string CategoryNameEn)
        {
            if (string.IsNullOrEmpty(CategoryNameEn))
            {
                return BadRequest($"Invalid Category Name : {CategoryNameEn}");
            }

            int? Category = clsCategories.GetCategoryIdByCategoryNameEn(CategoryNameEn);

            if (Category != null)
            {
                return Ok(Category);
            }
            else
            {
                return NotFound("Category Name  not found.");
            }
        }


        [HttpGet("GetCategoryIDByCategoryNameAr/{CategoryNameAr}", Name = "GetCategoryIDByCategoryNameAr")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<int> GetCategoryIDByCategoryNameAr(string CategoryNameAr)
        {

            if (string.IsNullOrEmpty(CategoryNameAr))
            {
                return BadRequest($"Invalid Category Name : {CategoryNameAr}");
            }

            int? Category = clsCategories.GetCategoryIdByCategoryNameAr(CategoryNameAr);

            if (Category != null)
            {
                return Ok(Category);
            }
            else
            {
                return NotFound("Category id not found.");
            }
        }

    }
}

