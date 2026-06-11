using Bogus;
using MyRecipeBook.Communication.Request;

namespace CommomTestsUtilities.Requests;

public class RequestRegisteredUserJsonBuild
{

    public RequestRegisteredUserJson Build(int passwordLenght = 10)
    {
        return new Faker<RequestRegisteredUserJson>()
            .RuleFor(user => user.Nome,     (f)    => f.Person.FirstName)
            .RuleFor(user => user.Email,    (f, user) => f.Internet.Email(user.Nome))
            .RuleFor(user => user.Password, (f)    => f.Internet.Password(passwordLenght));
           
    }
}
