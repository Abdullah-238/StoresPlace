using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoresPlace_Business;
using StoresPlace_DataAccess;

namespace StoresPlace_APIs.Coupons
{
    [Route("api/Coupons")]
    [ApiController]
    public class CouponController : ControllerBase
    {

        [HttpPost("AddCoupon", Name = "AddCoupon")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<CouponDTO> AddCoupon( CouponDTO couponDTO)
        {
            clsCoupon newCoupon = new clsCoupon(couponDTO);

            if (newCoupon.Save())
            {
                return Ok(newCoupon.CDTO);
            }
            else
            {
                return BadRequest("Failed to add the coupon.");
            }
        }


        [HttpPut("UpdateCoupon/{CouponID}", Name = "UpdateCoupon")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<CouponDTO> UpdateCoupon(int CouponID,  CouponDTO couponDTO)
        {
            if (CouponID != couponDTO.CouponID)
            {
                return BadRequest("CouponID mismatch.");
            }

            clsCoupon existingCoupon = clsCoupon.Find(CouponID);
            existingCoupon.Coupon = couponDTO.Coupon;
            existingCoupon.ExpiryDate = couponDTO.ExpiryDate;
            existingCoupon.IsActive = couponDTO.IsActive;
            existingCoupon.StoreID = couponDTO.StoreID;

            if (existingCoupon != null && existingCoupon.Save())
            {
                return Ok(existingCoupon.CDTO);
            }
            else
            {
                return NotFound("Coupon not found for update.");
            }
        }


        [HttpDelete("DeleteCoupon/{CouponID}", Name = "DeleteCoupon")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<string> DeleteCoupon(int CouponID)
        {
            bool result = clsCoupon.Delete(CouponID);
            if (result)
            {
                return Ok($"Coupon with ID {CouponID} deleted successfully.");
            }
            else
            {
                return NotFound($"Coupon with ID {CouponID} not found.");
            }
        }


        [HttpGet("GetCoupon/{CouponID}", Name = "GetCoupon")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<CouponDTO> GetCoupon(int CouponID)
        {
            var coupon = clsCoupon.Find(CouponID);
            if (coupon != null)
            {
                return Ok(coupon.CDTO);
            }
            else
            {
                return NotFound("Coupon not found.");
            }
        }

        [HttpGet("GetAllCoupons", Name = "GetAllCoupons")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<CouponDTO>> GetAllCoupons()
        {
            var coupons = clsCoupon.GetAll();
            if (coupons != null && coupons.Count > 0)
            {
                return Ok(coupons);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet("GetCouponsByStore/{StoreID}", Name = "GetCouponsByStore")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<CouponDTO>> GetCouponsByStore(int StoreID)
        {
            var coupons = clsCoupon.GetAll(StoreID);
            if (coupons != null && coupons.Count > 0)
            {
                return Ok(coupons);
            }
            else
            {
                return NotFound($"No coupons found for store ID {StoreID}.");
            }
        }

        [HttpGet("GetCouponCodesByStore/{StoreID}", Name = "GetCouponCodesByStore")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<string>> GetCouponCodesByStore(int StoreID)
        {
            var couponCodes = clsCoupon.GetAllCouponByStoreID(StoreID);
            if (couponCodes != null && couponCodes.Count > 0)
            {
                return Ok(couponCodes);
            }
            else
            {
                return NotFound($"No coupon codes found for store ID {StoreID}.");
            }
        }
    }
}
