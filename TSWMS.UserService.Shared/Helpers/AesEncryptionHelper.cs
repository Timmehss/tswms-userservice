using Dapr.Client;
using System.Security.Cryptography;

namespace TSWMS.UserService.Shared.Helpers;

public class AesEncryptionHelper
{
    private readonly byte[] _key;
    private readonly byte[] _iv;

    public AesEncryptionHelper(DaprClient daprClient)
    {
        // Fetch AES key & IV from local Dapr secret store
        var keySecret = daprClient.GetSecretAsync("local-secret-store", "aesKey").GetAwaiter().GetResult();
        var ivSecret = daprClient.GetSecretAsync("local-secret-store", "aesIV").GetAwaiter().GetResult();

        // Convert Base64 secrets to byte[]
        _key = Convert.FromBase64String(keySecret["aesKey"]);
        _iv = Convert.FromBase64String(ivSecret["aesIV"]);
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