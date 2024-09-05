using Mango.Web.Enums;
using Mango.Web.Models;
using Mango.Web.Service.IService;
using Mango.Web.Utility;

namespace Mango.Web.Service
{
    public class CouponService : ICouponService
    {
        IBaseService _baseService;

        public CouponService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> CreateCouponAsync(CouponDto couponDto)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiType.POST,
                Data = couponDto,
                Url = StaticDetails.CouponBaseUrl + "/api/coupon",
            });
        }

        public async Task<ResponseDto?> DeleteCouponAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiType.DELETE,
                Url = StaticDetails.CouponBaseUrl + "/api/coupon/GetByCode/" + id,
            });
        }

        public async Task<ResponseDto?> GetAllCouponsAsync()
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiType.GET,
                Url = StaticDetails.CouponBaseUrl + "/api/coupon",
            });
        }

        public async Task<ResponseDto?> GetCouponAsync(string couponCode)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiType.GET,
                Url = StaticDetails.CouponBaseUrl + "/api/coupon/GetByCode/" + couponCode,
            });
        }

        public async Task<ResponseDto?> GetCouponsByIdAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiType.GET,
                Url = StaticDetails.CouponBaseUrl + "/api/coupon/GetByCode/" + id,
            });
        }

        public async Task<ResponseDto?> UpdateCouponsAsync(CouponDto couponDto)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiType.PUT,
                Data = couponDto,
                Url = StaticDetails.CouponBaseUrl + "/api/coupon",
            });
        }
    }
}
