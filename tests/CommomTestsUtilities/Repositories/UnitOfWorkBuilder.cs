using Moq;
using MyRecipeBook.Domain.Repositories;

namespace CommomTestsUtilities.Repositories;

public class UnitOfWorkBuilder
{
    public static IUnitOfWork Build()
    {
        var moq = new Mock<IUnitOfWork>();

        return moq.Object;
    }
}
