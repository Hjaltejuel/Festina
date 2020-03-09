using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sommerhuse.Models;
using MySql.Data.EntityFrameworkCore.Extensions;

namespace Sommerhuse.Data
{
    public class SommerhusContext : IdentityDbContext<ApplicationUser>
    {
        public SommerhusContext (DbContextOptions<SommerhusContext> options)
            : base(options)
        {
        }

        public DbSet<Sommerhuse.Models.Dato> Dato { get; set; }

        public DbSet<Sommerhuse.Models.Image> Images { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.UseMySQL("Server=mysql77.unoeuro.com;Port=3306;Database=juelskovrup_dk_db;Uid=juelskovrup_dk;Pwd=netromlfs;");
        }
    }
}
