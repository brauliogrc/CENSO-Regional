using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CENSO.Models
{
    public class CDBContext : DbContext
    {

        public CDBContext(DbContextOptions<CDBContext> options) : base(options)
        {

        }

        public DbSet<HR_User> HR_Users { get; set; }
        public DbSet<Locations> Locations { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Theme> Theme { get; set; }

        protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Server=GLL1330W\SQL_ET; DataBase=censo02; User ID=sa; Password=AccesoConti08; ConnectRetryCount=0");
            //@"Server=GLL1330W\SQL_ET; DataBase=censo02; Trusted_Connection=true; ConnectRetryCount=0"
        }

        protected override void OnModelCreating( ModelBuilder modelBuilder )
        {
            //Relationship one-2-one entities Question and Request

            //Configuring primary key for joining table. Relationship many-2-many entities Locations and Theme
            modelBuilder.Entity<Location_Theme>().HasKey( sc => new { sc.locationId, sc.themeId } );

            //Configuring primary key for joining table. Relationship many-2-many entities HR_User and Theme
            modelBuilder.Entity<HRU_Theme>().HasKey(sc => new { sc.hruserId, sc.themeId });

            //Configuring primary key for joining table. Relationship many-2-many entities Question and Theme
            modelBuilder.Entity<Question_Theme>().HasKey(sc => new { sc.questionId, sc.themeId });

        }
    }

    //Joining entity class for relationship many-2-many between entities Locations and Theme
    public class Location_Theme
    {
        //Foreing key properties and reference navigation properties of entity Locations
        public int locationId { get; set; }
        public Locations locations { get; set; }

        //Foreing key properties and reference navigation properties of entity Theme
        public int themeId { get; set; }
        public Theme theme { get; set; }
    }

    //Joining entity class for relationship many-2-many between entities Locations and Theme
    public class HRU_Theme
    {
        //Foreing key properties and reference navigation properties of entity HR_User
        public int hruserId { get; set; }
        public HR_User hrUser { get; set; }

        //Foreing key properties and reference navigation properties of entity Theme
        public int themeId { get; set; }
        public Theme theme { get; set; }
    }

    //Joining entity class for relationship many-2-many between entities Question and Theme
    public class Question_Theme
    {
        //Foreing key properties and reference navigation properties of entity Question
        public int questionId { get; set; }
        public Question question { get; set; }

        //Foreing key properties and reference navigation properties of entity Theme
        public int themeId { get; set; }
        public Theme theme { get; set; }
    }
}
