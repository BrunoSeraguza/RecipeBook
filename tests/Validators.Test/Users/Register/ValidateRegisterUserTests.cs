using CommomTestsUtilities.Requests;
using MyRecipeBook.Application.UseCases.Users.Register;
using MyRecipeBook.Exceptions;
using Shouldly;

namespace Validators.Test.Users.Register;

public class ValidateRegisterUserTests
{

    // Fact -> testes simples
    // Theory -> quando quisermos loop
    [Fact]
    public  void Success()
    {
        var validade = new ValidateRegisterUser();
        var request = new RequestRegisteredUserJsonBuild();

        var result = validade.Validate(request.Build());


        result.IsValid.ShouldBeTrue();

        //Assert.True(result.IsValid);
    }

    [Fact]
    public void Error_Empty_Name()
    {
        var validade = new ValidateRegisterUser();
        var request = new RequestRegisteredUserJsonBuild().Build();
        request.Nome = string.Empty;

        var result = validade.Validate(request);

        Assert.False(result.IsValid);
        //result.IsValid.ShouldBeFalse();
        //result.Errors.ShouldHaveSingleItem();
       // result.Errors.ShouldContain(e => e.ErrorMessage == ResourceExceptionsMessage.NOME_VAZIO);
        Assert.Single(result.Errors);
        Assert.Contains(result.Errors, e => e.ErrorMessage == ResourceExceptionsMessage.NOME_VAZIO);
              
    }


    [Fact]
    public void Error_Empty_Email()
    {
        var validade = new ValidateRegisterUser();
        var request = new RequestRegisteredUserJsonBuild().Build();
        request.Email = string.Empty;

        var result = validade.Validate(request);


        Assert.False(result.IsValid);
        //result.IsValid.ShouldBeFalse();
        //result.Errors.ShouldHaveSingleItem();
        // result.Errors.ShouldContain(e => e.ErrorMessage == ResourceExceptionsMessage.NOME_VAZIO);
        Assert.Single(result.Errors);
        Assert.Contains(result.Errors, e => e.ErrorMessage == ResourceExceptionsMessage.EMAIL_VAZIO);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void Error_Password_Invalid(int passwordLenght)
    {
        var validade = new ValidateRegisterUser();
        var request = new RequestRegisteredUserJsonBuild().Build(passwordLenght);

        var result = validade.Validate(request);

        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Contains(result.Errors, e => e.ErrorMessage == ResourceExceptionsMessage.PASSWORD_INVALIDO);      
    }

}
