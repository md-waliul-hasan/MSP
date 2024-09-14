using Mango.Web.Models;
using Mango.Web.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Mango.Web.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICouponService _couponService;

        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        public async Task<IActionResult> CouponIndex()
        {
            List<CouponDto>? list = new();
            ResponseDto? responseDto = await _couponService.GetAllCouponsAsync();
            if (responseDto is not null && responseDto.Result is not null && responseDto.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<CouponDto>>(Convert.ToString(responseDto.Result));
            }

            return View(list);
        }

        public async Task<IActionResult> CouponCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CouponCreate(CouponDto couponDto)
        {
            if (ModelState.IsValid)
            {
                ResponseDto? responseDto = await _couponService.CreateCouponAsync(couponDto);
                if (responseDto is not null && responseDto.Result is not null && responseDto.IsSuccess)
                {
                    return RedirectToAction(nameof(CouponIndex));
                }
            }
            return View(couponDto);
        }

        public async Task<IActionResult> CouponDelete(int couponId)
        {
            ResponseDto? responseDto = await _couponService.GetCouponsByIdAsync(couponId);
            if (responseDto is not null && responseDto.Result is not null && responseDto.IsSuccess)
            {
                var coupon = JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(responseDto.Result));

                return View(coupon);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CouponDelete(CouponDto couponDto)
        {
            ResponseDto? responseDto = await _couponService.DeleteCouponAsync(couponDto.CouponId);
            if (responseDto is not null && responseDto.Result is not null && responseDto.IsSuccess)
            {
                return RedirectToAction(nameof(CouponIndex));
            }

            return View(couponDto);
        }
    }
}
