using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoresPlace_Business;
using StoresPlace_DataAccess;

namespace StoresPlace_APIs.Rates
{
    [Route("api/Rates")]
    [ApiController]
    public class RateController : ControllerBase
    {
        [HttpPost("AddRate", Name = "AddRate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<RateDTO> AddRate( RateDTO rateDTO)
        {
            clsRate newRate = new clsRate(rateDTO);

            if (newRate.Save())
            {
                return Ok(newRate.RDTO);
            }
            else
            {
                return BadRequest("Failed to add the rate.");
            }
        }

        [HttpPut("UpdateRate/{RateID}", Name = "UpdateRate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<RateDTO> UpdateRate(int RateID,  RateDTO rateDTO)
        {
            if (RateID != rateDTO.RateID)
            {
                return BadRequest("Rate ID mismatch.");
            }

            clsRate existingRate =  clsRate.Find(RateID);

            existingRate.Rate = rateDTO.Rate;
            existingRate.Comment = rateDTO.Comment;
            existingRate.PersonID = existingRate.PersonID;

            if (existingRate.Save())
            {
                return Ok(existingRate.RDTO);
            }
            else
            {
                return StatusCode(500, new { message = "Can't update this rate" });
            }
        }

        [HttpDelete("DeleteRate/{RateID}", Name = "DeleteRate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<string> DeleteRate(int RateID)
        {
            bool result = clsRate.Delete(RateID);

            if (result)
            {
                return Ok($"Rate with ID {RateID} deleted successfully.");
            }
            else
            {
                return NotFound($"Rate with ID {RateID} not found.");
            }
        }

        [HttpGet("GetRate/{RateID}", Name = "GetRate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<RateDTO> GetRate(int RateID)
        {
            var rate = clsRate.Find(RateID);

            if (rate != null)
            {
                return Ok(rate.RDTO);
            }
            else
            {
                return NotFound("Rate not found.");
            }
        }

        [HttpGet("GetRateByStoreAndPerson/{StoreID}/{PersonID}", Name = "GetRateByStoreAndPerson")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<RateDTO> GetRateByStoreAndPerson(int StoreID, int PersonID)
        {
            var rate = clsRate.Find(StoreID, PersonID);

            if (rate != null)
            {
                return Ok(rate.RDTO);
            }
            else
            {
                return NotFound("Rate not found for this Store and Person.");
            }
        }

        [HttpGet("GetAllRates", Name = "GetAllRates")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<RateDTO>> GetAllRates()
        {
            var rates = clsRate.GetAll();

            if (rates != null && rates.Count > 0)
            {
                return Ok(rates);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet("Exists/{RateID}", Name = "IsRateExists")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<bool> IsRateExists(int RateID)
        {
            bool exists = clsRate.IsExist(RateID);

            return Ok(exists);
        }

        [HttpGet("ExistsByStoreAndPerson/{StoreID}/{PersonID}", Name = "IsRateExistsByStoreAndPerson")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<bool> IsRateExistsByStoreAndPerson(int StoreID, int PersonID)
        {
            bool exists = clsRate.IsRateExists(StoreID, PersonID);

            return Ok(exists);
        }
    }

}
