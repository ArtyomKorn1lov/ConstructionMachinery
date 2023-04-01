using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.IRepositories;

namespace Infrastructure.Repositories
{
    public class MailRepository : IMailRepository
    {
        private ConstructionMachineryDbContext _constructionMachineryDbContext;

        public MailRepository(ConstructionMachineryDbContext constructionMachineryDbContext)
        {
            _constructionMachineryDbContext = constructionMachineryDbContext;
        }

        public async Task SaveMail(Mail mail)
        {
            await _constructionMachineryDbContext.Set<Mail>().AddAsync(mail);
        }
    }
}
