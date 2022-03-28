using System.Threading.Tasks;
using Quacore.Domain.Models;

namespace Quacore.Domain.Repositories
{
    public interface IProfileRepository
    {
        public Task<Profile> GetByUsername(string username);
        public void CreateProfile(Profile profile);
        void UpdateProfile(Profile profile);
    }
}