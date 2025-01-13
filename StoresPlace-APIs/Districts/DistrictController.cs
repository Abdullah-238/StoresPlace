using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoresPlace_Business;
using StoresPlace_DataAccess;

namespace StoresPlace_APIs.Districts
{
    [Route("api/District")]
    [ApiController]
    public class DistrictController : ControllerBase
    {

        [HttpGet("GetDistrict/{DistrictID}", Name = "GetDistrict")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<DistrictDTO> GetDistrict(int DistrictID)
        {
            try
            {
                // Find the district by ID
                var district = clsDistrict.Find(DistrictID);
                if (district != null)
                {
                    return Ok(district.DDTO);  // Return district data if found
                }
                else
                {
                    return NotFound("District not found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetAllDistricts", Name = "GetAllDistricts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<DistrictDTO>> GetAllDistricts()
        {
            try
            {
                // Get all districts
                var districts = clsDistrict.GetAll();
                if (districts != null && districts.Count > 0)
                {
                    return Ok(districts);  // Return all districts
                }
                else
                {
                    return NoContent();  // Return no content if no districts are found
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


    }

}



