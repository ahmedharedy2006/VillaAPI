using Microsoft.AspNetCore.Mvc;
using System.Net;
using VillaAPI.Models;
using VillaAPI.Models.Auth;
using VillaAPI.Models.DTO.Auth;
using VillaAPI.Rebository.Interfaces.Auth;

namespace VillaAPI.Controllers.V1
{
    [Route("api/UserAuth")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserRepository _userRepository;
        protected APIResponse _response;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _response = new();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO model)
        {
            var isUnique = _userRepository.isUniqueUser(model.Username);
            if (!isUnique)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>()
                {
                    "Username already exists"
                };
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
            var user = await _userRepository.Register(model);
            if (user == null)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>()
                {
                    "Something went wrong"
                };
                _response.StatusCode = HttpStatusCode.InternalServerError;
                return BadRequest(_response);
            }
            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            return Ok(_response);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
        {
            var result = await _userRepository.Login(model);
            if (result.user == null || string.IsNullOrEmpty(result.Token))
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>()
                {
                    "Invalid Username or Password"
                };
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            _response.Result = result;
            return Ok(_response);
        }
    }
}
