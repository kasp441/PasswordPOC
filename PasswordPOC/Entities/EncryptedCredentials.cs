using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordPOC.Entities
{
    public class EncryptedCredentials
    {
        public int Id { get; set; }

        public string domain { get; set; }
        public string Username { get; set; }
        public byte[] Password { get; set; }
    }
}
