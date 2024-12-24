using Microsoft.AspNetCore.Mvc;
using StoresPlace_Business;
using StoresPlace_DataAccess;


namespace StoresPlace_APIs.Stores
{
    [Route("api/Stores")]
    [ApiController]
    public class StoresController : ControllerBase
    {

        [HttpPost("AddStore", Name = "AddStore")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StoreDTO> AddStore(StoreDTO storeDTO)
        {


            if (storeDTO.PeronID == null || storeDTO.CategoryID == null || storeDTO.TypeID == null)
            {
                return BadRequest("Invalid category data");
            }

            clsStore newStore = new clsStore(storeDTO);

            if (newStore.Save())
            {
                return Ok(newStore.SDTO);
            }
            else
            {
                return StatusCode(500, $"Internal server error");
            }
        }


        [HttpPut("UpdateStore/{StoreID}", Name = "UpdateStore")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StoreDTO> UpdateStore(int StoreID, StoreDTO storeDTO)
        {


            if (StoreID < 1 || storeDTO == null || storeDTO.PeronID == null || storeDTO.CategoryID == null || storeDTO.TypeID == null)
            {
                return BadRequest("Invalid category data");
            }


            var existingStore = clsStore.Find(StoreID);

            if (existingStore == null)
            {
                return NotFound("Store not found.");
            }

            existingStore.Website = storeDTO.Website;
            existingStore.Address = storeDTO.Address;
            existingStore.CommercialNumber = storeDTO.CommercialNumber;
            existingStore.DistrictsID = storeDTO.DistrictsID;
            existingStore.Status = storeDTO.Status;
            existingStore.Name = storeDTO.Name;
            existingStore.TypeID = storeDTO.TypeID;
            existingStore.StoreID = storeDTO.StoreID;
            existingStore.Rating = storeDTO.Rating;
            existingStore.Photo = storeDTO.Photo;
            existingStore.NumberOfRates = storeDTO.NumberOfRates;
            existingStore.NumbersOfClick = storeDTO.NumbersOfClick;
            existingStore.PeronID = storeDTO.PeronID;
            existingStore.CategoryID = storeDTO.CategoryID;

            if (existingStore.Save())
            {
                return Ok(existingStore.SDTO);
            }
            else
            {
                return StatusCode(500, new { message = "Can't update this store" });
            }
        }


        [HttpGet("GetStore/{StoreID}", Name = "GetStore")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<StoreDTO> GetStore(int? StoreID)
        {
            clsStore store = clsStore.Find(StoreID);

            if (store == null)
            {
                return NotFound("Store not found.");
            }
            return Ok(store.SDTO);
        }

        [HttpGet("FindStoreAr/{StoreID}", Name = "GetStoreAR")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<StoreDetailsDTO> FindStoreAr(int? StoreID)
        {
            StoreDetailsDTO store = clsStore.FindStoreAr(StoreID);
            if (store == null)
            {
                return NotFound("Store not found.");
            }
            return Ok(store);
        }


        [HttpGet("FindStoreEn/{StoreID}", Name = "GetStoreEn")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<StoreDetailsDTO> FindStoreEn(int? StoreID)
        {
            StoreDetailsDTO store = clsStore.FindStoreEn(StoreID);
            if (store == null)
            {
                return NotFound("Store not found.");
            }
            return Ok(store);
        }




        [HttpDelete("DeleteStore/{StoreID}", Name = "DeleteStore")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<bool> DeleteStore(int StoreID)
        {
            if (clsStore.Delete(StoreID))
            {
                return Ok(true);
            }
            else
            {
                return NotFound("Store not found.");
            }
        }

        [HttpGet("GetAllStores", Name = "GetAllStores")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<StoreDTO>> GetAllStores()
        {
            var stores = clsStore.GetAll();
            if (stores.Count == 0)
            {
                return NotFound("No stores found.");
            }
            return Ok(stores);
        }

        [HttpGet("GetStoresByCategoryID/{CategoryID}", Name = "GetStoresByCategoryID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<StoreDetailsDTO>> GetStoresByCategoryID(int CategoryID)
        {
            var stores = clsStore.GetAllStoreByCategoryID(CategoryID);
            if (stores.Count == 0)
            {
                return NotFound("No stores found for this category.");
            }
            return Ok(stores);
        }

        [HttpGet("GetStoresByCategoryNameAr/{CategoryNameAr}/{TypeID?}", Name = "GetStoresByCategoryNameAr")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<StoreDetailsDTO>> GetStoresByCategoryNameAr(string CategoryNameAr, int? TypeID)
        {
            var stores = clsStore.GetAllStoreByCategoryNameAr(CategoryNameAr, TypeID);
            if (stores.Count == 0)
            {
                return NotFound("No stores found for this category.");
            }
            return Ok(stores);
        }

        [HttpGet("GetStoresByCategoryNameEn/{CategoryNameEn}/{TypeID?}", Name = "GetStoresByCategoryNameEn")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<StoreDetailsDTO>> GetStoresByCategoryNameEn(string CategoryNameEn, int? TypeID)
        {
            var stores = clsStore.GetAllStoreByCategoryNameEn(CategoryNameEn, TypeID);
            if (stores.Count == 0)
            {
                return NotFound("No stores found for this category.");
            }
            return Ok(stores);
        }

        [HttpGet("GetStoresByCategoryNameArAndRegionNameAr/{CategoryNameAr}/{RegionNameAr}/{TypeID?}", Name = "GetStoresByCategoryNameArAndRegionNameAr")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<StoreDetailsDTO>> GetStoresByCategoryNameArAndRegionNameAr(string CategoryNameAr, string RegionNameAr, int? TypeID)
        {
            var stores = clsStore.GetAllStoreByCategoryNameArAndRegionName(CategoryNameAr, RegionNameAr, TypeID);
            if (stores.Count == 0)
            {
                return NotFound("No stores found for this category and region.");
            }
            return Ok(stores);
        }

        [HttpGet("GetStoresByCategoryNameEnAndRegionNameEn/{CategoryNameEn}/{RegionNameEn}/{TypeID?}", Name = "GetStoresByCategoryNameEnAndRegionNameEn")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<StoreDetailsDTO>> GetStoresByCategoryNameEnAndRegionNameEn(string CategoryNameEn, string RegionNameEn, int? TypeID)
        {
            var stores = clsStore.GetAllStoreByCategoryNameEnAndRegionName(CategoryNameEn, RegionNameEn, TypeID);
            if (stores.Count == 0)
            {
                return NotFound("No stores found for this category and region.");
            }
            return Ok(stores);
        }

        [HttpGet("GetStoresByCategoryNameEnAndCityNameEn/{CategoryNameEn}/{CityNameEn}/{TypeID?}", Name = "GetStoresByCategoryNameEnAndCityNameEn")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<StoreDetailsDTO>> GetStoresByCategoryNameEnAndCityNameEn(string CategoryNameEn, string CityNameEn, int? TypeID)
        {
            var stores = clsStore.GetAllStoreByCategoryNameEnAndCityNameEn(CategoryNameEn, CityNameEn, TypeID);
            if (stores.Count == 0)
            {
                return NotFound("No stores found for this category and city.");
            }
            return Ok(stores);
        }

        [HttpGet("GetStoresByCategoryNameArAndCityNameAr/{CategoryNameAr}/{CityNameAr}/{TypeID?}", Name = "GetStoresByCategoryNameArAndCityNameAr")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<StoreDetailsDTO>> GetStoresByCategoryNameArAndCityNameAr(string CategoryNameAr, string CityNameAr, int? TypeID)
        {
            var stores = clsStore.GetAllStoreByCategoryNameArAndCityNameAr(CategoryNameAr, CityNameAr, TypeID);
            if (stores.Count == 0)
            {
                return NotFound("No stores found for this category and city.");
            }
            return Ok(stores);
        }

        [HttpGet("GetStoresByCategoryNameArAndDistrictsNameAr/{CategoryNameAr}/{DistrictsNameAr}/{TypeID?}", Name = "GetStoresByCategoryNameArAndDistrictsNameAr")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<StoreDetailsDTO>> GetStoresByCategoryNameArAndDistrictsNameAr(string CategoryNameAr, string DistrictsNameAr, int? TypeID)
        {
            var stores = clsStore.GetAllStoreByCategoryNameArAndDistrictsNameAr(CategoryNameAr, DistrictsNameAr, TypeID);
            if (stores.Count == 0)
            {
                return NotFound("No stores found for this category and district.");
            }
            return Ok(stores);
        }

        [HttpGet("GetStoresByCategoryNameEnAndDistrictsNameEn/{CategoryNameEn}/{DistrictsNameEn}/{TypeID?}", Name = "GetStoresByCategoryNameEnAndDistrictsNameEn")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<StoreDetailsDTO>> GetStoresByCategoryNameEnAndDistrictsNameEn(string CategoryNameEn, string DistrictsNameEn, int? TypeID)
        {
            var stores = clsStore.GetAllStoreByCategoryNameEnAndDistrictsNameEn(CategoryNameEn, DistrictsNameEn, TypeID);
            if (stores.Count == 0)
            {
                return NotFound("No stores found for this category and district.");
            }
            return Ok(stores);
        }


        [HttpGet("GetAllStoresByPersonID/{PersonID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<StoreDTO>> GetAllStoresByPersonID(int? PersonID)
        {

            var stores = clsStoreData.GetAllStoresByPersonID(PersonID);
            if (stores != null && stores.Count > 0)
            {
                return Ok(stores);  // Return the list of stores
            }
            else
            {
                return NotFound("No stores found for the given PersonID.");
            }

        }

        [HttpGet("GetAllStoresInDetailsAr")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<StoreDetailsDTO>> GetAllStoresInDetailsAr()
        {

            var storeDetails = clsStoreData.GetAllStoresInDetailsAr();
            if (storeDetails != null && storeDetails.Count > 0)
            {
                return Ok(storeDetails);  // Return store details in Arabic
            }
            else
            {
                return NotFound("No store details found.");
            }

        }

        [HttpGet("GetAllStoresInDetailsByPersonIDAr/{PersonID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<StoreDetailsDTO>> GetAllStoresInDetailsByPersonIDAr(int? PersonID)
        {

            var storeDetails = clsStoreData.GetAllStoresInDetailsByPersonIDAr(PersonID);
            if (storeDetails != null && storeDetails.Count > 0)
            {
                return Ok(storeDetails);  // Return store details by PersonID in Arabic
            }
            else
            {
                return NotFound("No store details found for the given PersonID.");
            }
        }



        [HttpGet("GetAllStoresInDetailsEn")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<StoreDetailsDTO>> GetAllStoresInDetailsEn()
        {

            var storeDetails = clsStoreData.GetAllStoresInDetailsEn();
            if (storeDetails != null && storeDetails.Count > 0)
            {
                return Ok(storeDetails);  // Return store details in English
            }
            else
            {
                return NotFound("No store details found.");
            }

        }

        [HttpGet("GetAllStoresInDetailsByPersonIDEn/{PersonID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<StoreDetailsDTO>> GetAllStoresInDetailsByPersonIDEn(int? PersonID)
        {

            var storeDetails = clsStoreData.GetAllStoresInDetailsByPersonIDEn(PersonID);
            if (storeDetails != null && storeDetails.Count > 0)
            {
                return Ok(storeDetails);
            }
            else
            {
                return NotFound("No store details found for the given PersonID.");
            }

        }



        [HttpPut("UpdateStoreRating/{StoreID}/{Rate}", Name = "UpdateStoreRating")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult<bool> UpdateStoreRating(byte? Rate, int? StoreID)
        {

            if (StoreID == null || Rate == null)
            {
                return BadRequest("StoreID and Rate are required.");
            }

            bool result = clsStoreData.UpdateStoreRating(Rate, StoreID);

            if (result)
            {
                return Ok(true);
            }
            else
            {
                return StatusCode(500, $"Internal server error");

            }

        }

        [HttpPut("UpdateStoreRatingWithOldRate/{StoreID}/{OldRate}/{NewRate}", Name = "UpdateStoreRatingWithOldRate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult UpdateStoreRatingWithOldRate(byte? OldRate, byte? NewRate, int? StoreID)
        {


            if (StoreID == null || OldRate == null || NewRate == null)
            {
                return BadRequest("StoreID, OldRate, and NewRate are required.");
            }

            bool result = clsStore.UpdateStoreRating(NewRate, OldRate, StoreID);

            if (result)
            {
                return Ok("Store rating updated successfully.");
            }
            else
            {
                return StatusCode(500, $"Internal server error");
            }
        }
    }
}
