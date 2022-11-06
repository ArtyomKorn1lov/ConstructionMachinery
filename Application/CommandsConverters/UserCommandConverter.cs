using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Application.Commands;

namespace Application.CommandsConverters
{
    public static class UserCommandConverter
    {
        public static User UserCreateCommandConvertToUserEntity(UserCreateCommand user)
        {
            if (user == null)
                return null;
            return new User
            {
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone,
                Password = user.Password
            };
        }
        public static UserCommand UserEntityConvertToUserCommand(User user)
        {
            if (user == null)
                return null;
            return new UserCommand
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone
            };
        }
        public static User UserUpdateCommandConvertToUserEntity(UserUpdateCommand user)
        {
            if (user == null)
                return null;
            return new User
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone,
                Password = user.Password
            };
        }

        public static UserTokenCommand UserEntityConvertToUserToken(User user)
        {
            if (user == null)
            {
                return null;
            }
            return new UserTokenCommand
            {
                Id = user.Id,
                Email = user.Email,
                RefreshToken = user.RefreshToken,
                RefreshTokenExpiryTime = user.RefreshTokenExpiryTime
            };
        }
    }
}
