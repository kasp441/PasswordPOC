using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordPOC.Helpers
{
    using System;
    using System.IO;
    using System.Security.Cryptography;

    public class KeyFileGenerator
    {
        private const int KeySize = 256;

        public byte[] Key { get; private set; }

        public KeyFileGenerator()
        {
            Key = GenerateKey();
        }

        private byte[] GenerateKey()
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                var key = new byte[KeySize];
                rng.GetBytes(key);
                return key;
            }
        }

        public string SaveToFile(string fileName = "keyfile.bin")
        {
            var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var filePath = Path.Combine(desktopPath, fileName);
            File.WriteAllBytes(filePath, Key);
            return filePath;
        }

        public byte[] ReadKeyFromFile(string filePath)
        {
            try { return File.ReadAllBytes(filePath); }
            catch (Exception e) { throw new Exception("Error reading key file", e); }
        }


    }

}
