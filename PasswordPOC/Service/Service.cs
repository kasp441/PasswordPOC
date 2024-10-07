using PasswordPOC.Entities;
using PasswordPOC.Helpers;
using PasswordPOC.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordPOC.Service
{
    public class Service : IService
    {
        IRepo _repo;
        public Service() 
        {
            _repo = new Repo.Repo();
        }
        public void addCredentials(Credentials credentials, byte[] keyfile)
        {
            //encrypt password 
            var encryptedPassword = PasswordHandler.Encrypt(credentials.Password, keyfile);
            _repo.addCredentials(new EncryptedCredentials
            {
                Password = encryptedPassword,
                Username = credentials.Username,
                domain = credentials.domain
            });
        }

        public List<Credentials> getCredentials(byte[] keyfile)
        {
            List<EncryptedCredentials> encryptedList = _repo.getCredentials();
            List<Credentials> DecryptedList = new List<Credentials>();

            foreach (var item in encryptedList)
            {
                //if passwordhandler returns error, skip this item
                try
                {
                    var decryptedPassword = PasswordHandler.Decrypt(item.Password, keyfile);
                    DecryptedList.Add(new Credentials
                    {
                        Username = item.Username,
                        domain = item.domain,
                        Password = decryptedPassword
                    });
                }
                catch (Exception e)
                {
                    continue;
                }
            }
            return DecryptedList;
        }
    }
}
