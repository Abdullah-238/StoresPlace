using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoresPlace_Business;
using StoresPlace_DataAccess;

namespace StoresPlace_APIs.Regions
{
    [Route("api/Region")]
    [ApiController]
    public class RegionController : ControllerBase
    {
      
        [HttpGet("GetRegion/{RegionID}", Name = "GetRegion")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<RegionDTO> GetRegion(int RegionID)
        {
            var region = clsRegion.Find(RegionID);
            if (region != null)
            {
                return Ok(region.RDTO);
            }
            else
            {
                return NotFound("Region not found.");
            }
        }

        [HttpGet("GetAllRegions", Name = "GetAllRegions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<RegionDTO>> GetAllRegions()
        {
            var regions = clsRegion.GetAll();
            if (regions != null && regions.Count > 0)
            {
                return Ok(regions);
            }
            else
            {
                return NoContent();
            }
        }

      
    }
}
