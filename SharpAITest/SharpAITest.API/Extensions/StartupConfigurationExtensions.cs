using Scalar.AspNetCore;
using SharpAITest.Application.Services;
using SharpAITest.Application.Services.Abstraction;
using SharpAITest.DataAccessLibrary.Repositories;
using SharpAITest.DataAccessLibrary.Repositories.Abstractions;
using SharpAITest.DataAccessLibrary.Services;
using SharpAITest.DataAccessLibrary.Services.Abstractions;

namespace SharpAITest.API.Extensions;

public static class StartupConfigurationExtensions
{
    public static IServiceCollection AddLocalDependencies(this IServiceCollection services)
    {
        services.AddScoped<IAppDbContext, SqlDbContext>();

        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IOrderProductService, OrderProductService>();

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderProductRepository, OrderProductRepository>();
        return services;
    }

    public static void AddDevTools(this WebApplication app)
    {
        app.MapOpenApi();
        app.MapScalarApiReference(options =>
        {
            options
                .WithTheme(ScalarTheme.BluePlanet)
                .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.Http);
        });
    }
}
