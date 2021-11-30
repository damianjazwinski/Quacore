using AutoMapper;

using Microsoft.Extensions.Configuration;

using Quacore.Domain.Helpers;
using Quacore.Domain.JwtTokens;
using Quacore.Domain.Models;
using Quacore.Domain.Repositories;
using Quacore.Domain.Responses;
using Quacore.Domain.Services;

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Quacore.Services
{
    public class TokenService : ITokenService
    {
        private SigningConfigurationHelper SigningConfiguration { get; }
        private IConfiguration Configuration { get; }
        private ITokenRepository TokenRepository { get; }
        private IMapper Mapper { get; }
        private IUnitOfWork UnitOfWork { get; }

        public TokenService(
            SigningConfigurationHelper signingConfiguration,
            IConfiguration configuration,
            ITokenRepository tokenRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            SigningConfiguration = signingConfiguration;
            Configuration = configuration;
            TokenRepository = tokenRepository;
            Mapper = mapper;
            UnitOfWork = unitOfWork;
        }

        public async Task<JwtAccessToken> CreateAccessToken(User user)
        {
            var accessTokenExpiration = DateTime.UtcNow.AddSeconds(double.Parse(Configuration["Jwt:AccessTokenExpiration"]));
            var securityToken = new JwtSecurityToken(
                issuer: Configuration["Jwt:Issuer"],
                audience: Configuration["Jwt:Audience"],
                claims: new List<Claim>() { new Claim("User", user.Id.ToString()) },
                expires: accessTokenExpiration,
                notBefore: DateTime.UtcNow,
                signingCredentials: SigningConfiguration.SigningCredentials);
            var jwtHandler = new JwtSecurityTokenHandler();
            var accessTokenString = jwtHandler.WriteToken(securityToken);

            // create refresh token
            var tokenHash = HasherHelper.GetHash(SHA256.Create(), Configuration["AppSettings:RefreshSecret"] + Guid.NewGuid().ToString());
            var refreshToken = new JwtRefreshToken(tokenHash, DateTime.UtcNow.AddSeconds(double.Parse(Configuration["Jwt:RefreshTokenExpiration"])));
            var accessToken = new JwtAccessToken(refreshToken, accessTokenString, accessTokenExpiration);

            // add tokens to db
            var at = Mapper.Map<AccessToken>(accessToken);
            var rt = Mapper.Map<RefreshToken>(refreshToken);
            rt.User = user;
            await TokenRepository.Add(at, rt);
            await UnitOfWork.Complete();

            return accessToken;
        }

        public bool Exists(string accessToken, string refreshToken)
        {
            var tokensTuple = TokenRepository.GetTokens(accessToken, refreshToken);

            if (tokensTuple.Item1 == null || tokensTuple.Item2 == null) return false;

            return true;
        }

        public async Task<RefreshTokenResponse> Refresh(string accessToken, string refreshToken)
        {
            if (!Exists(accessToken, refreshToken))
            {
                return new RefreshTokenResponse(false, null);
            }

            var tokenPair = TokenRepository.GetTokens(accessToken, refreshToken);

            TokenRepository.Remove(accessToken, refreshToken);
            var newToken = await CreateAccessToken(tokenPair.Item2.User);
            await TokenRepository.Add(Mapper.Map<AccessToken>(newToken), Mapper.Map<RefreshToken>(newToken.RefreshToken));
            await UnitOfWork.Complete();

            return new RefreshTokenResponse(true, newToken);
        }
    }
}
