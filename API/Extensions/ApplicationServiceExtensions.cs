using API.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
            IConfiguration config)
            {


services.AddScoped<iTokenService, TokenService>();
return services;

    }
}
