using webApp.Models.Auth;

namespace webApp.Models.DTO.Auth
{
    public class LoginResponseDTO
    {
        public User user {  get; set; }

        public string Token { get; set; }
    }
}
