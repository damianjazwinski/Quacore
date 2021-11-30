using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Quacore.Domain.Models;

namespace Quacore.Domain.Responses
{
    public class RegisterResponse : BaseResponse
    {
        public User User { get; }
        public RegisterResponse(bool isSuccess, User user) : base(isSuccess)
        {
            User = user;
        }
    }
}
