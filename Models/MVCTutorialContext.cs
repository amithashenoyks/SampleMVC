using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVC_Tutorial.Models
{
    public class MVCTutorialContext : DbContext
    {

        public MVCTutorialContext() : base("name=TutorialDB")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MVCTutorialContext, MVC_Tutorial.Migrations.Configuration>());
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}