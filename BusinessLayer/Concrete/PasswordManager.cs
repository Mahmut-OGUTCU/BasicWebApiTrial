using BusinessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class PasswordManager : IPasswordService
    {
        private readonly string CryptographyKey = "A72g1F848B4F4133aaDe5eF2813E1916";

        public string PasswordToDecrypt(string password)
        {
            var fullCipher = Convert.FromBase64String(password);

            var iv = new byte[16];
            var cipher = new byte[fullCipher.Length - iv.Length];

            Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, fullCipher.Length - iv.Length);
            var key = Encoding.UTF8.GetBytes(CryptographyKey);

            using var aes = Aes.Create();
            using var decryptor = aes.CreateDecryptor(key, iv);
            string result;
            using var memoryStream = new MemoryStream(cipher);
            using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            using var streamReader = new StreamReader(cryptoStream);
            result = streamReader.ReadToEnd();

            return result;
        }

        public string PasswordToEncrypt(string password)
        {
            if (string.IsNullOrEmpty(password))
                return password;

            using var aes = Aes.Create();
            using var encryptor = aes.CreateEncryptor(Encoding.UTF8.GetBytes(CryptographyKey), aes.IV);
            using var memoryStream = new MemoryStream();
            using (CryptoStream cryptoStream = new(memoryStream, encryptor, CryptoStreamMode.Write))
            using (var streamWriter = new StreamWriter(cryptoStream))
                streamWriter.Write(password);

            var decryptedContent = memoryStream.ToArray();
            var result = new byte[aes.IV.Length + decryptedContent.Length];
            Buffer.BlockCopy(aes.IV, 0, result, 0, aes.IV.Length);
            Buffer.BlockCopy(decryptedContent, 0, result, aes.IV.Length, decryptedContent.Length);

            return Convert.ToBase64String(result);
        }

        public string PasswordToSHA256Hash(string password)
        {
            string source = password + CryptographyKey;

            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] sourceBytes = Encoding.UTF8.GetBytes(source);
                byte[] hashBytes = sha256Hash.ComputeHash(sourceBytes);
                string hash = BitConverter.ToString(hashBytes).Replace("-", String.Empty);
                return hash;
            }
        }
    }
}
