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


    }
}
