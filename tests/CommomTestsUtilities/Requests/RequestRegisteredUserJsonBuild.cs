using Bogus;
using MyRecipeBook.Communication.Request;

namespace CommomTestsUtilities.Requests;

public class RequestRegisteredUserJsonBuild
{

    public RequestRegisteredUserJson Build()
    {
        return new Faker<RequestRegisteredUserJson>()
            .RuleFor(user => user.Nome,     (f)    => f.Person.FirstName)
            .RuleFor(user => user.Email,    (f, u) => f.Internet.Email(u.Nome))
            .RuleFor(user => user.Password, (f)    => f.Internet.Password());
           
    }
}
