using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Quacore.Domain.Models;
using Quacore.Domain.Repositories;
using Quacore.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Quacore.Persistence.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(QuacoreDbContext context) : base(context) { }

        public async Task Add(User user)
        {
            await Context.AddAsync(user);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await Context.Users
                .ToListAsync();
        }

        public async Task<User> GetByUsername(string username)
        {
            return await Context.Users
                .Where(u => u.Username == username)
                .Include(u => u.Mentions)
                .SingleAsync();
        }

        public async Task<User> GetById(int id)
        {
            return await Context.Users
                .FindAsync(id);
        }
    }
}
