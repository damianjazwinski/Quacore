using Microsoft.EntityFrameworkCore;

using Quacore.Domain.Models;
using Quacore.Domain.Repositories;
using Quacore.Persistence.Contexts;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quacore.Persistence.Repositories
{
    public class QuackRepository : BaseRepository, IQuackRepository
    {
        public QuackRepository(QuacoreDbContext context) : base(context)
        {
        }

        public async Task Add(Quack quack)
        {
            await Context.AddAsync(quack);
        }

        public async Task<IEnumerable<Quack>> GetAll()
        {
            return await Context.Quacks
                .ToListAsync();
        }

        public async Task<Quack> GetById(int id)
        {
            return await Context.Quacks
                .FindAsync(id);
        }

        public async Task<IEnumerable<Quack>> GetByUser(int quantitity, int? startingId, int userId)
        {
            return await Context.Quacks
                .Where(q => q.UserId == userId)
                .Include(q => q.User)
                .OrderByDescending(q => q.CreatedAt)
                .ToListAsync();
        }        

        public async Task<IEnumerable<Quack>> GetByUser(int quantitity, int? startingId, string username)
        {
            var query = Context.Quacks
                .Include(q => q.User)
                .Include(q => q.Mentions)
                    .ThenInclude(m => m.User)
                .Where(q => q.User.Username == username)
                .OrderByDescending(q => q.CreatedAt);
                

            if (startingId != null)
            {
                var quack = await Context.Quacks.FindAsync(startingId);
                var numberToSkip = await Context.Quacks.Where(q => q.CreatedAt > quack.CreatedAt && q.User.Username == username).CountAsync() + 1;
                return query.Skip(numberToSkip).Take(quantitity).ToList();
            }

            return query.Take(quantitity).ToList();
        }


        public async Task<IEnumerable<Quack>> GetFeed(int quantitity, int? startingId, int userId)
        {
            var query = Context.Quacks
                .Where(q => q.UserId == userId)
                .Include(q => q.User)
                .Include(q => q.Mentions)
                    .ThenInclude(m => m.User)
                .OrderByDescending(q => q.CreatedAt);

            if (startingId != null)
            {
                var quack = await Context.Quacks.FindAsync(startingId);
                var numberToSkip = await Context.Quacks.Where(q => q.CreatedAt > quack.CreatedAt && q.UserId == userId).CountAsync() + 1;
                return query.Skip(numberToSkip).Take(quantitity).ToList();
            }

            return query.Take(quantitity).ToList();
        }

        public Task Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
