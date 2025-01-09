using webApp.Models;
using webApp.Models.Models.DTO;

namespace webApp.Services
{
    public interface IService
    {
        APIResponse response { get; set; }

        Task<T> SendAsync<T>(APIRequest request);
    }
}
