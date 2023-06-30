using System.ComponentModel.DataAnnotations;

namespace webapi.Dto.Users
{
    public class CreateUserDto
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Nationality { get; set; }
        public DateTime? DataOdBirdth { get; set; }

        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

    }
}
