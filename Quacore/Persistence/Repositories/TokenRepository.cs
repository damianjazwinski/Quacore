using Quacore.Domain.Models;
using Quacore.Domain.Repositories;
using Quacore.Persistence.Contexts;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Quacore.Persistence.Repositories
{
    public class TokenRepository : BaseRepository, ITokenRepository
    {
        public TokenRepository(QuacoreDbContext context) : base(context)
        {
        }

        public async Task Add(AccessToken accessToken, RefreshToken refreshToken)
        {
            await Context.AccessTokens.AddAsync(accessToken);
            await Context.RefreshTokens.AddAsync(refreshToken);
        }

        public (AccessToken, RefreshToken) GetTokens(string accessToken, string refreshToken)
        {
            var at = Context.AccessTokens
                .Where(x => x.Token == accessToken)
                .Single();

            var rt = Context.RefreshTokens
                .Include(u => u.User)
                .Where(x => x.Token == refreshToken)
                .Single();

            return (at, rt);
        }

        public void Remove(string accessToken, string refreshToken)
        {
            Context.AccessTokens.Remove(Context.AccessTokens.SingleOrDefault(x => x.Token == accessToken));
            Context.RefreshTokens.Remove(Context.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken));
        }
    }
}
