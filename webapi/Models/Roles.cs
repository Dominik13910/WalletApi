namespace webapi.Models
{
    public class Roles
    {
        public int RolesID { get; set; }
        public string RoleName { get; set; }
        public virtual List<User> User { get; set; }

        
    }
}
