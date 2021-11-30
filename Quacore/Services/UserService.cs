using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Quacore.Domain.Models;
using Quacore.Domain.Repositories;
using Quacore.Domain.Responses;
using Quacore.Domain.Services;

namespace Quacore.Services
{
    public class UserService : IUserService
    {
        private ITokenService TokenService { get; }
        private IUserRepository UserRepository { get; }
        private IUnitOfWork UnitOfWork { get; }

        public UserService(
            IUserRepository userRepository, 
            IUnitOfWork unitOfWork, 
            ITokenService tokenService)
        {
            UserRepository = userRepository;
            UnitOfWork = unitOfWork;
            TokenService = tokenService;
        }

        public async Task<RegisterResponse> Register(User user)
        {
            // TODO: Zahashuj tu hasło kurła
            try
            {
                await UserRepository.Add(user);
                await UnitOfWork.Complete();
                return new RegisterResponse(true, user);
            }
            catch (Exception)
            {
                return new RegisterResponse(false, null);
            }
        }

        public async Task<LoginResponse> Login(string username, string password)
        {
            try
            {
                var user = await UserRepository.GetByCredentials(username, password);
                var token = await TokenService.CreateAccessToken(user);
                return new LoginResponse(true, token);
            }
            catch (Exception e)
            {
                return new LoginResponse(false, null);
            }
        }
    }
}
