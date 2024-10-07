using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PasswordPOC.Helpers
{
    public class PasswordHandler
    {
        public static byte[] Encrypt(string password, byte[] key)
        {
            using (System.Security.Cryptography.Aes MyAes = System.Security.Cryptography.Aes.Create())
            {
                MyAes.KeySize = 256;
                MyAes.Key = DeriveKey(key);
                MyAes.GenerateIV();

                var encryptor = MyAes.CreateEncryptor(MyAes.Key, MyAes.IV);

                using (var ms = new MemoryStream())
                {
                    ms.Write(MyAes.IV, 0, MyAes.IV.Length);
                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    using (var sw = new StreamWriter(cs))
                    {
                        sw.Write(password);
                    }
                    return ms.ToArray();
                }
            }
        }

        public static string Decrypt(byte[] password, byte[] key)
        {
            try
            {
                using (System.Security.Cryptography.Aes MyAes = System.Security.Cryptography.Aes.Create())
                {
                    MyAes.Key = DeriveKey(key);
                    using (var ms = new MemoryStream(password))
                    {
                        byte[] iv = new byte[16];
                        ms.Read(iv, 0, 16);
                        MyAes.IV = iv;
                        var decryptor = MyAes.CreateDecryptor(MyAes.Key, MyAes.IV);
                        var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
                        using (var sr = new StreamReader(cs))
                        {
                            return sr.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error decrypting password", e);
            }
        }

        private static byte[] DeriveKey(byte[] key) // Fix: Made this method static
        {
            using (var deriveBytes = new Rfc2898DeriveBytes(key, key, 1000))
            {
                return deriveBytes.GetBytes(32);
            }
        }
    }
}
