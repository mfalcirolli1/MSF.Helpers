using System.IO;
using System.Security.Cryptography;

namespace MSF.Util.Encrypt
{
    public static class AES
    {
        public static string Decrypt(byte[] cipheredText, byte[] key, byte[] iniVector)
        {
            string text = string.Empty;
            using (Aes aes = Aes.Create())
            {
                ICryptoTransform decryptor = aes.CreateDecryptor(key, iniVector);

                using (MemoryStream memoryStream = new MemoryStream(cipheredText))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader(cryptoStream))
                        {
                            text = streamReader.ReadToEnd();
                        }
                    }
                }
            }

            return text;
        }

        public static byte[] Encrypt(string text, byte[] key, byte[] iniVector)
        {
            byte[] cipheredText;
            using (Aes aes = Aes.Create())
            {
                ICryptoTransform encryptor = aes.CreateEncryptor(key, iniVector);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                        {
                            streamWriter.Write(text);
                        }

                        cipheredText = memoryStream.ToArray();
                    }
                }
            }

            return cipheredText;
        }
    }
}
