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

        private readonly int _quantity = 25;

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

        public async Task<GetQuacksResponse> GetByUser(int? startingId, string username)
        {
            try
            {
                var quacks = await QuackRepository.GetByUser(_quantity, startingId, username);

                var secondFetchResult = await QuackRepository.GetByUser(1, quacks.Last().Id, username);

                return new GetQuacksResponse(true, quacks, secondFetchResult.Any());
            }
            catch (Exception)
            {
                return new GetQuacksResponse(false, null, false);
            }
        }

        public async Task<GetQuacksResponse> GetFeed(int? startingId, int userId)
        {
            try
            {
                var quacks = await QuackRepository.GetFeed(_quantity, startingId, userId);
                var secondFetchResult = await QuackRepository.GetFeed(_quantity, quacks.Last().Id, userId);
                return new GetQuacksResponse(true, quacks, secondFetchResult.Any());
            }
            catch (Exception)
            {
                return new GetQuacksResponse(false, null, false);
            }
        }
    }
}
