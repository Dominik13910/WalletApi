using Microsoft.AspNetCore.Mvc;
using webapi.Dto;
using webapi.Dto.Users;
using webapi.Models;

namespace webapi.Interfaces
{
    public interface UserInterface
    {
        UserDto GetById(int rolesId, int userId);
        List<UserDto> GetAll(int rolesID);
        int Create(int rolesId, CreateUserDto dto);
        void Delete(int rolesID, int userId);
        void Update(int rolesID, int userId, UpdateUserDto dto);
        string GenerateJWt(int rolesID, LoginDto dto);


    }
}
