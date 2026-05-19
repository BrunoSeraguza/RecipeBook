using Microsoft.EntityFrameworkCore;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Repositories.Users;
using MyRecipeBook.Infrastructure.DataSource;

namespace MyRecipeBook.Infrastructure.Repositories;

internal class UserRepository : IUserReadOnlyRepository, IUserWriteOnlyRepository
{
    private readonly MyRecipeBookDbContext _dbContext;

    public UserRepository(MyRecipeBookDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(User user)
    {
        await _dbContext.Users.AddAsync(user);
    }

    public async Task<bool> ExistActiveUserEmail(string email)
    {
        return await _dbContext.Users.AnyAsync(user => user.Email.Equals(email) && user.Active);
    }
}
