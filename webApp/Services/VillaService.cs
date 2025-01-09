using webApp.Models.DTO;
using webApp.Models.Models.DTO;
using static WebAppUtil.SD;

namespace webApp.Services
{
    public class VillaService : Service, IVillaService
    {
        private readonly IHttpClientFactory _clientFactory;

        private string villaUrl;

        public VillaService(IConfiguration configuration , IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;

            villaUrl = configuration.GetValue<string>("VillaAPI:BaseUrl");
        }

        public Task<T> CreateAsync<T>(VillaCreateDTO villa)
        {
            return SendAsync<T>(new APIRequest
            {
                apiType = ApiType.POST,
                Url = $"{villaUrl}/api/v1/villa",
                Data = villa
            });
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest
            {
                apiType = ApiType.DELETE,
                Url = $"{villaUrl}/api/v1/villa"+id,
            });
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new APIRequest
            {
                apiType = ApiType.GET,
                Url = $"{villaUrl}/api/v1/villa",
            });
        }

        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest
            {
                apiType = ApiType.GET,
                Url = $"{villaUrl}/api/v1/villa" +id,
            });
        }

        public Task<T> UpdateAsync<T>(VillaUpdateDTO villa)
        {
            return SendAsync<T>(new APIRequest
            {
                apiType = ApiType.PUT,
                Url = $"{villaUrl}/api/v1/villa"+villa.Id,
                Data = villa
            });
        }
    }
}
