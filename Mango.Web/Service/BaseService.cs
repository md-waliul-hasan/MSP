using Mango.Web.Models;
using Mango.Web.Service.IService;
using Newtonsoft.Json;
using System.Text;

namespace Mango.Web.Service
{
    public class BaseService : IBaseService
    {
        #region Members

        private readonly IHttpClientFactory _httpClientFactory;

        #endregion

        #region Ctor

        public BaseService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        #endregion

        #region Methods 

        public async Task<ResponseDto?> SendAsync(RequestDto requestDto)
        {
            try
            {
                HttpClient client = _httpClientFactory.CreateClient("MangoApi");
                HttpRequestMessage httpRequestMessage = new();
                httpRequestMessage.Headers.Add("Accept", "application/json");

                httpRequestMessage.RequestUri = new Uri(requestDto.Url);
                if (requestDto.Data is not null)
                {
                    httpRequestMessage.Content = new StringContent(
                        content: JsonConvert.SerializeObject(requestDto.Data),
                        encoding: Encoding.UTF8,
                        mediaType: "application/json");
                }

                SetMethod(requestDto, httpRequestMessage);

                HttpResponseMessage? httpResponseMessage = await client.SendAsync(httpRequestMessage);
                return await SetHttpResponseMessage(httpResponseMessage);
            }
            catch (Exception ex)
            {
                return new ResponseDto()
                {
                    ErrorMessage = ex.Message,
                    IsSuccess = false,
                };
            }

        }

        private static async Task<ResponseDto?> SetHttpResponseMessage(HttpResponseMessage httpResponseMessage)
        {
            switch (httpResponseMessage.StatusCode)
            {
                case System.Net.HttpStatusCode.Unauthorized:
                    return new() { IsSuccess = false, ErrorMessage = "Unauthorized" };
                case System.Net.HttpStatusCode.Forbidden:
                    return new() { IsSuccess = false, ErrorMessage = "Access Denied" };
                case System.Net.HttpStatusCode.NotFound:
                    return new() { IsSuccess = false, ErrorMessage = "Not Found" };
                case System.Net.HttpStatusCode.InternalServerError:
                    return new() { IsSuccess = false, ErrorMessage = "Internal Server Error" };
                default:
                    var apiContent = await httpResponseMessage.Content.ReadAsStringAsync();
                    var apiResponseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
                    return apiResponseDto;
            }
        }

        private static void SetMethod(RequestDto requestDto, HttpRequestMessage httpRequestMessage)
        {
            switch (requestDto.ApiType)
            {
                case Enums.ApiType.GET:
                    httpRequestMessage.Method = HttpMethod.Get;
                    break;
                case Enums.ApiType.POST:
                    httpRequestMessage.Method = HttpMethod.Post;
                    break;
                case Enums.ApiType.PUT:
                    httpRequestMessage.Method = HttpMethod.Put;
                    break;
                case Enums.ApiType.DELETE:
                    httpRequestMessage.Method = HttpMethod.Delete;
                    break;
                default:
                    break;
            }
        }

        #endregion
    }
}
