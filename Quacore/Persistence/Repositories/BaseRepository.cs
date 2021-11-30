using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Quacore.Persistence.Contexts;

namespace Quacore.Persistence.Repositories
{
    public class BaseRepository
    {
        protected QuacoreDbContext Context { get; }

        public BaseRepository(QuacoreDbContext context)
        {
            Context = context;
        }
    }
}
