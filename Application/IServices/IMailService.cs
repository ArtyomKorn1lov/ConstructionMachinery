using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Commands;

namespace Application.IServices
{
    public interface IMailService
    {
        Task<bool> SaveMail(MailCommandCreate mail);
    }
}
