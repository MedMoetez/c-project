﻿// <auto-generated />
using System;
using AppelsDOffresApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AppelsDOffresApp.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240717142225_njarbou")]
    partial class njarbou
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AppelsDOffresApp.Models.AppelDOffreCollaborateur", b =>
                {
                    b.Property<int>("AppelDOffreId")
                        .HasColumnType("int");

                    b.Property<int>("UtilisateurId")
                        .HasColumnType("int");

                    b.HasKey("AppelDOffreId", "UtilisateurId");

                    b.HasIndex("UtilisateurId");

                    b.ToTable("AppelDOffreCollaborateurs");
                });

            modelBuilder.Entity("AppelsDOffresApp.Models.AppelOffre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DescriptionOffre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GestionnaireId")
                        .HasColumnType("int");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int?>("UtilisateurId")
                        .HasColumnType("int");

                    b.Property<int?>("UtilisateurId1")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GestionnaireId");

                    b.HasIndex("UtilisateurId");

                    b.HasIndex("UtilisateurId1");

                    b.ToTable("AppelsDOffres");
                });

            modelBuilder.Entity("AppelsDOffresApp.Models.Documents", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AppelDOffreId")
                        .HasColumnType("int");

                    b.Property<int>("AppelOffreId")
                        .HasColumnType("int");

                    b.Property<byte[]>("Contenu")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AppelOffreId");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("AppelsDOffresApp.Models.Rappel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UtilisateurId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UtilisateurId");

                    b.ToTable("Rappels");
                });

            modelBuilder.Entity("AppelsDOffresApp.Models.Utilisateur", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MotDePasse")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Utilisateurs");
                });

            modelBuilder.Entity("AppelsDOffresApp.Models.AppelDOffreCollaborateur", b =>
                {
                    b.HasOne("AppelsDOffresApp.Models.AppelOffre", "AppelDOffre")
                        .WithMany("Collaborateurs")
                        .HasForeignKey("AppelDOffreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppelsDOffresApp.Models.Utilisateur", "Utilisateur")
                        .WithMany()
                        .HasForeignKey("UtilisateurId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppelDOffre");

                    b.Navigation("Utilisateur");
                });

            modelBuilder.Entity("AppelsDOffresApp.Models.AppelOffre", b =>
                {
                    b.HasOne("AppelsDOffresApp.Models.Utilisateur", "Gestionnaire")
                        .WithMany()
                        .HasForeignKey("GestionnaireId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AppelsDOffresApp.Models.Utilisateur", null)
                        .WithMany("AppelsDOffresCollaborateur")
                        .HasForeignKey("UtilisateurId");

                    b.HasOne("AppelsDOffresApp.Models.Utilisateur", null)
                        .WithMany("AppelsDOffresGestionnaire")
                        .HasForeignKey("UtilisateurId1");

                    b.Navigation("Gestionnaire");
                });

            modelBuilder.Entity("AppelsDOffresApp.Models.Documents", b =>
                {
                    b.HasOne("AppelsDOffresApp.Models.AppelOffre", "AppelOffre")
                        .WithMany("Documents")
                        .HasForeignKey("AppelOffreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppelOffre");
                });

            modelBuilder.Entity("AppelsDOffresApp.Models.Rappel", b =>
                {
                    b.HasOne("AppelsDOffresApp.Models.Utilisateur", "Utilisateur")
                        .WithMany("Rappels")
                        .HasForeignKey("UtilisateurId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Utilisateur");
                });

            modelBuilder.Entity("AppelsDOffresApp.Models.AppelOffre", b =>
                {
                    b.Navigation("Collaborateurs");

                    b.Navigation("Documents");
                });

            modelBuilder.Entity("AppelsDOffresApp.Models.Utilisateur", b =>
                {
                    b.Navigation("AppelsDOffresCollaborateur");

                    b.Navigation("AppelsDOffresGestionnaire");

                    b.Navigation("Rappels");
                });
#pragma warning restore 612, 618
        }
    }
}
