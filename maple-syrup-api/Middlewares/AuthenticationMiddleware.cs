using maple_syrup_api.Dto;
using maple_syrup_api.Helpers;
using maple_syrup_api.Repositories.IRepository;
using maple_syrup_api.Services.IService;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace maple_syrup_api
{
    public class AuthenticationMiddleWare
    {
        private readonly RequestDelegate _next;

        public AuthenticationMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext pContext,IUserRepository pUserRepository, IJwtUtils pJwtUtils)
        {
            var token = pContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            ValidateToken newToken = await pJwtUtils.ValidateJwtToken(token);
            if (newToken?.UserId != null)
            {
                // attach user to context on successful jwt validation
                pContext.Items["User"] = pUserRepository.Get(newToken.UserId.Value);
                pContext.Items["Token"] = newToken.AccessToken;
            }

            await _next(pContext);
        }
    }
}
