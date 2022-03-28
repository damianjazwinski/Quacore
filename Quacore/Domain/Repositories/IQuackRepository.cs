using Quacore.Domain.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quacore.Domain.Repositories
{
    public interface IQuackRepository
    {
        public Task<Quack> GetById(int quackId);
        public Task<IEnumerable<Quack>> GetAll();
        public Task<IEnumerable<Quack>> GetByUser(int quantitity, int? startingId, int userId);
        public Task<IEnumerable<Quack>> GetByUser(int quantitity, int? startingId, string username);
        public Task<IEnumerable<Quack>> GetFeed(int quantitity, int? startingId, int userId);
        public Task Add(Quack quack);
        public Task Remove(int id);
    }
}
