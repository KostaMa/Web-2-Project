using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ProjectOther.WebApi.Authorization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly String[] _roles;
        private readonly IServiceProvider serviceProvider;
        public AuthorizeAttribute(params String[] roles)
        {
            _roles = roles ?? new String[] { };
        }

        public async void OnAuthorization(AuthorizationFilterContext context)
        {
            // skip authorization if action is decorated with [AllowAnonymous] attribute
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (allowAnonymous)
                return;

            // authorization
            var role = (string)context.HttpContext.Items["role"];
            String[] roleArray;
            if (_roles.Any())
            {
                roleArray = _roles[0].Split(',').Select(p => p.Trim()).ToArray();
            }
            else
            {
                roleArray = new String[0];
            }

            if (String.IsNullOrEmpty(role) || (roleArray.Any() && !roleArray.Contains(role)))
            {
                // not logged in or role not authorized
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}
