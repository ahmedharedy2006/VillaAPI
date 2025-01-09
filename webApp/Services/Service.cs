using Newtonsoft.Json;
using System.Net;
using System.Text;
using webApp.Models;
using webApp.Models.Models.DTO;
using static WebAppUtil.SD;

namespace webApp.Services
{
    public class Service : IService
    {
        public APIResponse response { get; set; }

        public IHttpClientFactory clientFactory { get; set; }

        public Service(IHttpClientFactory httpClientFactory) 
        {
            this.response = new();

            this.clientFactory = httpClientFactory;
        }

        public async Task<T> SendAsync<T>(APIRequest request)
        {
            try
            {
                var client = clientFactory.CreateClient("APICaller");

                HttpRequestMessage message = new();

                message.Headers.Add("Accept", "application/json");

                message.RequestUri = new Uri(request.Url);

                if (request.Data != null)
                {
                    message.Content = new StringContent(
                        JsonConvert.SerializeObject(request.Data),
                        Encoding.UTF8,
                        "application/json"
                    );
                }

                switch (request.apiType)
                {
                    case ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }

                HttpResponseMessage apiResponse = null;

                apiResponse = await client.SendAsync(message);

                var apiContent = await apiResponse.Content.ReadAsStringAsync();

                var apiResponseObj = JsonConvert.DeserializeObject<T>(apiContent);

                return apiResponseObj;

            }

            catch(Exception ex)
            {
                var dto = new APIResponse
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    IsSuccess = false,
                    ErrorMessages = new List<string> { ex.Message }
                };

                var res = JsonConvert.SerializeObject(dto);

                var apiResponseObj = JsonConvert.DeserializeObject<T>(res);

                return apiResponseObj;
            }

        }
    }
}
