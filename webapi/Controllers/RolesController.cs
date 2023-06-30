using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webapi.Dto.Roles;
using webapi.Dto.Users;
using webapi.Interfaces;

namespace webapi.Controllers
{
    [Route("api/Roles")]
    [ApiController]
    [Authorize (Roles = "Moderator")]
    public class RolesController : ControllerBase
    {
        private readonly IRolesInterface _rolesSevices;

        public RolesController(IRolesInterface rolesServices)
        {
            _rolesSevices = rolesServices;
        }

        [HttpGet]
        public ActionResult<IEnumerable<RolesDto>> GetAll()
        {
            var rolesDto = _rolesSevices.GetAll();

            return Ok(rolesDto);
        }

        [HttpGet("{rolesID}")]
        public ActionResult<UserDto> Get([FromRoute] int rolesID)
        {
            var roles = _rolesSevices.GetById(rolesID);

            return Ok(roles);

        }

        [HttpPost]
        public ActionResult CreeateRoles([FromBody] CreateRolesDto dto)
        {
            var rolesID = _rolesSevices.Create(dto);

            return Created($"/api/Roles/{rolesID}", null);
        }

        [HttpDelete("{rolesID}")]
        public ActionResult Delete([FromRoute] int rolesID)
        {
            _rolesSevices.Delete(rolesID);

            return NotFound();
        }
      
    }
}
