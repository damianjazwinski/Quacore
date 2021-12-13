using Quacore.Domain.Models;
using Quacore.Domain.Repositories;
using Quacore.Persistence.Contexts;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace Quacore.Persistence.Repositories
{
    public class TokenRepository : BaseRepository, ITokenRepository
    {
        public TokenRepository(QuacoreDbContext context) : base(context)
        {
        }

        public void Add(Token token)
        {
            Context.Tokens.Add(token);
        }

        public async Task<Token> GetToken(string accessToken, int userId)
        {
            return await Context.Tokens.Include(x => x.User).SingleOrDefaultAsync(t => t.AccessToken == accessToken && t.User.Id == userId);
        }

        public async Task<Token> GetToken(string refreshToken)
        {
            return await Context.Tokens.Include(x => x.User).SingleOrDefaultAsync(t => t.RefreshToken == refreshToken);
        }

        public void Remove(Token token)
        {
            Context.Tokens.Remove(token);
        }
    }
}
