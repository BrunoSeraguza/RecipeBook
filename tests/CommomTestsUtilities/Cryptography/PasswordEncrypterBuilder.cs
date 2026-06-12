using MyRecipeBook.Application.Services.EncryptPassword;

namespace CommomTestsUtilities.Cryptography;

 public class PasswordEncrypterBuilder
{
    public static EncryptPassword Build() => new EncryptPassword("abc123");
}
