using Quacore.Domain.Models;
using Quacore.Domain.Responses;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quacore.Domain.Services
{
    public interface IQuackService
    {
        public Task<StatusResponse> Add(Quack quack);
        public Task<GetQuackResponse> Get(int id);
        public Task<StatusResponse> Delete(int id);
        public Task<GetQuacksResponse> GetByUser(int userId);
        public Task<GetQuacksResponse> GetByUsers(IEnumerable<int> userIds);
    }
}
