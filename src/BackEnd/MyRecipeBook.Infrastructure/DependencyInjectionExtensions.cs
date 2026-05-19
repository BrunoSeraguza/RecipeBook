using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Domain.Repositories.Users;
using MyRecipeBook.Infrastructure.DataSource;
using MyRecipeBook.Infrastructure.Repositories;

namespace MyRecipeBook.Infrastructure;

public static class DependencyInjectionExtensions 
{
    public static void AddInfrastructure(this IServiceCollection service, string connectionString)
    {
        AddRepositories(service);
        AddDbContext(service, connectionString);
    }

    private static void AddDbContext(IServiceCollection service, string connectionString)
    {
        //var connectionString = "Server=;Database=meuLivroDeReceitas;Trusted_Connection=True;Encrypt=False;MultipleActiveResultSets=true";

        service.AddDbContext<MyRecipeBookDbContext>(
           dbContextOptions => dbContextOptions.UseSqlServer(connectionString)
        );

    }

    private static void AddRepositories(IServiceCollection service)
    {
        service.AddScoped<IUserReadOnlyRepository, UserRepository>();
        service.AddScoped<IUserWriteOnlyRepository, UserRepository>();

    }
}
