using EmployeeDirectoryAPI.Services;
using System;

namespace EmployeeDirectoryAPI.Controllers
{
    public class BlacklistMiddleware
    {
        private readonly RequestDelegate _next;

        public BlacklistMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, EmpContext dbContext)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null && dbContext.Tokens.Any(t => t.token == token))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Token is blacklisted");
            }
            else
            {
                await _next(context);
            }
        }
    }


}
