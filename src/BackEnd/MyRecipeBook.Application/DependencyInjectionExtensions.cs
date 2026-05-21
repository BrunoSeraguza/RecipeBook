using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Application.Services.EncryptPassword;
using MyRecipeBook.Application.UseCases.Users.Register;

namespace MyRecipeBook.Application;

public static class DependencyInjectionExtensions
{
    public static void AddApplication(this IServiceCollection service)
    {
        AddUserCases(service);
    }

    private static void AddUserCases(IServiceCollection service)
    {
        service.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        service.AddScoped(options => new EncryptPassword());
       
    }
}
