﻿using API.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
    IConfiguration config)
    {
            
services.AddControllers();

services.AddDbContext<DataContext>(opt =>
{
    opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
});

services.AddCors();

services.AddScoped<iTokenService,TokenService>();
return services;

    }
}
