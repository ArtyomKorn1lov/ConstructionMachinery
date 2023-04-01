using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.IServices;
using Application.Commands;
using Application.CommandsConverters;
using Domain.Entities;
using Domain.IRepositories;

namespace Application.Services
{
    public class MailService : IMailService
    {
        private IMailRepository _mailRepository;

        public MailService(IMailRepository mailRepository)
        {
            _mailRepository = mailRepository;
        }

        public async Task<bool> SaveMail(MailCommandCreate mail)
        {
            try
            {
                if (mail == null)
                    return false;
                mail.Created = DateTime.Now;
                await _mailRepository.SaveMail(MailCommandConverter.MailCommandConvertToEntity(mail));
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
