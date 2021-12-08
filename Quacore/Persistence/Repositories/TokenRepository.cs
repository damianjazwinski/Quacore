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

        public async Task<Token> GetTokenByAccessTokenString(string accessToken)
        {
            return await Context.Tokens.Include(x => x.User).SingleOrDefaultAsync(t => t.AccessToken == accessToken);
        }

        public async Task<Token> GetTokenByRefreshTokenString(string refreshToken)
        {
            return await Context.Tokens.Include(x => x.User).SingleOrDefaultAsync(t => t.RefreshToken == refreshToken);
        }

        public void Remove(Token token)
        {
            Context.Tokens.Remove(token);
        }
    }
}
