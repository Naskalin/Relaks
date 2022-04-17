﻿// <auto-generated />
using System;
using App.DbConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace App.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220415221720_AddPosts")]
    partial class AddPosts
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.2");

            modelBuilder.Entity("App.Models.Entry", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("Avatar")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("DeletedReason")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("EndAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("EntryType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Reputation")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("StartAt")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Entries");
                });

            modelBuilder.Entity("App.Models.EntryInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("DeletedReason")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("EntryId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("EntryInfos", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("EntryInfo");
                });

            modelBuilder.Entity("App.Models.FileModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("DeletedReason")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Files", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("FileModel");
                });

            modelBuilder.Entity("App.Models.Post", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("AnyField")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("App.Models.PostFts", b =>
                {
                    b.Property<int>("RowId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Match")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("PostFts");

                    b.Property<double?>("Rank")
                        .HasColumnType("REAL");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("RowId");

                    b.ToTable("PostFts");
                });

            modelBuilder.Entity("App.Models.EntryDate", b =>
                {
                    b.HasBaseType("App.Models.EntryInfo");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.HasIndex("EntryId");

                    b.HasDiscriminator().HasValue("Date");
                });

            modelBuilder.Entity("App.Models.EntryEmail", b =>
                {
                    b.HasBaseType("App.Models.EntryInfo");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasIndex("EntryId");

                    b.HasDiscriminator().HasValue("Email");
                });

            modelBuilder.Entity("App.Models.EntryFile", b =>
                {
                    b.HasBaseType("App.Models.FileModel");

                    b.Property<Guid>("EntryId")
                        .HasColumnType("TEXT");

                    b.HasIndex("EntryId");

                    b.HasDiscriminator().HasValue("EntryFile");
                });

            modelBuilder.Entity("App.Models.EntryInfoFile", b =>
                {
                    b.HasBaseType("App.Models.FileModel");

                    b.Property<Guid>("EntryInfoId")
                        .HasColumnType("TEXT");

                    b.HasIndex("EntryInfoId");

                    b.HasDiscriminator().HasValue("EntryInfoFile");
                });

            modelBuilder.Entity("App.Models.EntryNote", b =>
                {
                    b.HasBaseType("App.Models.EntryInfo");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasIndex("EntryId");

                    b.HasDiscriminator().HasValue("Note");
                });

            modelBuilder.Entity("App.Models.EntryPhone", b =>
                {
                    b.HasBaseType("App.Models.EntryInfo");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneRegion")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasIndex("EntryId");

                    b.HasDiscriminator().HasValue("Phone");
                });

            modelBuilder.Entity("App.Models.EntryUrl", b =>
                {
                    b.HasBaseType("App.Models.EntryInfo");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasIndex("EntryId");

                    b.HasDiscriminator().HasValue("Url");
                });

            modelBuilder.Entity("App.Models.EntryDate", b =>
                {
                    b.HasOne("App.Models.Entry", null)
                        .WithMany("Dates")
                        .HasForeignKey("EntryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("App.Models.EntryEmail", b =>
                {
                    b.HasOne("App.Models.Entry", null)
                        .WithMany("Emails")
                        .HasForeignKey("EntryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("App.Models.EntryFile", b =>
                {
                    b.HasOne("App.Models.Entry", null)
                        .WithMany("Files")
                        .HasForeignKey("EntryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("App.Models.EntryInfoFile", b =>
                {
                    b.HasOne("App.Models.EntryInfo", null)
                        .WithMany("Files")
                        .HasForeignKey("EntryInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("App.Models.EntryNote", b =>
                {
                    b.HasOne("App.Models.Entry", null)
                        .WithMany("Notes")
                        .HasForeignKey("EntryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("App.Models.EntryPhone", b =>
                {
                    b.HasOne("App.Models.Entry", null)
                        .WithMany("Phones")
                        .HasForeignKey("EntryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("App.Models.EntryUrl", b =>
                {
                    b.HasOne("App.Models.Entry", null)
                        .WithMany("Urls")
                        .HasForeignKey("EntryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("App.Models.Entry", b =>
                {
                    b.Navigation("Dates");

                    b.Navigation("Emails");

                    b.Navigation("Files");

                    b.Navigation("Notes");

                    b.Navigation("Phones");

                    b.Navigation("Urls");
                });

            modelBuilder.Entity("App.Models.EntryInfo", b =>
                {
                    b.Navigation("Files");
                });
#pragma warning restore 612, 618
        }
    }
}