using System.Threading.Tasks;
using Quacore.Domain.Models;
using Quacore.Domain.Responses;

namespace Quacore.Domain.Services
{
    public interface IProfileService
    {
        public Task<GetProfileResponse> GetByUsernameAsync(string username);
        public Task<StatusResponse> CreateProfile(Profile profile);
    }
}