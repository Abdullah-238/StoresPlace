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
        [HttpGet("AllCategoryAvailableByNameEn", Name = "GetAllCategoryAvailableByNameEn")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<IEnumerable<CategoryDTO>> GetAllCategoryAvailableByNameEn(int? TypeID)
        {
            List<string> AllNewCategory = clsCategories.GetAllCategoryAvailableByNameEn(TypeID);

            if (AllNewCategory.Count == 0)
                return NotFound("No Category found");
            else
                return Ok(AllNewCategory);
        }



        [HttpGet("AllCategoryAvailableByNameAr", Name = "GetAllCategoryAvailableByNameAr")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<IEnumerable<string>> GetAllCategoryAvailableByNameAr(int ?TypeID)
        {
            List<string> AllNewCategory = clsCategories.GetAllCategoryAvailableByNameAr(TypeID);

            if (AllNewCategory.Count == 0)
                return NotFound("No Category found");
            else
                return Ok(AllNewCategory);
        }
    }
}
