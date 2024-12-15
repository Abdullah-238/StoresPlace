using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoresPlace_Business;
using StoresPlace_DataAccess;


namespace StoresPlace_API
{
    [Route("api/Stores")]
    [ApiController]
    public class StoresController : ControllerBase
    {
        [HttpGet("AllStoreByCategoryNameAr", Name = "GetAllStoreByCategoryNameAr")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<StoreDetailsDTO>> GetAllStoreByCategoryNameAr(string CategoryNameAr,int? TypeID)
        {
            List<StoreDetailsDTO> AllNewStores = clsStore.GetAllStoreByCategoryNameAr(CategoryNameAr, TypeID);

            if (AllNewStores.Count == 0)
                return NotFound("No Stores found");
            else
                return Ok(AllNewStores);
        }

        [HttpGet("AllStoreByCategoryNameEn", Name = "GetAllStoreByCategoryNameEn")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<StoreDetailsDTO>> GetAllStoreByCategoryNameEn(string CategoryNameAr, int? TypeID)
        {
            List<StoreDetailsDTO> AllNewStores = clsStore.GetAllStoreByCategoryNameEn(CategoryNameAr, TypeID);

            if (AllNewStores.Count == 0)
                return NotFound("No Stores found");
            else
                return Ok(AllNewStores);
        }

    }
}
