using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using webapi.Dto.Users;
using webapi.Interfaces;
using webapi.Models;

namespace webapi.Controllers
{
    [Route("api/Roles/{rolesID}/User")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class UserController : ControllerBase
    {
        private readonly UserInterface _userServices;

        public UserController(UserInterface userServices)
        {
            _userServices = userServices;
        }
        
        [HttpGet]
        public ActionResult<List<UserDto>> GetAll([FromRoute] int rolesID)
        {
            var result = _userServices.GetAll(rolesID);

            return Ok(result);
        }
        
        [HttpGet("{userId}")]
        public ActionResult<UserDto> Get([FromRoute] int rolesID, [FromRoute] int userId)
        {
            UserDto user = _userServices.GetById(rolesID, userId);

            return Ok(user);

        }
       
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Post([FromRoute]int rolesID, [FromBody] CreateUserDto dto)
        {          
            var newUser = _userServices.Create(rolesID, dto);

            return Created($"/api/Roles/{rolesID}/User/{newUser}", null);
        }
        
        [HttpDelete("{userId}")]
        public ActionResult Delete([FromRoute] int rolesID, [FromRoute] int userId)
        {
              _userServices.Delete(rolesID, userId);

            return NotFound();
        }
        
        [HttpPut("{userId}")]
        public ActionResult Update([FromBody] UpdateUserDto dto, [FromRoute] int rolesID, [FromRoute] int userId)
        {          
            _userServices.Update(rolesID, userId, dto);
           
            return Ok();           
        }
       
        [HttpPost("login")]
        public ActionResult Login([FromBody]LoginDto dto, [FromRoute] int rolesID)
        {
            string token = _userServices.GenerateJWt(rolesID,dto);
            return Ok(token);
        }
    }
}