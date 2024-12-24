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

        [HttpPost("AddDistrict", Name = "AddDistrict")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<DistrictDTO> AddDistrict(DistrictDTO districtDTO)
        {
            try
            {
                clsDistrict newDistrict = new clsDistrict(districtDTO);

                if (newDistrict.Save())
                {
                    return Ok(newDistrict.DDTO);
                }
                else
                {
                    return BadRequest("Failed to add the district.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("UpdateDistrict/{DistrictID}", Name = "UpdateDistrict")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<DistrictDTO> UpdateDistrict(int DistrictID, DistrictDTO districtDTO)
        {

            if (DistrictID != districtDTO.DistrictsID)
            {
                return BadRequest("DistrictID mismatch.");
            }

            clsDistrict existingDistrict = clsDistrict.Find(DistrictID);

            existingDistrict.DistrictsNameEn = districtDTO.DistrictsNameEn;
            existingDistrict.DistrictsNameAr = districtDTO.DistrictsNameAr;
            existingDistrict.CityID = districtDTO.CityID;

            if (existingDistrict != null && existingDistrict.Save())
            {
                return Ok(existingDistrict.DDTO);  // Return updated district data
            }
            else
            {
                return StatusCode(500, new { message = "Can't update this district" });
            }

        }

        [HttpDelete("DeleteDistrict/{DistrictID}", Name = "DeleteDistrict")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<string> DeleteDistrict(int DistrictID)
        {
            try
            {
                // Attempt to delete the district
                bool result = clsDistrict.Delete(DistrictID);
                if (result)
                {
                    return Ok($"District with ID {DistrictID} deleted successfully.");
                }
                else
                {
                    return NotFound($"District with ID {DistrictID} not found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

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

        //// Get districts by City ID
        //[HttpGet("GetDistrictsByCity/{CityID}", Name = "GetDistrictsByCity")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public ActionResult<List<DistrictDTO>> GetDistrictsByCity(int CityID)
        //{
        //    try
        //    {
        //        var districts = clsDistrict.GetAll(CityID);
        //        if (districts != null && districts.Count > 0)
        //        {
        //            return Ok(districts);  // Return districts for the specified city
        //        }
        //        else
        //        {
        //            return NotFound($"No districts found for city ID {CityID}.");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}

        // Get district names by city name in Arabic
        [HttpGet("GetAllDistrictByCityNameAr/{CityNameAr}", Name = "GetAllDistrictByCityNameAr")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<string>> GetAllDistrictByCityNameAr(string CityNameAr)
        {
            try
            {
                // Get all district names by city name in Arabic
                var districts = clsDistrict.GetAllDistrictByCityNameAr(CityNameAr);
                if (districts != null && districts.Count > 0)
                {
                    return Ok(districts);  // Return district names
                }
                else
                {
                    return NotFound($"No districts found for city name {CityNameAr}.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Get district names by city name in English
        [HttpGet("GetAllDistrictByCityNameEn/{CityNameEn}", Name = "GetAllDistrictByCityNameEn")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<string>> GetAllDistrictByCityNameEn(string CityNameEn)
        {
            try
            {
                // Get all district names by city name in English
                var districts = clsDistrict.GetAllDistrictByCityNameEn(CityNameEn);
                if (districts != null && districts.Count > 0)
                {
                    return Ok(districts);  // Return district names
                }
                else
                {
                    return NotFound($"No districts found for city name {CityNameEn}.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }




        [HttpGet("GetDistrictsNameArByDistrictsID/{DistrictsID}", Name = "GetDistrictsNameArByDistrictsID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<string> GetDistrictsNameArByDistrictsID(int? DistrictsID)
        {
            string district = clsDistrict.GetDistrictsNameArByDistrictsID(DistrictsID);

            if (district != null)
            {
                return Ok(district);
            }
            else
            {
                return NotFound("District not found.");
            }
        }

        [HttpGet("GetDistrictsNameEnByDistrictsID/{DistrictsID}", Name = "GetDistrictsNameEnByDistrictsID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<string> GetDistrictsNameEnByDistrictsID(int? DistrictsID)
        {
            string district = clsDistrict.GetDistrictsNameEnByDistrictsID(DistrictsID);

            if (district != null)
            {
                return Ok(district);
            }
            else
            {
                return NotFound("District not found.");
            }
        }





        [HttpGet("GetDistrictsIDByDistrictNameEn/{DistrictNameEn}", Name = "GetDistrictsIDByDistrictNameEn")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<int> GetTypeIDByTypeNameEn(string DistrictNameEn)
        {
            int? DistrictsID = clsDistrict.GetDistrictsIDByDistrictNameEn(DistrictNameEn);

            if (DistrictsID != null)
            {
                return Ok(DistrictsID);
            }
            else
            {
                return NotFound("Districts ID id not found.");
            }
        }


        [HttpGet("GetDistrictsIDByDistrictNameAr/{DistrictNameAr}", Name = "GetDistrictsIDByDistrictNameAr")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<int> GetDistrictsIDByDistrictNameAr(string DistrictNameAr)
        {
            int? DistrictsID = clsDistrict.GetDistrictsIDByDistrictNameAr(DistrictNameAr);

            if (DistrictsID != null)
            {
                return Ok(DistrictsID);
            }
            else
            {
                return NotFound("Districts ID id not found.");
            }
        }


    }

}



