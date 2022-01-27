﻿using Microsoft.EntityFrameworkCore;

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

        public async Task<IEnumerable<Quack>> GetByUser(int userId)
        {
            return await Context.Quacks
                .Where(q => q.UserId == userId)
                .Include(q => q.User)
                .OrderByDescending(q => q.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Quack>> GetByUsers(IEnumerable<int> userIds)
        {
            return await Context.Quacks
                .Where(q => userIds.Contains(q.UserId))
                .Include(q => q.User)
                .OrderByDescending(q => q.CreatedAt)
                .ToListAsync();
        }

        public Task Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
