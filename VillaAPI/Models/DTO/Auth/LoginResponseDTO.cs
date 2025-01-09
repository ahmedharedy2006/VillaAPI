using VillaAPI.Models.Auth;

namespace VillaAPI.Models.DTO.Auth
{
    public class LoginResponseDTO
    {
        public User user {  get; set; }

        public string Token { get; set; }
    }
}
