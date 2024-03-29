﻿using Application.Commands;
using WebAPI.Models;

namespace WebAPI.ModelsConverters
{
    public static class UserModelConverter
    {
        public static UserCreateCommand RegisterModelConvertToUserCreateCommand(RegisterModel model)
        {
            if (model == null)
                return null;
            return new UserCreateCommand
            {
                Email = model.Email,
                Name = model.Name,
                Phone = model.Phone,
                Address = model.Address,
                Password = model.Password
            };
        }

        public static UserModel UserCommandConvertToUserModel(UserCommand command)
        {
            if (command == null)
                return null;
            return new UserModel
            {
                Id = command.Id,
                Email = command.Email,
                Name = command.Name,
                Address = command.Address,
                Created = command.Created,
                Phone = command.Phone
            };
        }

        public static UserUpdateCommand UserUpdateModelConvertToUserUpdateCommand(UserUpdateModel model)
        {
            if (model == null)
                return null;
            return new UserUpdateCommand
            {
                Id = model.Id,
                Email = model.Email,
                Name = model.Name,
                Address = model.Address,
                Password = model.Password,
                Phone = model.Phone
            };
        }
    }
}
