using Microsoft.Extensions.Configuration;

namespace MyRecipeBook.Infrastructure
{
    public static class ConfigurationExtensions
    {
        public static bool IsUnitTestEnviroment(this IConfiguration configuration)
        {
            return configuration.GetValue<bool>("InMemoryTest");
        }
    }
}
