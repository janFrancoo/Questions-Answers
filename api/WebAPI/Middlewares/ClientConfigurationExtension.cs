using Microsoft.AspNetCore.Builder;

namespace WebAPI.Middlewares
{
    public static class ClientConfigurationExtension
    {
        public static IApplicationBuilder UseClientConfiguration(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ClientConfigurationMiddleware>();
        }
    }
}
