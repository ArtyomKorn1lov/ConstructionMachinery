using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Commands;
using Domain.Entities;

namespace Application.CommandsConverters
{
    public static class MailCommandConverter
    {
        public static Mail MailCommandConvertToEntity(MailCommandCreate mail)
        {
            if (mail == null)
                return null;
            return new Mail
            {
                Created = mail.Created,
                Name = mail.Name,
                Email = mail.Email,
                Phone = mail.Phone,
                Description = mail.Description
            };
        }
    }
}
