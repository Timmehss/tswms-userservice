using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;
using TSWMS.UserService.Shared.Options;

namespace TSWMS.UserService.Shared.Helpers;

public class AesEncryptionHelper
{
    private readonly byte[] _key;
    private readonly byte[] _iv;

    public AesEncryptionHelper(IOptions<EncryptionOptions> options)
    {
        var settings = options.Value;
        _key = Encoding.UTF8.GetBytes(settings.Key);
        _iv = Encoding.UTF8.GetBytes(settings.IV);
    }

    public string EncryptString(string plainText)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = _key;
            aes.IV = _iv;

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter sw = new StreamWriter(cs))
                    {
                        sw.Write(plainText);
                    }
                }
                return Convert.ToBase64String(ms.ToArray());
            }
        }
    }

    public string DecryptString(string cipherText)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = _key;
            aes.IV = _iv;

            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(cipherText)))
            {
                using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader sr = new StreamReader(cs))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
        }
    }
}