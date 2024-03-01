using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Semprg_BlazorIntroductory.Entity.Repositories;
using Semprg_BlazorIntroductory.Infrastructure.Repositories;
using Semprg_BlazorIntroductory.Infrastructure.Services;

namespace Semprg_BlazorIntroductory.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<IGameTreeRepository, GameTreeRepository>();
        services.AddScoped<GameTreeService>();

        services.AddDbContext<AppDbContext>(c =>
        {
            c.UseSqlServer(config["SQLServerConnectionString"]);
        });

        return services;
    }

    public static async Task<IHost> InitializeInfrastructure(this IHost app, string rootNodeStoryContent)
    {
        using var scope = app.Services.CreateScope();

        var gameTreeService = scope.ServiceProvider.GetRequiredService<GameTreeService>();
        await gameTreeService.EnsureRootExistsAsync(rootNodeStoryContent);

        return app;
    }
}