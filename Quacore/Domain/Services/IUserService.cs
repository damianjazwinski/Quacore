using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Quacore.Domain.Models;
using Quacore.Domain.Responses;

namespace Quacore.Domain.Services
{
    public interface IUserService
    {
        public Task<RegisterResponse> Register(User user);
        public Task<LoginResponse> Login(string username, string password);
    }
}
