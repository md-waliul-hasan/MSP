using AutoMapper;
using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponAPI.Controllers
{
    [Route("api/coupon")]
    [ApiController]
    public class CouponApiController : ControllerBase
    {
        #region Members

        private readonly AppDbContext _appDbContext;
        private readonly ResponseDto _response;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public CouponApiController(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _response = new ResponseDto();
            _mapper = mapper;
        }

        #endregion

        #region Methods

        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                var objResult = _appDbContext.Coupons.ToList();
                _response.Result = _mapper.Map<IEnumerable<CouponDto>>(objResult);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = ex.Message;
            }

            return _response;
        }

        [HttpGet]
        [Route("{id}")]
        public ResponseDto GetById(int id)
        {
            try
            {
                var obj = _appDbContext.Coupons.First(x => x.CouponId == id);
                _response.Result = _mapper.Map<CouponDto>(source: obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = ex.Message;
            }

            return _response;
        }

        [HttpGet]
        [Route("GetByCode/{couponCode}")]
        public ResponseDto GetByCode(string couponCode)
        {
            try
            {
                Coupon? coupon = _appDbContext.Coupons.FirstOrDefault(u => u.CouponCode.ToLower() == couponCode.ToLower());

                _response.Result = _mapper.Map<CouponDto>(source: coupon);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = ex.Message;
            }

            return _response;
        }

        [HttpPost]
        public ResponseDto AddCoupon([FromBody] CouponDto coupon)
        {
            try
            {
                Coupon repoCoupon = _mapper.Map<Coupon>(coupon);
                _appDbContext.Coupons.Add(repoCoupon);
                _appDbContext.SaveChanges();

                _response.Result = _mapper.Map<CouponDto>(repoCoupon);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = ex.Message;
            }
            return _response;
        }

        [HttpPut]
        public ResponseDto UpdateCoupon([FromBody] CouponDto coupon)
        {
            try
            {
                Coupon repoCoupon = _mapper.Map<Coupon>(coupon);
                _appDbContext.Coupons.Update(repoCoupon);
                _appDbContext.SaveChanges();

                _response.Result = _mapper.Map<CouponDto>(repoCoupon);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = ex.Message;
            }
            return _response;
        }

        [HttpDelete]
        [Route("{couponId:int}")]
        public ResponseDto DeleteCoupon(int couponId)
        {
            try
            {
                Coupon repoCoupon = _appDbContext.Coupons.First(x => x.CouponId == couponId);
                _appDbContext.Coupons.Remove(repoCoupon);
                _appDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = ex.Message;
            }
            return _response;
        }

        #endregion
    }
}
