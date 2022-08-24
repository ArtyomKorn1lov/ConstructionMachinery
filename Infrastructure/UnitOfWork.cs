using Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private ConstructionMachineryDbContext _constructionMachineryDbContext;

        public UnitOfWork(ConstructionMachineryDbContext constructionMachineryDbContext)
        {
            _constructionMachineryDbContext = constructionMachineryDbContext;
        }

        public async Task Commit()
        {
            await _constructionMachineryDbContext.SaveChangesAsync();
        }
    }
}
