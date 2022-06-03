﻿// <auto-generated />
using System;
using App.DbConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace App.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<Guid>("EntryId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("EntryId");

                    b.ToTable("EntryInfos", (string)null);
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

            modelBuilder.Entity("App.Models.FtsEntry", b =>
                {
                    b.Property<int>("RowId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Match")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("FtsEntry");

                    b.Property<double?>("Rank")
                        .HasColumnType("REAL");

                    b.HasKey("RowId");

                    b.ToTable("FtsEntry");
                });

            modelBuilder.Entity("App.Models.FtsEntryInfo", b =>
                {
                    b.Property<int>("RowId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("EntryId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Match")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("FtsEntryInfo");

                    b.Property<double?>("Rank")
                        .HasColumnType("REAL");

                    b.HasKey("RowId");

                    b.ToTable("FtsEntryInfo");
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

            modelBuilder.Entity("App.Models.EntryInfo", b =>
                {
                    b.HasOne("App.Models.Entry", null)
                        .WithMany("EntryInfos")
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

            modelBuilder.Entity("App.Models.Entry", b =>
                {
                    b.Navigation("EntryInfos");

                    b.Navigation("Files");
                });

            modelBuilder.Entity("App.Models.EntryInfo", b =>
                {
                    b.Navigation("Files");
                });
#pragma warning restore 612, 618
        }
    }
}
