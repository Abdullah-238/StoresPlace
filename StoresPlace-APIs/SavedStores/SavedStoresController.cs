using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoresPlace_Business;
using StoresPlace_DataAccess;
using System;

namespace StoresPlace_APIs.SavedStores
{
    [Route("api/SavedStores")]
    [ApiController]
    public class SavedStoresController : ControllerBase
    {
        [HttpPost("AddSavedStore", Name = "AddSavedStore")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<SavedStoreDTO> AddSavedStore([FromBody] SavedStoreDTO savedStoreDTO)
        {
            clsSavedStore newSavedStore = new clsSavedStore(savedStoreDTO);

            if (newSavedStore.Save())
            {
                return Ok(newSavedStore.SDTO);
            }
            else
            {
                return BadRequest("Failed to add the saved store.");
            }
        }

        [HttpPut("UpdateSavedStore/{StoreSavedId}", Name = "UpdateSavedStore")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<SavedStoreDTO> UpdateSavedStore(int StoreSavedId,  SavedStoreDTO savedStoreDTO)
        {
            if (StoreSavedId != savedStoreDTO.StoreSavedId)
            {
                return BadRequest("StoreSavedId mismatch.");
            }

            clsSavedStore existingSavedStore =  clsSavedStore.Find(StoreSavedId);

            existingSavedStore.PersonID = savedStoreDTO.PersonID;
            existingSavedStore.StoreID = savedStoreDTO.StoreSavedId;


            if (existingSavedStore.Save())
            {
                return Ok(existingSavedStore.SDTO);
            }
            else
            {
                return NotFound("Saved store not found for update.");
            }
        }

        [HttpDelete("DeleteSavedStore/{StoreSavedId}", Name = "DeleteSavedStore")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<string> DeleteSavedStore(int StoreSavedId)
        {
            bool result = clsSavedStore.Delete(StoreSavedId);

            if (result)
            {
                return Ok($"Saved store with ID {StoreSavedId} deleted successfully.");
            }
            else
            {
                return NotFound($"Saved store with ID {StoreSavedId} not found.");
            }
        }

        [HttpDelete("DeleteSavedStoreByStoreID/{StoreSavedId}/{PersonID}", Name = "DeleteSavedStoreByStoreID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<string> DeleteSavedStoreByStoreID(int StoreSavedId, int PersonID)
        {
            bool result = clsSavedStore.DeleteSavedStoreByStoreID(StoreSavedId, PersonID);

            if (result)
            {
                return Ok($"Saved store with ID {StoreSavedId} for PersonID {PersonID} deleted successfully.");
            }
            else
            {
                return NotFound($"Saved store with ID {StoreSavedId} for PersonID {PersonID} not found.");
            }
        }

        [HttpGet("GetSavedStore/{StoreSavedId}", Name = "GetSavedStore")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<SavedStoreDTO> GetSavedStore(int StoreSavedId)
        {
            var savedStore = clsSavedStore.Find(StoreSavedId);

            if (savedStore != null)
            {
                return Ok(savedStore.SDTO);
            }
            else
            {
                return NotFound("Saved store not found.");
            }
        }

      
        [HttpGet("GetSavedStoreByPersonIDAr/{PersonID}/{PageNumber}", Name = "GetSavedStoreByPersonIDAr")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<List<StoreDetailsDTO>> GetSavedStoreByPersonIDAr(int PersonID, int? PageNumber)
        {
            var savedStores = clsSavedStore.GetAllSavedStoreByPersonIDAr(PersonID, PageNumber);

            if (savedStores.Count == 0)
            {
                return NotFound("No stores found for this category.");
            }
            return Ok(savedStores);
        }

        [HttpGet("GetSavedStoreByPersonIDEn/{PersonID}/{PageNumber}", Name = "GetSavedStoreByPersonIDEn")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<StoreDetailsDTO>> GetSavedStoreByPersonIDEn(int PersonID, int? PageNumber)
        {
            var savedStores = clsSavedStore.GetAllSavedStoreByPersonIDEn(PersonID, PageNumber);

            if (savedStores.Count == 0)
            {
                return NotFound("No stores found for this category.");
            }
            return Ok(savedStores);
        }

        [HttpGet("Exists/{StoreSavedId}", Name = "IsSavedStoreExists")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<bool> IsSavedStoreExists(int StoreSavedId)
        {
            bool exists = clsSavedStore.IsExist(StoreSavedId);

            return Ok(exists);
        }

        [HttpGet("ExistsByStoreAndPerson/{StoreID}/{PersonID}", Name = "IsSavedStoreExistsByStoreAndPerson")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<bool> IsSavedStoreExistsByStoreAndPerson(int StoreID, int PersonID)
        {
            bool exists = clsSavedStore.IsSavedStoreExists(StoreID, PersonID);

            return Ok(exists);
        }
    }
}

