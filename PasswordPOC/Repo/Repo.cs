using PasswordPOC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordPOC.Repo
{
    public class Repo : IRepo
    {
        RepoContext _context;
        public Repo()
        {
            _context = new RepoContext();
            _context.Database.EnsureCreated();
        }

        public void addCredentials(EncryptedCredentials credentials)
        {
            _context.Credentials.Add(credentials);
            _context.SaveChanges();
        }

        public List<EncryptedCredentials> getCredentials()
        {
            return _context.Credentials.ToList();
        }
    }
}
