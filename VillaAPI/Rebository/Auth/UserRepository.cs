using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VillaAPI.data;
using VillaAPI.Models.Auth;
using VillaAPI.Models.DTO.Auth;
using VillaAPI.Rebository.Interfaces.Auth;

namespace VillaAPI.Rebository.Auth
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        private readonly IMapper _mapper;

        private string SecretKey;
        public UserRepository(AppDbContext context , IMapper mapper , IConfiguration configuration) 
        {
            _context = context;

            _mapper = mapper;

            SecretKey = configuration.GetSection("AppSettings:Secret").Value;
        }

        public bool isUniqueUser(string username)
        {
            var user = _context.Users.FirstOrDefault(x => x.Username == username);
            
            if (user == null)
            {
                return true;
            }

            return false;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == loginRequestDTO.Username
            && u.Password == loginRequestDTO.Password);

            if (user == null)
            {
                return new LoginResponseDTO()
                {
                    Token = "",
                    user = null
                };
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(SecretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),

                Expires = DateTime.UtcNow.AddDays(14),
                SigningCredentials = new(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256)

            };

            var Token = tokenHandler.CreateToken(tokenDescriptor);

            LoginResponseDTO loginResponseDTO = new LoginResponseDTO()
            {
                Token = tokenHandler.WriteToken(Token),
                user = user,

            };
            return loginResponseDTO;
            }

        public async Task<User> Register(RegisterRequestDTO registerRequestDTO)
        {
            var user = _mapper.Map<User>(registerRequestDTO);

             _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return user;
        }
    }
}
