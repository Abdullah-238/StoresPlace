using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoresPlace_DataAccess;

namespace StoresPlace_APIs.AppSettings
{
    [Route("api/AppSettings")]
    [ApiController]
    public class AppSettingsController : ControllerBase
    {

        [HttpGet("GetVersionString", Name = "GetVersionString")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult<string> GetVersionString()
        {
            try
            {
                string versionString = clsAppSettingsData.GetVersionString();

                if (versionString != null)
                {
                    return Ok(versionString);  
                }
                else
                {
                    return NotFound("Version string not found."); 
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error");  
            }
        }
    }
}
