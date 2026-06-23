using System.Reflection;
using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Domain.Repositories.Users;
using MyRecipeBook.Infrastructure.DataSource;
using MyRecipeBook.Infrastructure.Repositories;

namespace MyRecipeBook.Infrastructure;

public static class DependencyInjectionExtensions 
{
    public static void AddInfrastructure(this IServiceCollection service, IConfiguration configuration)
    {
        AddRepositories(service);

        if (configuration.IsUnitTestEnviroment())
            return;

        AddDbContext(service, configuration);
        FluentMigratorSqlServer(service, configuration);
    }

    private static void AddDbContext(IServiceCollection service, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        service.AddDbContext<MyRecipeBookDbContext>(
           dbContextOptions => dbContextOptions.UseSqlServer(connectionString)
        );

    }

    private static void AddRepositories(IServiceCollection service)
    {
        service.AddScoped<IUnitOfWork, UnitOfWork>();
        service.AddScoped<IUserReadOnlyRepository, UserRepository>();
        service.AddScoped<IUserWriteOnlyRepository, UserRepository>();

    }

    private static void FluentMigratorSqlServer(IServiceCollection service, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        service.AddFluentMigratorCore().ConfigureRunner(options =>
        {
            options.AddSqlServer()
            .WithGlobalConnectionString(connectionString)
            .ScanIn(Assembly.Load("MyRecipeBook.Infrastructure")).For.All();
        });
    }

}
