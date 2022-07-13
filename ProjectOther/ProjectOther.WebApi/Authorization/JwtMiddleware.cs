using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using ProjectOther.WebApi.Authentication;

namespace ProjectOther.WebApi.Authorization
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            _next = next;
            _appSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext context, IJwtUtils jwtUtils)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var role = jwtUtils.ValidateJwtToken(token);
            if (!String.IsNullOrEmpty(role))
            {
                // attach user to context on successful jwt validation
                context.Items["role"] = role;
            }

            await _next(context);
        }
    }
}
