using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Quacore.Domain.Models;

namespace Quacore.Domain.Repositories
{
    public interface IUserRepository
    {
        public Task<User> GetById(int id);
        public Task<IEnumerable<User>> GetAll();
        public Task Add(User user);
        public Task<User> GetByUsername(string username);
    }
}
