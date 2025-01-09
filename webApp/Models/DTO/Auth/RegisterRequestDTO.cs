namespace VillaAPI.Models.DTO.Auth
{
    public class RegisterRequestDTO
    {
        public string Name { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }
    }
}
