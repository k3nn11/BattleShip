using System;
using Application.DTO.UserDTO;
using BattleShip.Models;

namespace Application.ModelMapper
{
    public static class UserMapper
    {
        public static GetUserDTO MapToDTO(User user)
        {
            return new GetUserDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role.ToString(),
            };
        }

        public static User PostMapToModel(PostUserDTO postUserDTO)
        {
            return new User()
            {
                FirstName = postUserDTO.FirstName,
                LastName = postUserDTO.LastName,
                Role = postUserDTO.Role,
            };
        }

        public static User PutMapToModel(PutUserDTO userDTO, User user)
        {
           user.FirstName = userDTO.FirstName;
           user.LastName = userDTO.LastName;
           user.Role = userDTO.Role;
           return user;
        }


    }
}
