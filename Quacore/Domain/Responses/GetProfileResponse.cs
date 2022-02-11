using Quacore.Domain.Models;

namespace Quacore.Domain.Responses
{
    public class GetProfileResponse : BaseResponse
    {
        public Profile Profile { get; }
        public GetProfileResponse(bool isSuccess, Profile profile) : base(isSuccess)
        {
            Profile = profile;
        }
    }
}