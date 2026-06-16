using CommomTestsUtilities.Cryptography;
using CommomTestsUtilities.Repositories;
using CommomTestsUtilities.Requests;
using MyRecipeBook.Application.UseCases.Users.Register;

namespace UseCases.Test.Users.Register;

public class RegisterUseUserCaseTest
{
    [Fact]
    public async Task Success()
    {
        //result.nome devve ser igual a request.name
        var request =  RequestRegisteredUserJsonBuild.Build();
        var useCase = CreateUseCase();
        var result  = await useCase.Execute(request);

        Assert.NotNull(result);

        //Assert.Contains(result.Name, u => u.Equals(request.Nome));
        Assert.Equal(result.Name, request.Nome);
    }

    private RegisterUserUseCase CreateUseCase()
    {
        var encryptPassword         = PasswordEncrypterBuilder.Build();
        var unitOfWork              = UnitOfWorkBuilder.Build();
        var userWriteOnlyRepository = UserWriteOnlyRepositoryBuilder.Build();
        var userReadOnlyRepository  = new UserReadOnlyRepositoryBuilder().Build();

        return new RegisterUserUseCase(userReadOnlyRepository, userWriteOnlyRepository, encryptPassword, unitOfWork);
    }
}
