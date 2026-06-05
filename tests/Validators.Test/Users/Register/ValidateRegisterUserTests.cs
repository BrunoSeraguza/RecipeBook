using CommomTestsUtilities.Requests;
using MyRecipeBook.Application.UseCases.Users.Register;

namespace Validators.Test.Users.Register;

public class ValidateRegisterUserTests
{
    [Fact]
    public  void Success()
    {
        var validade = new ValidateRegisterUser();
        var request = new RequestRegisteredUserJsonBuild();

        var result = validade.Validate(request.Build());

        Assert.True(result.IsValid);
    }
}
