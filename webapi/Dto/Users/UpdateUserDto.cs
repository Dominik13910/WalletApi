using System.ComponentModel.DataAnnotations;

namespace webapi.Dto.Users
{
    public class UpdateUserDto
    {

        public string Name { get; set; }

        public string Email { get; set; }

        [MaxLength(200)]
        public string Password { get; set; }

    }
}
