using System.Security.Cryptography;
using System.Text;

namespace MyRecipeBook.Application.Services.EncryptPassword;

public class EncryptPassword
{
    public string Encrypt(string password)
    {
        string code = "shz";
        var newPassword = $"{password}{code}";

        var bytes = Encoding.UTF8.GetBytes(newPassword);

        var hashBytes = SHA512.HashData(bytes);

        return StringBytes(hashBytes);
    }

    private string StringBytes(byte[] bytes)
    {
        var sb = new StringBuilder();

        foreach (byte b in bytes)
        {
            var rex = b.ToString("x2");
            sb.Append(rex);
        }

        return sb.ToString();
    }
}
