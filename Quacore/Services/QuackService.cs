using Quacore.Domain.Models;
using Quacore.Domain.Repositories;
using Quacore.Domain.Responses;
using Quacore.Domain.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Quacore.Services
{
    public class QuackService : IQuackService
    {
        private IUnitOfWork UnitOfWork { get; }
        private IQuackRepository QuackRepository { get; }
        private IUserRepository UserRepository { get; }

        public QuackService(IUnitOfWork unitOfWork, IQuackRepository quackRepository, IUserRepository userRepository)
        {
            UnitOfWork = unitOfWork;
            QuackRepository = quackRepository;
            UserRepository = userRepository;
        }

        public async Task<StatusResponse> Add(Quack quack)
        {
            try
            {
                quack.CreatedAt = DateTime.UtcNow;
                var matches = Regex.Matches(quack.Content, @"(?<=\s|^|,|;)@\w+(?=\s|$|;|,|\.)");
                var matchesStrings = matches.Select(x => x.Value).Distinct().ToList();
                var mentions = new List<Mention>();

                foreach (var matchUsername in matchesStrings)
                {
                    try
                    {
                        var user = await UserRepository.GetByUsername(matchUsername[1..]);
                        mentions.Add(new Mention() { User = user });
                    }
                    catch (Exception) { }
                }
                quack.Mentions = mentions;
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

        public async Task<GetQuacksResponse> GetByUser(string username)
        {
            try
            {
                var quacks = await QuackRepository.GetByUser(username);
                return new GetQuacksResponse(true, quacks);
            }
            catch (Exception)
            {
                return new GetQuacksResponse(false, null);
            }
        }

        public async Task<GetQuacksResponse> GetByUsers(IEnumerable<string> usernames)
        {
            try
            {
                var quacks = await QuackRepository.GetByUsers(usernames);
                return new GetQuacksResponse(true, quacks);
            }
            catch (Exception)
            {
                return new GetQuacksResponse(false, null);
            }
        }

        public async Task<GetQuacksResponse> GetFeed(int? startingId, int userId)
        {
            try
            {
                var quacks = await QuackRepository.GetFeed(25, startingId, userId);
                return new GetQuacksResponse(true, quacks);
            }
            catch (Exception)
            {
                return new GetQuacksResponse(false, null);
            }
        }
    }
}
