using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MyCloud.Models
{
    public class CloudContext : IdentityDbContext<CloudUser>
    {
        private IConfigurationRoot _config;

        public CloudContext(IConfigurationRoot config, DbContextOptions options) : base(options)
        {
            _config = config;
        }

        public DbSet<CloudUser> CloudUsers { get; set; }
        public DbSet<Folder> Folders { get; set; }
        public DbSet<FileData> FileDatas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlite("Filename=./TinyHubDatabase.db");
        }
    }
}
