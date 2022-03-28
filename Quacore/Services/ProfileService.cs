using System;
using System.Threading.Tasks;
using Quacore.Domain.Models;
using Quacore.Domain.Repositories;
using Quacore.Domain.Responses;
using Quacore.Domain.Services;

namespace Quacore.Services
{
    public class ProfileService : IProfileService
    {
        private IUnitOfWork UnitOfWork { get; }
        private IProfileRepository ProfileRepository { get; }
        
        public ProfileService(IUnitOfWork unitOfWork, IProfileRepository profileRepository)
        {
            UnitOfWork = unitOfWork;
            ProfileRepository = profileRepository;
        }
        
        public async Task<GetProfileResponse> GetByUsernameAsync(string username)
        {
            try
            {
                var profile = await ProfileRepository.GetByUsername(username);
                return new GetProfileResponse(true, profile);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new GetProfileResponse(false, null);
            }
        }

        public async Task<StatusResponse> CreateProfile(Profile profile)
        {
            try
            {
                ProfileRepository.CreateProfile(profile);
                await UnitOfWork.Complete();
                return new StatusResponse(true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new StatusResponse(false);
            }
        }

        public async Task<StatusResponse> UpdateProfile(Profile profile)
        {
            try
            {
                ProfileRepository.UpdateProfile(profile);
                await UnitOfWork.Complete();
                return new StatusResponse(true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new StatusResponse(false);
            }
        }

    }
}