using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using webapi.Authentications;
using webapi.Dto.Roles;
using webapi.Exceptios;
using webapi.Interfaces;
using webapi.Models;

namespace webapi.Services
{
    public class RolesServices :IRolesInterface
    {

        private readonly AppDbContext _appDbContext;
        
        private readonly IMapper _mapper;
        public readonly ILogger<UserServices> _logger;
        public RolesServices(AppDbContext dbContext, IMapper mapper, ILogger<UserServices> logger)
        {
            _appDbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }
        public RolesDto GetById(int rolesID)
        {
            var roles = _appDbContext
           .Roles
           .FirstOrDefault(r => r.RolesID == rolesID);

            if (roles == null)
                throw new NotFoundException("Roles not found");
            
            var result = _mapper.Map<RolesDto>(roles);
           return result;
        }
        public ActionResult<IEnumerable<RolesDto>> GetAll()
        {
            var roles = _appDbContext
                    .Roles
                    .ToList();

            var rolesDto = _mapper.Map<List<RolesDto>>(roles);
            return rolesDto;
        }

        public int Create(CreateRolesDto dto)
        {
            var roles = _mapper.Map<Roles>(dto);        
            _appDbContext.Roles.Add(roles);
            _appDbContext.SaveChanges();
            return roles.RolesID;
        }
        public void Delete(int rolesID)
        {
            _logger.LogError($"User with id: {rolesID} Deleted action invoked");
            var roles = _appDbContext
             .Roles
             .FirstOrDefault(r => r.RolesID == rolesID);
              if (roles == null)
                throw new NotFoundException("Roles not found");
            _appDbContext.Roles.Remove(roles);
            _appDbContext.SaveChanges();



        }
        

       
    }
}
