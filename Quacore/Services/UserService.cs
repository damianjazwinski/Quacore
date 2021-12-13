using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Quacore.Domain.Helpers;
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
        private ITokenRepository TokenRepository { get; }
        private IUnitOfWork UnitOfWork { get; }

        public UserService(
            IUserRepository userRepository,
            IUnitOfWork unitOfWork,
            ITokenService tokenService, 
            ITokenRepository tokenRepository)
        {
            UserRepository = userRepository;
            UnitOfWork = unitOfWork;
            TokenService = tokenService;
            TokenRepository = tokenRepository;
        }

        public async Task<RegisterResponse> Register(User user)
        {
            user.Salt = Guid.NewGuid().ToString("N");
            var rawPassword = Encoding.UTF8.GetBytes(user.Password);
            var rawSalt = Encoding.UTF8.GetBytes(user.Salt);

            var rawHashedPassword = HasherHelper.GenerateSaltedHash(rawPassword, rawSalt);
            var hashedPassword = Convert.ToBase64String(rawHashedPassword);

            user.Password = hashedPassword;

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
                var user = await UserRepository.GetByUsername(username);
                var rawPassword = Encoding.UTF8.GetBytes(password);
                var rawSalt = Encoding.UTF8.GetBytes(user.Salt);
                var rawHash = HasherHelper.GenerateSaltedHash(rawPassword, rawSalt);
                var hash = Convert.ToBase64String(rawHash);

                if (user.Password != hash) return new LoginResponse(false, null);

                var token = await TokenService.CreateAccessToken(user);

                return new LoginResponse(true, token);
            }
            catch (Exception e)
            {
                return new LoginResponse(false, null);
            }
        }

        public async Task<LogoutResponse> Logout(string accessToken, int userId)
        {
            var response = await TokenService.AccessTokenExists(accessToken, userId);

            if (response.IsSuccess)
            {
                var token = await TokenRepository.GetToken(accessToken, userId);
                TokenRepository.Remove(token);
                await UnitOfWork.Complete();
                return new LogoutResponse(true);
            }

            return new LogoutResponse(false);
        }
    }
}
