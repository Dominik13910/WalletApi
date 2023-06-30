using System;
using webapi.Models;

namespace webapi.Seeders
{
    public class RolesSeeder
    {
        private readonly AppDbContext _appDbContext;

        public RolesSeeder(AppDbContext dbContext)
        {
            _appDbContext = dbContext;
        }
        public void Seed()
        {
            if (_appDbContext.Database.CanConnect())
            {
            
                if (!_appDbContext.Roles.Any())
                {
                    var roles = GetUsers();
                    _appDbContext.Roles.AddRange(roles);
                    _appDbContext.SaveChanges();
                }
            }
        }
      
        private IEnumerable<Roles> GetUsers()
        {
            var roles = new List<Roles>()
            {
                new Roles()
                {
                  RoleName = "User"
                },
                new Roles()
                {
                   RoleName = "Admin"
                },
                new Roles()
                {
                    RoleName = "Moderator"
                }
            };
            return roles;

        }
    }
}

