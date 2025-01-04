using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoresPlace_Business;
using StoresPlace_DataAccess;

namespace StoresPlace_APIs.Cities
{
    [Route("api/City")]
    [ApiController]
    public class CityController : ControllerBase
    {
        [HttpGet("GetCity/{CityID}", Name = "GetCity")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<CityDTO> GetCity(int CityID)
        {
            var city = clsCity.Find(CityID);
            if (city != null)
            {
                return Ok(city.CDTO);
            }
            else
            {
                return NotFound("City not found.");
            }
        }

        [HttpGet("GetAllCities", Name = "GetAllCities")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<CityDTO>> GetAllCities()
        {
            var cities = clsCity.GetAll();
            if (cities != null && cities.Count > 0)
            {
                return Ok(cities);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet("GetCitiesByRegion/{RegionID}", Name = "GetCitiesByRegion")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<CityDTO>> GetCitiesByRegion(int RegionID)
        {
            var cities = clsCity.GetCitiesByRegion(RegionID);
            if (cities != null && cities.Count > 0)
            {
                return Ok(cities);
            }
            else
            {
                return NotFound($"No cities found for region ID {RegionID}.");
            }
        }

        [HttpGet("GetCitiesByRegionNameEn/{RegionNameEn}", Name = "GetCitiesByRegionNameEn")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<string>> GetCitiesByRegionNameEn(string RegionNameEn)
        {
            var cities = clsCity.GetAllCitiesByRegionNameEn(RegionNameEn);
            if (cities != null && cities.Count > 0)
            {
                return Ok(cities);
            }
            else
            {
                return NotFound($"No cities found for region name {RegionNameEn}.");
            }
        }

        [HttpGet("GetCitiesByRegionNameAr/{RegionNameAr}", Name = "GetCitiesByRegionNameAr")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<List<string>> GetCitiesByRegionNameAr(string RegionNameAr)
        {
            var cities = clsCity.GetAllCitiesByRegionNameAr(RegionNameAr);
            if (cities != null && cities.Count > 0)
            {
                return Ok(cities);
            }
            else
            {
                return NotFound($"No cities found for region name {RegionNameAr}.");
            }
        }


        [HttpGet("GetCityNameArByDistrictsID/{DistrictsID}", Name = "GetCityNameArByDistrictsID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<string> GetCityNameArByDistrictsID(int? DistrictsID)
        {
            string city = clsCity.GetCityNameArByDistrictsID(DistrictsID);

            if (city != null)
            {
                return Ok(city);
            }
            else
            {
                return NotFound("City not found.");
            }
        }

        [HttpGet("GetCityNameEnByDistrictsID/{DistrictsID}", Name = "GetCityNameEnByDistrictsID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<string> GetCityNameEnByDistrictsID(int? DistrictsID)
        {
            string city = clsCity.GetCityNameEnByDistrictsID(DistrictsID);

            if (city != null)
            {
                return Ok(city);
            }
            else
            {
                return NotFound("City not found.");
            }
        }

    }
}
