using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Infrastructure.DataSource;

namespace MyRecipeBook.Infrastructure.Repositories;

internal class UnitOfWork : IUnitOfWork
{
    
    private readonly MyRecipeBookDbContext _dbContext;

    public UnitOfWork(MyRecipeBookDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Commit() =>  await _dbContext.SaveChangesAsync();

}
