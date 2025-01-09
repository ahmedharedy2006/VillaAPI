using VillaAPI.Models.Auth;
using VillaAPI.Models.DTO.Auth;

namespace VillaAPI.Rebository.Interfaces.Auth
{
    public interface IUserRepository
    {
        bool isUniqueUser(string username);
        
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);

        Task<User> Register(RegisterRequestDTO registerRequestDTO);
    }
}
