using PasswordPOC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordPOC.Repo
{
    public interface IRepo
    {
        List<EncryptedCredentials> getCredentials();

        void addCredentials(EncryptedCredentials credentials);

    }
}
