using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

using Quacore.Domain.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quacore.Middlewares
{
    public class CheckAccessTokenMiddleware
    {
        private readonly RequestDelegate _next;

        public CheckAccessTokenMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ITokenService tokenService)
        {
            if(context.Request.Path.StartsWithSegments("/static", StringComparison.OrdinalIgnoreCase))
            {
                await _next(context);
                return;
            }

            if (context.GetEndpoint().Metadata.GetMetadata<AuthorizeAttribute>() is null)
            {
                await _next(context);
                return;
            }

            string authorization = context.Request.Headers[HeaderNames.Authorization];

            if(string.IsNullOrEmpty(authorization))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            var token = authorization.Split(' ')[1];
            var userId = int.Parse(context.User.FindFirst("User").Value);
            var response = await tokenService.AccessTokenExists(token, userId);


            if (!response.IsSuccess)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }    

            await _next(context);
        }
    }
}
