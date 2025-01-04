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

        [HttpGet("GetAllRegionsByNameAr", Name = "GetAllRegionsByNameAr")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<string>> GetAllRegionsByNameAr()
        {
            var regions = clsRegion.GetAllRegionsByNameAr();
            if (regions != null && regions.Count > 0)
            {
                return Ok(regions);
            }
            else
            {
                return NotFound("No regions found.");
            }
        }

        [HttpGet("GetAllRegionsByNameEn", Name = "GetAllRegionsByNameEn")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<string>> GetAllRegionsByNameEn()
        {
            var regions = clsRegion.GetAllRegionsByNameEn();
            if (regions != null && regions.Count > 0)
            {
                return Ok(regions);
            }
            else
            {
                return NotFound("No regions found.");
            }
        }





        [HttpGet("GetRegionNameArByRegionID/{RegionID}", Name = "GetRegionNameArByRegionID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<string> GetRegionNameArByRegionID(int? RegionID)
        {
            string region = clsRegion.GetRegionNameArByRegionID(RegionID);

            if (region != null)
            {
                return Ok(region);
            }
            else
            {
                return NotFound("Region not found.");
            }
        }

        [HttpGet("GetRegionNameEnByRegionID/{RegionID}", Name = "GetRegionNameEnByRegionID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<string> GetRegionNameEnByRegionID(int? RegionID)
        {
            string region = clsRegion.GetRegionNameEnByRegionID(RegionID);

            if (region != null)
            {
                return Ok(region);
            }
            else
            {
                return NotFound("Region not found.");
            }
        }

        [HttpGet("GetRegionNameArByDistrictsID/{DistrictsID}", Name = "GetRegionNameArByDistrictsID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<string> GetRegionNameArByDistrictsID(int? DistrictsID)
        {
            string region = clsRegion.GetRegionNameArByDistrictsID(DistrictsID);

            if (region != null)
            {
                return Ok(region);
            }
            else
            {
                return NotFound("District not found.");
            }
        }

        [HttpGet("GetRegionNameEnByDistrictsID/{DistrictsID}", Name = "GetRegionNameEnByDistrictsID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<string> GetRegionNameEnByDistrictsID(int? DistrictsID)
        {
            string region = clsRegion.GetRegionNameEnByDistrictsID(DistrictsID);

            if (region != null)
            {
                return Ok(region);
            }
            else
            {
                return NotFound("District not found.");
            }
        }

    }
}
