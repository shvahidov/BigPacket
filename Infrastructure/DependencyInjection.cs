using Application.Interfaces;
using Hangfire;
using Hangfire.PostgreSql;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Services;
using Infrastructure.Services.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration config)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(config.GetConnectionString("Default")));
        services.AddScoped<IPacketRepository, PacketRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IPacketTypeRepository, PacketTypeRepository>();
        services.AddHttpContextAccessor();
        services.AddHangfire(hangfire =>
            hangfire.UsePostgreSqlStorage(config.GetConnectionString("Default")));
        services.AddHangfireServer();
        services.AddScoped<PacketExpirationJob>();

        return services;
    }
}