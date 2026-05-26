using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Application.Services.EncryptPassword;
using MyRecipeBook.Application.UseCases.Users.Register;

namespace MyRecipeBook.Application;

public static class DependencyInjectionExtensions
{
    public static void AddApplication(this IServiceCollection service, IConfiguration configuration)
    {
        AddUserCases(service,configuration);
    }

    private static void AddUserCases(IServiceCollection service, IConfiguration configuration)
    {
        //binder
        var additionalKey = configuration.GetValue<string>("Settings:Passwords:AdditionalKey");

        service.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        service.AddScoped(options => new EncryptPassword(additionalKey!));
       
    }
}
