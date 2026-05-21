using MyRecipeBook.Communication.Request;
using MyRecipeBook.Communication.Response;

namespace MyRecipeBook.Application.UseCases.Users.Register;

public interface IRegisterUserUseCase
{
    public  Task<ResponseRegisteredUserJson> Execute(RequestRegisteredUserJson request);
}
