using Quacore.Domain.Models;
using Quacore.Domain.Repositories;
using Quacore.Domain.Responses;
using Quacore.Domain.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quacore.Services
{
    public class QuackService : IQuackService
    {
        private IUnitOfWork UnitOfWork { get; }
        private IQuackRepository QuackRepository { get; }

        public QuackService(IUnitOfWork unitOfWork, IQuackRepository quackRepository)
        {
            UnitOfWork = unitOfWork;
            QuackRepository = quackRepository;
        }

        public async Task<StatusResponse> Add(Quack quack)
        {
            try
            {
                quack.CreatedAt = DateTime.UtcNow;
                await QuackRepository.Add(quack);
                await UnitOfWork.Complete();
                return new StatusResponse(true);
            }
            catch (Exception)
            {
                return new StatusResponse(false);
            }
        }

        public Task<StatusResponse> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<GetQuackResponse> Get(int id)
        {
            try
            {
                var quack = await QuackRepository.GetById(id);
                return new GetQuackResponse(true, quack);
            }
            catch (Exception)
            {
                return new GetQuackResponse(false, null);
            }
        }

        public async Task<GetQuacksResponse> GetByUser(int userId)
        {
            try
            {
                var quacks = await QuackRepository.GetByUser(userId);
                return new GetQuacksResponse(true, quacks);
            }
            catch (Exception)
            {
                return new GetQuacksResponse(false, null);
            }
        }
        public async Task<GetQuacksResponse> GetByUsers(IEnumerable<int> userIds)
        {
            try
            {
                var quacks = await QuackRepository.GetByUsers(userIds);
                return new GetQuacksResponse(true, quacks);
            }
            catch (Exception)
            {
                return new GetQuacksResponse(false, null);
            }
        }
    }
}
