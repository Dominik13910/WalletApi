using Microsoft.AspNetCore.Mvc;
using webapi.Dto.Roles;
using webapi.Models;

namespace webapi.Interfaces
{
    public interface IRolesInterface
    {
        public RolesDto GetById(int id);
        public ActionResult<IEnumerable<RolesDto>> GetAll();
        public int Create(CreateRolesDto dto);
        public void Delete(int id);




    }
}
