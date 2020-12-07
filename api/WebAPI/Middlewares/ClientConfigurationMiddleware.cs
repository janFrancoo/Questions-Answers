using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebAPI.Middlewares
{
    public class ClientConfigurationMiddleware
    {
        private readonly RequestDelegate _next;

        public ClientConfigurationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, IClientConfiguration clientConfiguration)
        {
            if (httpContext.Request.Headers.ContainsKey("Authorization"))
            {
                var identity = httpContext.User.Identity as ClaimsIdentity;
                var uId = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
                clientConfiguration.UserId = uId;
            }

            await _next.Invoke(httpContext);
        }
    }
}
