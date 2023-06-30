using System.ComponentModel.DataAnnotations;
using System.Data;

namespace webapi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Nationality { get; set; }
        public DateTime? DataOdBirdth { get; set; }
        public Wallet Wallet { get; set; }
        public Roles Roles { get; set; }
        public int RolesID { get; set; }
        

    }
}
