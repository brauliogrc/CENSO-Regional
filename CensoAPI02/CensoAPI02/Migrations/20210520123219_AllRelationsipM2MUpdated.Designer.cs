﻿// <auto-generated />
using System;
using CENSO.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CensoAPI02.Migrations
{
    [DbContext(typeof(CDBContext))]
    [Migration("20210520123219_AllRelationsipM2MUpdated")]
    partial class AllRelationsipM2MUpdated
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CENSO.Models.HRU_Theme", b =>
                {
                    b.Property<int>("hruserId")
                        .HasColumnType("int");

                    b.Property<int>("themeId")
                        .HasColumnType("int");

                    b.HasKey("hruserId", "themeId");

                    b.HasIndex("themeId");

                    b.ToTable("HRU_Theme");
                });

            modelBuilder.Entity("CENSO.Models.HR_User", b =>
                {
                    b.Property<int>("HR_UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("User_Creation_User")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("User_Creeation_Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("User_Email")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("User_Modification_Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("User_Modification_User")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("User_Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("User_Rol")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<bool>("User_Status")
                        .HasColumnType("bit");

                    b.HasKey("HR_UserId");

                    b.ToTable("HR_Users");
                });

            modelBuilder.Entity("CENSO.Models.Locations", b =>
                {
                    b.Property<int>("LocationsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Location_Creation_Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Location_Creation_User")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("Location_Modification_Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Location_Modification_User")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Location_Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("Location_Status")
                        .HasColumnType("bit");

                    b.HasKey("LocationsId");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("CENSO.Models.Question", b =>
                {
                    b.Property<int>("QuestionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Question_Creation_Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Question_Creation_User")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("Question_Modification_Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Question_Modification_User")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Question_Name")
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.Property<bool>("Question_Status")
                        .HasColumnType("bit");

                    b.HasKey("QuestionId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("CENSO.Models.Question_Theme", b =>
                {
                    b.Property<int>("questionId")
                        .HasColumnType("int");

                    b.Property<int>("themeId")
                        .HasColumnType("int");

                    b.HasKey("questionId", "themeId");

                    b.HasIndex("themeId");

                    b.ToTable("Question_Theme");
                });

            modelBuilder.Entity("CENSO.Models.Request", b =>
                {
                    b.Property<int>("RequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.Property<string>("Request_Answer_Status")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Request_Area")
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    b.Property<string>("Request_Attachement")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("Request_Creation_Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Request_Creation_User")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Request_Employee_Leader")
                        .HasColumnType("int");

                    b.Property<int>("Request_Employee_Type")
                        .HasColumnType("int");

                    b.Property<string>("Request_Issue")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime>("Request_Modification_Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Request_Modification_User")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Request_Theme")
                        .HasColumnType("int");

                    b.Property<string>("Request_User_Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("RequestId");

                    b.HasIndex("QuestionId")
                        .IsUnique();

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("CENSO.Models.Theme", b =>
                {
                    b.Property<int>("ThemeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Theme_Creation_Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Theme_Creation_User")
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    b.Property<int>("Theme_Modification_User")
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    b.Property<DateTime>("Theme_Modification_date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Theme_Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("Theme_Status")
                        .HasColumnType("bit");

                    b.HasKey("ThemeId");

                    b.ToTable("Theme");
                });

            modelBuilder.Entity("HR_UserTheme", b =>
                {
                    b.Property<int>("HR_UsersHR_UserId")
                        .HasColumnType("int");

                    b.Property<int>("ThemesThemeId")
                        .HasColumnType("int");

                    b.HasKey("HR_UsersHR_UserId", "ThemesThemeId");

                    b.HasIndex("ThemesThemeId");

                    b.ToTable("HR_UserTheme");
                });

            modelBuilder.Entity("LocationsTheme", b =>
                {
                    b.Property<int>("LocationsId")
                        .HasColumnType("int");

                    b.Property<int>("ThemesThemeId")
                        .HasColumnType("int");

                    b.HasKey("LocationsId", "ThemesThemeId");

                    b.HasIndex("ThemesThemeId");

                    b.ToTable("LocationsTheme");
                });

            modelBuilder.Entity("QuestionTheme", b =>
                {
                    b.Property<int>("QuestionsQuestionId")
                        .HasColumnType("int");

                    b.Property<int>("ThemesThemeId")
                        .HasColumnType("int");

                    b.HasKey("QuestionsQuestionId", "ThemesThemeId");

                    b.HasIndex("ThemesThemeId");

                    b.ToTable("QuestionTheme");
                });

            modelBuilder.Entity("CENSO.Models.HRU_Theme", b =>
                {
                    b.HasOne("CENSO.Models.HR_User", "hrUser")
                        .WithMany()
                        .HasForeignKey("hruserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CENSO.Models.Theme", "theme")
                        .WithMany()
                        .HasForeignKey("themeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("hrUser");

                    b.Navigation("theme");
                });

            modelBuilder.Entity("CENSO.Models.Question_Theme", b =>
                {
                    b.HasOne("CENSO.Models.Question", "question")
                        .WithMany()
                        .HasForeignKey("questionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CENSO.Models.Theme", "theme")
                        .WithMany()
                        .HasForeignKey("themeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("question");

                    b.Navigation("theme");
                });

            modelBuilder.Entity("CENSO.Models.Request", b =>
                {
                    b.HasOne("CENSO.Models.Question", "question")
                        .WithOne("request")
                        .HasForeignKey("CENSO.Models.Request", "QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("question");
                });

            modelBuilder.Entity("HR_UserTheme", b =>
                {
                    b.HasOne("CENSO.Models.HR_User", null)
                        .WithMany()
                        .HasForeignKey("HR_UsersHR_UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CENSO.Models.Theme", null)
                        .WithMany()
                        .HasForeignKey("ThemesThemeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LocationsTheme", b =>
                {
                    b.HasOne("CENSO.Models.Locations", null)
                        .WithMany()
                        .HasForeignKey("LocationsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CENSO.Models.Theme", null)
                        .WithMany()
                        .HasForeignKey("ThemesThemeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("QuestionTheme", b =>
                {
                    b.HasOne("CENSO.Models.Question", null)
                        .WithMany()
                        .HasForeignKey("QuestionsQuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CENSO.Models.Theme", null)
                        .WithMany()
                        .HasForeignKey("ThemesThemeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CENSO.Models.Question", b =>
                {
                    b.Navigation("request");
                });
#pragma warning restore 612, 618
        }
    }
}
