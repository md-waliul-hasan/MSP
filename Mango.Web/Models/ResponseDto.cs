namespace Mango.Web.Models
{
    public class ResponseDto
    {
        public object Result { get; set; } = new object();
        public bool IsSuccess { get; set; } = true;
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
