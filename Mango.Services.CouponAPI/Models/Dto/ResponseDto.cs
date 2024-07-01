namespace Mango.Services.CouponAPI.Models.Dto
{
    public class ResponseDto
    {
        public object Result { get; set; } = new object();
        public bool IsSuccess { get; set; } = true;
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
