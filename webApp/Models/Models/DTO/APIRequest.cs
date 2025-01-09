using static WebAppUtil.SD;

namespace webApp.Models.Models.DTO
{
    public class APIRequest
    {
        public ApiType apiType { get; set; } = ApiType.GET;

        public string Url { get; set; }

        public object Data { get; set; }
    }
}
