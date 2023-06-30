using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using webapi.Authentications;
using webapi.Dto;
using webapi.Dto.Users;
using webapi.Exceptios;
using webapi.Interfaces;
using webapi.Models;

namespace webapi.Services
{

    public class UserServices : UserInterface
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        public readonly ILogger<UserServices> _logger;
        public readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSetting _authenticationSetting;
        public UserServices(AppDbContext dbContext, IMapper mapper,ILogger<UserServices> logger, IPasswordHasher<User> passwordHasher, AuthenticationSetting authenticationSetting)
        {
            _appDbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            _passwordHasher = passwordHasher;
            _authenticationSetting = authenticationSetting;
        }
        public UserDto GetById(int rolesID, int userId)
        {
            var roles = GetRolesById(rolesID);
            var user = GetUserById(userId);
            var result = _mapper.Map<UserDto>(user);
           return result;
        }
        public List<UserDto> GetAll(int rolesID)
        {
            var roles = GetRolesById(rolesID);
            var usersDto = _mapper.Map<List<UserDto>>(roles.User);
            return usersDto;
        }

        public int Create(int rolesID, CreateUserDto dto)
        {
            var roles = GetRolesById(rolesID);
            var user = _mapper.Map<User>(dto);
            var paswordHasher = _passwordHasher.HashPassword(user, dto.Password);
            user.Password = paswordHasher;
            user.RolesID = rolesID;
            _appDbContext.User.Add(user);
            _appDbContext.SaveChanges();
            return user.Id;
        }
        public void Delete(int rolesID, int userId)
        {
            _logger.LogError($"User with id: {userId} Deleted action invoked");
            var roles = GetRolesById(rolesID);
            var user = GetUserById(userId);
            _appDbContext.User.Remove(user);
            _appDbContext.SaveChanges();

           
       
        }
        public void Update( int rolesID, int userId,UpdateUserDto dto)
        {
            var roles = GetRolesById(rolesID);
            var user = GetUserById(userId);
            user.Name = dto.Name;
            user.Email = dto.Email;
            var paswordHasher = _passwordHasher.HashPassword(user, dto.Password);
            user.Password = paswordHasher;

            _appDbContext.SaveChanges ();     
        }
        private Roles GetRolesById(int rolesID)
        {
            var roles = _appDbContext
                .Roles
                .Include(u => u.User)
                .FirstOrDefault(u => u.RolesID == rolesID);

            if (roles is null)
                throw new NotFoundException("Role not found");

            return roles;
        }
        private User GetUserById(int userId)
        {
           var user = _appDbContext
            .User
            .FirstOrDefault(u => u.Id == userId);
            if (user is null)
                throw new NotFoundException("User not found");
            return user;
        }

        public string GenerateJWt( int rolesID, LoginDto dto)
        {
            var roles = GetRolesById(rolesID);
            var user = _appDbContext
                .User
                .Include(u => u.Roles)
                .FirstOrDefault(u => u.Email == dto.Email);

            if (user == null)
            {
                throw new Microsoft.AspNetCore.Http.BadHttpRequestException("Invalid user name or password");
            }
            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, dto.Password);
           
            if (result == PasswordVerificationResult.Failed)
            {
                throw new Microsoft.AspNetCore.Http.BadHttpRequestException("Invalid user name or password");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.Name}"),
                new Claim (ClaimTypes.Role, $"{user.Roles.RoleName}"),
                new Claim("DateOfBirdth", user.DataOdBirdth.Value.ToString("yyyy-MM-dd")),
                    new Claim("Nationality", user.Nationality)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSetting.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSetting.JwtExpireDays);

            var token = new JwtSecurityToken(_authenticationSetting.JwtIssuer,
                _authenticationSetting.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);



        }


    }
    
}
