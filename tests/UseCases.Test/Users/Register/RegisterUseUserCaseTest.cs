using CommomTestsUtilities.Cryptography;
using CommomTestsUtilities.Requests;
using MyRecipeBook.Application.UseCases.Users.Register;

namespace UseCases.Test.Users.Register;

public class RegisterUseUserCaseTest
{
   
    public async Task Success()
    {
        //result.nome devve ser igual a request.name
        var request = new RequestRegisteredUserJsonBuild().Build();
        var encryptPassword =  PasswordEncrypterBuilder.Build();
        //var useCase = new RegisterUserUseCase();

        //var result = await useCase.Execute(request);

        //Assert.NotNull(result);

        //Assert.Contains(result.Name, u => u.Equals(request.Nome));
        //Assert.Equal(result.Name, request.Nome);

    }
}
