using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoresPlace_Business;
using StoresPlace_DataAccess;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StoresPlace_APIs.StoresForSale
{
    [Route("api/StoresForSale")]
    [ApiController]
    public class StoresForSaleController : ControllerBase
    {
        [HttpPost("AddStoresForSale", Name = "AddStoresForSale")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<StoresForSaleDTO> AddStoresForSale([FromBody] StoresForSaleDTO storesForSaleDTO)
        {
            clsStoresForSale newStoresForSale = new clsStoresForSale(storesForSaleDTO);

            if (newStoresForSale.Save())
            {
                return Ok(newStoresForSale.SDTO);
            }
            else
            {
                return BadRequest("Failed to add the store for sale.");
            }
        }

        [HttpPut("UpdateStoresForSale/{StoreForSaleID}", Name = "UpdateStoresForSale")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<StoresForSaleDTO> UpdateStoresForSale(int StoreForSaleID, StoresForSaleDTO storesForSaleDTO)
        {
            if (StoreForSaleID != storesForSaleDTO.StoreForSaleID)
            {
                return BadRequest("StoreForSaleID mismatch.");
            }

            clsStoresForSale existingStoresForSale =  clsStoresForSale.Find(StoreForSaleID);

            existingStoresForSale.Price = storesForSaleDTO.Price;
            existingStoresForSale.Status = storesForSaleDTO.Status;
            existingStoresForSale.StoreID = storesForSaleDTO.StoreID;

            if (existingStoresForSale.Save())
            {
                return Ok(existingStoresForSale.SDTO);  // Successfully updated store rating
            }
            else
            {
                return NotFound("Store for sale not found for update.");
            }
        }

        [HttpDelete("DeleteStoresForSale/{StoreForSaleID}", Name = "DeleteStoresForSale")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<string> DeleteStoresForSale(int StoreForSaleID)
        {
            bool result = clsStoresForSale.Delete(StoreForSaleID);

            if (result)
            {
                return Ok($"Store for sale with ID {StoreForSaleID} deleted successfully.");
            }
            else
            {
                return NotFound($"Store for sale with ID {StoreForSaleID} not found.");
            }
        }

        [HttpGet("GetStoresForSale/{StoreID}", Name = "GetStoresForSale")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<StoresForSaleDTO> GetStoresForSale(int StoreID)
        {
            var storeForSale = clsStoresForSale.Find(StoreID);

            if (storeForSale != null)
            {
                return Ok(storeForSale.SDTO);
            }
            else
            {
                return NotFound("Store for sale not found.");
            }
        }

        [HttpGet("GetAllStoresForSaleAr", Name = "GetAllStoresForSaleAr")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<StoresForSaleDetailsDTO>> GetAllStoresForSaleAr()
        {
            var storesForSale = clsStoresForSale.GetAllStoresForSaleAr();

            if (storesForSale != null && storesForSale.Count > 0)
            {
                return Ok(storesForSale);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet("GetAllStoresForSaleEn", Name = "GetAllStoresForSaleEn")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<StoresForSaleDetailsDTO>> GetAllStoresForSaleEn()
        {
            var storesForSale = clsStoresForSale.GetAllStoresForSaleEn();

            if (storesForSale != null && storesForSale.Count > 0)
            {
                return Ok(storesForSale);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet("Exists/{StoreID}", Name = "IsStoresForSaleExists")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<bool> IsStoresForSaleExists(int StoreID)
        {
            bool exists = clsStoresForSale.IsStoresForSaleExists(StoreID);

            return Ok(exists);
        }
    }
}

