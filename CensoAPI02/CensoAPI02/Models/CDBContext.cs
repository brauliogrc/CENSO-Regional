using CensoAPI02.Models.UnionTables;
using CensoAPI02.UnionTables;
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
        public DbSet<LocationsTheme> LocationsThemes { get; set; }
        public DbSet<HRUsersTheme> HRUsersThemes { get; set; }
        public DbSet<QuestionsTheme> QuestionsThemes { get; set; }

        protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Server=GLL1330W\SQL_ET; DataBase=censo02; User ID=sa; Password=AccesoConti08; ConnectRetryCount=0");
            //@"Server=GLL1330W\SQL_ET; DataBase=censo02; Trusted_Connection=true; ConnectRetryCount=0"
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // configuración para que la realacion M2M utilice la entidad LocationsTheme
            modelBuilder.Entity<Locations>()
                .HasMany(l => l.Themes) // Una Location tiene muchos temas
                .WithMany(t => t.Locations) // Un theme peude estar en muchas locations
                .UsingEntity<LocationsTheme>( // Decimos que utilice nuestra entidad
                    lt => lt.HasOne(prop => prop.Theme) // Un registro individual se relaciona con una location y un theme
                    .WithMany()
                    .HasForeignKey(prop => prop.ThemeId), // Relacionamos la ForeignKey con la PK de la enttidad Theme
                    lt => lt.HasOne(prop => prop.Locations)
                    .WithMany()
                    .HasForeignKey(prop => prop.LocationId),
                    lt =>
                    {
                        lt.HasKey(prop => new { prop.ThemeId, prop.LocationId });
                    }
                );

            // configuración para que la realacion M2M utilice la entidad HRUsersTheme
            modelBuilder.Entity<HR_User>()
                .HasMany(u => u.Themes)
                .WithMany(t => t.HR_Users)
                .UsingEntity<HRUsersTheme>(
                    hrth => hrth.HasOne(prop => prop.Theme)
                    .WithMany()
                    .HasForeignKey(prop => prop.ThemeId),
                    hrth => hrth.HasOne(prop => prop.HR_User)
                    .WithMany()
                    .HasForeignKey(prop => prop.HRUId),
                    hrth =>
                    {
                        hrth.HasKey(prop => new { prop.ThemeId, prop.HRUId });
                    }
                );

            // configuración para que la realacion M2M utilice la entidad QuestionsThemes
            modelBuilder.Entity<Question>()
                .HasMany(q => q.Themes)
                .WithMany(t => t.Questions)
                .UsingEntity<QuestionsTheme>(
                    qt => qt.HasOne(prop => prop.Theme)
                    .WithMany()
                    .HasForeignKey(prop => prop.ThemeId),
                    qt => qt.HasOne(prop => prop.Question)
                    .WithMany()
                    .HasForeignKey(prop => prop.QuestionId),
                    qt =>
                    {
                        qt.HasKey(prop => new { prop.ThemeId, prop.QuestionId });
                    }
                );
        }
    }

    /** REMOVE THE FOLLOWING CODE **/
    //Joining entity class for relationship many-2-many between entities Locations and Theme
    /*public class Location_Theme
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
    }*/
}
