using CommomTestsUtilities.Cryptography;
using CommomTestsUtilities.Repositories;
using CommomTestsUtilities.Requests;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using MyRecipeBook.Application.UseCases.Users.Register;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ExceptionsBase;

namespace UseCases.Test.Users.Register;

public class RegisterUseUserCaseTest
{
    [Fact]
    public async Task Success()
    {
        var request =  RequestRegisteredUserJsonBuild.Build();
        var useCase = CreateUseCase();
        var result  = await useCase.Execute(request);

        Assert.NotNull(result);

        //Assert.Contains(result.Name, u => u.Equals(request.Nome));
        Assert.Equal(result.Name, request.Nome);
    }

    [Fact]
    public async void Error_Email_Already_Registered()
    {
        var request = RequestRegisteredUserJsonBuild.Build();
        var useCase = CreateUseCase(request.Email);

        Func<Task> act = async () => await useCase.Execute(request);

        var exception = await Assert.ThrowsAsync<ErrorOnValidateException>(act);

        Assert.Single(exception.ErrorMessage);
        Assert.Contains(ResourceExceptionsMessage.EMAIL_EXISTE, exception.ErrorMessage);
    }

    [Fact]
    public async void Error_Empry_Name()
    {
        var request = RequestRegisteredUserJsonBuild.Build();
        var useCase = CreateUseCase(request.Email);
        request.Nome = string.Empty;        

        Func<Task> act = async () => await useCase.Execute(request);

        var exception = await Assert.ThrowsAsync<ErrorOnValidateException>(act);

        Assert.Single(exception.ErrorMessage);
        Assert.Contains(ResourceExceptionsMessage.NOME_VAZIO, exception.ErrorMessage);
    }

    private RegisterUserUseCase CreateUseCase(string? email = null)
    {
        var encryptPassword         = PasswordEncrypterBuilder.Build();
        var unitOfWork              = UnitOfWorkBuilder.Build();
        var writeOnlyRepository     = UserWriteOnlyRepositoryBuilder.Build();
        var readOnlyRepository      = new UserReadOnlyRepositoryBuilder();

        if (!string.IsNullOrEmpty(email))
            readOnlyRepository.ExistActiveUserEmail(email);

        return new RegisterUserUseCase(readOnlyRepository.Build(), writeOnlyRepository, encryptPassword, unitOfWork);
    }

 

}
