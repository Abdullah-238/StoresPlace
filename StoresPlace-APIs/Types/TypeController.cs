using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static StoresPlace_DataAccess.clsTypeData;
using StoresPlace_Business;

namespace StoresPlace_APIs.Types
{
    [Route("api/Types")]
    [ApiController]
    public class TypeController : ControllerBase
    {

      
        // Get a type by ID
        [HttpGet("GetType/{TypeID}", Name = "GetType")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<TypeDTO> GetType(int TypeID)
        {
            try
            {
                // Find the type by ID
                var type = clsType.Find(TypeID);
                if (type != null)
                {
                    return Ok(type.TDTO);  // Return type data if found
                }
                else
                {
                    return NotFound("Type not found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Get all types
        [HttpGet("GetAllTypes", Name = "GetAllTypes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<TypeDTO>> GetAllTypes()
        {
            try
            {
                // Get all types
                var types = clsType.GetAll();
                if (types != null && types.Count > 0)
                {
                    return Ok(types);  // Return all types
                }
                else
                {
                    return NoContent();  // Return no content if no types are found
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Get all types in Arabic
        [HttpGet("GetAllTypesAr", Name = "GetAllTypesAr")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<string>> GetAllTypesAr()
        {
            try
            {
                // Get all type names in Arabic
                var types = clsType.GetAllTypeAr();
                if (types != null && types.Count > 0)
                {
                    return Ok(types);  // Return type names in Arabic
                }
                else
                {
                    return NotFound("No types found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Get all types in English
        [HttpGet("GetAllTypesEn", Name = "GetAllTypesEn")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<string>> GetAllTypesEn()
        {
            try
            {
                // Get all type names in English
                var types = clsType.GetAllTypeEn();
                if (types != null && types.Count > 0)
                {
                    return Ok(types);  // Return type names in English
                }
                else
                {
                    return NotFound("No types found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Find a type by Arabic name
        [HttpGet("FindTypeAr/{TypeNameAr}", Name = "FindTypeAr")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<TypeDTO> FindTypeAr(string TypeNameAr)
        {
            try
            {
                var type = clsType.FindTypeAr(TypeNameAr);
                if (type != null)
                {
                    return Ok(type);  // Return type if found
                }
                else
                {
                    return NotFound($"Type with name {TypeNameAr} not found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Find a type by English name
        [HttpGet("FindTypeEn/{TypeNameEn}", Name = "FindTypeEn")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<TypeDTO> FindTypeEn(string TypeNameEn)
        {
            try
            {
                var type = clsType.FindTypeEn(TypeNameEn);
                if (type != null)
                {
                    return Ok(type);  // Return type if found
                }
                else
                {
                    return NotFound($"Type with name {TypeNameEn} not found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        [HttpGet("GetTypeNameArByTypeID/{TypeID}", Name = "GetTypeNameArByTypeID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<string> GetTypeNameArByTypeID(int TypeID)
        {
            string Type = clsType.GetTypeNameArByTypeID(TypeID);

            if (Type != null)
            {
                return Ok(Type);
            }
            else
            {
                return NotFound("Type not found.");
            }
        }


        [HttpGet("GetTypeNameEnByTypeID/{TypeID}", Name = "GetTypeNameEnByTypeID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<string> GetTypeNameEnByTypeID(int TypeID)
        {
            string Type = clsType.GetTypeNameEnByTypeID(TypeID);

            if (Type != null)
            {
                return Ok(Type);
            }
            else
            {
                return NotFound("Type not found.");
            }
        }



        [HttpGet("GetTypeIDByTypeNameEn/{TypeNameEn}", Name = "GetTypeIDByTypeNameEn")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<int> GetTypeIDByTypeNameEn(string TypeNameEn)
        {
            int? Type = clsType.GetTypeIDByTypeNameEn(TypeNameEn);

            if (Type != null)
            {
                return Ok(Type);
            }
            else
            {
                return NotFound("Type id not found.");
            }
        }


        [HttpGet("GetTypeIDByTypeNameAr/{TypeNameAr}", Name = "GetTypeIDByTypeNameAr")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<int> GetTypeNameArByTypeID(string TypeNameAr)
        {
            int? Type = clsType.GetTypeIDByTypeNameAr(TypeNameAr);

            if (Type != null)
            {
                return Ok(Type);
            }
            else
            {
                return NotFound("Type id not found.");
            }
        }

    }

}