using CensoAPI02.Models;    
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

        // Tables
        public DbSet<HRU> HRU { get; set; }
        public DbSet<Locations> Locations { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Theme> Theme { get; set; }
        public DbSet<AnonRequest> AnonRequests { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<RequestStatus> RequestStatus { get; set; }
        public DbSet<AnswerStatus> Answer { get; set; }

        // UnionTables
        public DbSet<LocationsTheme> LocationsThemes { get; set; }
        public DbSet<HRUsersTheme> HRUsersThemes { get; set; }
        public DbSet<QuestionsTheme> QuestionsThemes { get; set; }
        public DbSet<AreasLocations> AreasLocations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
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
            modelBuilder.Entity<HRU>()
                .HasMany(u => u.Themes)
                .WithMany(t => t.HRU)
                .UsingEntity<HRUsersTheme>(
                    hrth => hrth.HasOne(prop => prop.Theme)
                    .WithMany()
                    .HasForeignKey(prop => prop.ThemeId),
                    hrth => hrth.HasOne(prop => prop.HRU)
                    .WithMany()
                    .HasForeignKey(prop => prop.UserId),
                    hrth =>
                    {
                        hrth.HasKey(prop => new { prop.ThemeId, prop.UserId });
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


            // Configuracion para que la relacion M2M utilice la entidad AreasLocations
            modelBuilder.Entity<Area>()
                .HasMany(a => a.locations)
                .WithMany(l => l.areas)
                .UsingEntity<AreasLocations>(
                    al => al.HasOne(prop => prop.Locations)
                    .WithMany()
                    .HasForeignKey(prop => prop .LocationId),
                    al => al.HasOne(prop => prop.Area)
                    .WithMany()
                    .HasForeignKey(prop => prop.AreaId),
                    al =>
                    {
                        al.HasKey(prop => new { prop.LocationId, prop.AreaId });
                    }
                );


            // configuración para relacion one-to-may HRU Locations
            modelBuilder.Entity<HRU>() // Parte 1 de la relación
                .HasOne<Locations>(hru => hru.locations) // Utilizamos las propiedades de naegación
                .WithMany(l => l.HRU) // Utilizamos las propiedades de naegación
                .HasForeignKey(hru => hru.LocationId); // Definimos el la FOREIGN KEY


            // configuración para relación one-tomany Questions and Request
            modelBuilder.Entity<Request>() // Parte 1 de la relación
                .HasOne<Question>(req => req.question)
                .WithMany(ques => ques.request)
                .HasForeignKey(req => req.QuestionId);


            // configuración para relación one-to-many Questions and AnonRequest
            modelBuilder.Entity<AnonRequest>() // Parte 1 de la relación
                 .HasOne<Question>(ques => ques.question)
                 .WithMany(anr => anr.anonRequest)
                 .HasForeignKey(ques => ques.QuestionId);


            // Configuracion de relación one-to-one AnonRequest and AnswerStatus
            modelBuilder.Entity<AnonRequest>()
                .HasOne<AnswerStatus>(ar => ar.answerStatus)
                .WithOne(ans => ans.anonRequest)
                .HasForeignKey<AnswerStatus>(ansF => ansF.AnonRequestId)
                .OnDelete(DeleteBehavior.NoAction);


            // configuración para relación one-to-one Request and AnswerSatatus
            modelBuilder.Entity<Request>()
                .HasOne<AnswerStatus>(r => r.answerStatus) // Utilizamos las propiedades de naegación
                .WithOne(ans => ans.request) // Utilizamos las propiedades de naegación
                .HasForeignKey<AnswerStatus>(ansF => ansF.RequestId); // Definimos el la FOREIGN KEY


            // configuracions para relacón one-to-many Area and Request
            modelBuilder.Entity<Request>() // Parte 1 de la relación
                .HasOne<Area>(req => req.area)
                .WithMany(area => area.request)
                .HasForeignKey(req => req.AreaId);


            // configuracions para relacón one-to-many Area and AnonRequest
            modelBuilder.Entity<AnonRequest>() // Parte 1 de la relación
                .HasOne<Area>(areq => areq.area)
                .WithMany(area => area.anonRequest)
                .HasForeignKey(areq => areq.AreaId);


            // configuracions para relacón one-to-many Theme and Request
            modelBuilder.Entity<Request>()
                .HasOne<Theme>(r => r.theme)
                .WithMany(th => th.Requests)
                .HasForeignKey(r => r.ThemeId);


            // configuracions para relacón one-to-many Theme and AnonRequest
            modelBuilder.Entity<AnonRequest>()
                .HasOne<Theme>(ar => ar.theme)
                .WithMany(th => th.AnonRequests)
                .HasForeignKey(ar => ar.ThemeId);


            // configuracions para relacón one-to-many HRU and Roles
            modelBuilder.Entity<HRU>()
                .HasOne<Roles>(hru => hru.roles)
                .WithMany(r => r.hru)
                .HasForeignKey(hru => hru.RoleId);


            // configuracions para relacón one-to-many Locations and Request
            modelBuilder.Entity<Request>()
                .HasOne<Locations>(r => r.locations)
                .WithMany(l => l.Request)
                .HasForeignKey(r => r.LocationId)
                .OnDelete(DeleteBehavior.NoAction); // Deshabilitamos la eliminacion en cascada


            // configuracions para relacón one-to-many Locations and AnonRequest
            modelBuilder.Entity<AnonRequest>()
                .HasOne<Locations>(ar => ar.locations)
                .WithMany(l => l.anonRequest)
                .HasForeignKey(ar => ar.LocationId)
                .OnDelete(DeleteBehavior.NoAction); // Deshabilitamos la eliminacion en cascada


            // configuracions para relacón one-to-many RequestStatus and Request
            modelBuilder.Entity<Request>()
                .HasOne<RequestStatus>(r => r.requestStatus)
                .WithMany(rs => rs.requests)
                .HasForeignKey(r => r.StatusId)
                .OnDelete(DeleteBehavior.NoAction);


            // configuracions para relacón one-to-many RequestStatusand AnonRequest
            modelBuilder.Entity<AnonRequest>()
                .HasOne<RequestStatus>(ar => ar.requestStatus)
                .WithMany(rs => rs.anonRequests)
                .HasForeignKey(ar => ar.StatusId)
                .OnDelete(DeleteBehavior.NoAction);


            // configuracions para relacón one-to-many HRU AnswerStatus
            modelBuilder.Entity<AnswerStatus>()
                .HasOne<HRU>(answer => answer.hru)
                .WithMany(hru => hru.answerStatus)
                .HasForeignKey(answer => answer.UserId)
                .OnDelete(DeleteBehavior.NoAction);


            // configuracions para relacón one-to-many HRU Request
            modelBuilder.Entity<Request>()
                .HasOne<HRU>(r => r.hru)
                .WithMany(hru => hru.requests)
                .HasForeignKey(r => r.rModificationUser)
                .OnDelete(DeleteBehavior.NoAction);


            // configuraciones para relacion one-to-may HRU AnonRequest
            modelBuilder.Entity<AnonRequest>()
                .HasOne<HRU>(ar => ar.hru)
                .WithMany(hru => hru.anonRequests)
                .HasForeignKey(ar => ar.arModificationUser)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
