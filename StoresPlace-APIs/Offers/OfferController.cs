using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoresPlace_Business;
using StoresPlace_DataAccess;

namespace StoresPlace_APIs.Offers
{
    [Route("api/Offer")]
    [ApiController]
    public class OfferController : ControllerBase
    {
        [HttpPost("AddOffer", Name = "AddOffer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<OfferDTO> AddOffer(OfferDTO offerDTO)
        {
            clsOffer newOffer = new clsOffer(offerDTO);

            if (newOffer.Save())
            {
                return Ok(newOffer.ODTO);
            }
            else
            {
                return BadRequest("Failed to add the offer.");
            }
        }

        [HttpPut("UpdateOffer/{OfferID}", Name = "UpdateOffer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<OfferDTO> UpdateOffer(int OfferID, OfferDTO offerDTO)
        {
            if (OfferID != offerDTO.OfferID)
            {
                return BadRequest("Offer ID mismatch.");
            }

            clsOffer existingOffer =  clsOffer.Find(OfferID);

            existingOffer.Offer = offerDTO.Offer;
            existingOffer.StoreID = offerDTO.StoreID;

            if (existingOffer.Save())
            {
                return Ok(existingOffer.ODTO);
            }
            else
            {
                return StatusCode(500, new { message = "Can't update this district" });
            }
        }

        [HttpDelete("DeleteOffer/{OfferID}", Name = "DeleteOffer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<string> DeleteOffer(int OfferID)
        {
            bool result = clsOffer.Delete(OfferID);

            if (result)
            {
                return Ok($"Offer with ID {OfferID} deleted successfully.");
            }
            else
            {
                return NotFound($"Offer with ID {OfferID} not found.");
            }
        }

        [HttpGet("GetOffer/{OfferID}", Name = "GetOffer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<OfferDTO> GetOffer(int OfferID)
        {
            var offer = clsOffer.Find(OfferID);

            if (offer != null)
            {
                return Ok(offer.ODTO);
            }
            else
            {
                return NotFound("Offer not found.");
            }
        }

        [HttpGet("GetAllOffers", Name = "GetAllOffers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<OfferDTO>> GetAllOffers()
        {
            var offers = clsOffer.GetAll();

            if (offers != null && offers.Count > 0)
            {
                return Ok(offers);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet("GetAllOfferByStoreID/{StoreID}", Name = "GetAllOfferByStoreID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<string>> GetAllOfferByStoreID( int? StoreID)
        {
            var offers = clsOffer.GetAllOfferByStoreID(StoreID);

            return Ok(offers);
        }

        [HttpGet("GetAllOfferByStore/{StoreID}", Name = "GetAllOfferByStore")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<OfferDTO>> GetAllOfferByStore( int? StoreID)
        {
            var offers = clsOffer.GetAllOffer(StoreID);

            return Ok(offers);
        }

        [HttpGet("Exists/{OfferID}", Name = "IsOfferExists")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<bool> IsOfferExists(int OfferID)
        {
            bool exists = clsOffer.IsExist(OfferID);

            return Ok(exists);
        }
    }
}
