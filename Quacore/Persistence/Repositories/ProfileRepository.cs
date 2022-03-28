using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Quacore.Domain.Models;
using Quacore.Domain.Repositories;
using Quacore.Persistence.Contexts;

namespace Quacore.Persistence.Repositories
{
    public class ProfileRepository : BaseRepository, IProfileRepository
    {
        public ProfileRepository(QuacoreDbContext context) : base(context)
        {
        }

        public async Task<Profile> GetByUsername(string username)
        {
            return await Context.Profiles
                .Include(x => x.User)
                .SingleOrDefaultAsync(x => x.User.Username == username);

        }

        public void CreateProfile(Profile profile)
        {
            Context.Profiles.Add(profile);
        }

        public void UpdateProfile(Profile profile)
        {
            Context.Profiles.Update(profile);
        }
    }
}