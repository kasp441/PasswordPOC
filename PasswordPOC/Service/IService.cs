using PasswordPOC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordPOC.Service
{
    public interface IService
    {
        void addCredentials(Credentials credentials, byte[] keyfile);

        List<Credentials> getCredentials(byte[] keyfile);
    }
}
