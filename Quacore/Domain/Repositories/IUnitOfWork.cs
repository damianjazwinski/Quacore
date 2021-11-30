using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quacore.Domain.Repositories
{
    public interface IUnitOfWork
    {
        public Task Complete();
    }
}
