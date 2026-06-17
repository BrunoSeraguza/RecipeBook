using Moq;
using MyRecipeBook.Domain.Repositories.Users;

namespace CommomTestsUtilities.Repositories;

public class UserReadOnlyRepositoryBuilder
{
    private readonly Mock<IUserReadOnlyRepository> _repository;

    public UserReadOnlyRepositoryBuilder() => _repository = new Mock<IUserReadOnlyRepository>();
    public IUserReadOnlyRepository Build() => _repository.Object; 

    public void ExistActiveUserEmail(string email)
    {
        _repository.Setup(repository => repository.ExistActiveUserEmail(email)).ReturnsAsync(true);
    }


}
