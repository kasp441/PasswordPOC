using Microsoft.EntityFrameworkCore;
using PasswordPOC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordPOC.Repo
{
    public class RepoContext : DbContext
    {
        public DbSet<EncryptedCredentials> Credentials { get; set; }

        public RepoContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=../../../PasswordPOC.db");
        }

    }
}
