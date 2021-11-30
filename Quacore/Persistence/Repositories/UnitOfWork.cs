using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Quacore.Domain.Repositories;
using Quacore.Persistence.Contexts;

namespace Quacore.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        protected QuacoreDbContext Context { get; }

        public UnitOfWork(QuacoreDbContext context)
        {
            Context = context;
        }
        public async Task Complete()
        {
            await Context.SaveChangesAsync();
        }
    }
}
