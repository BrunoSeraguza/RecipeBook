using Moq;
using MyRecipeBook.Domain.Repositories.Users;

namespace CommomTestsUtilities.Repositories;

public class UserWriteOnlyRepositoryBuilder
{
    public static IUserWriteOnlyRepository Build()
    {
        var moq = new Mock<IUserWriteOnlyRepository>();

        return moq.Object;
    }
}
