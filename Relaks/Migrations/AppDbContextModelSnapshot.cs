﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Relaks.Database;

#nullable disable

namespace Relaks.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.5");

            modelBuilder.Entity("Relaks.Models.BaseEntry", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("DeletedReason")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("EndAt")
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

                    b.HasDiscriminator<string>("Discriminator").HasValue("BaseEntry");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Relaks.Models.BaseEntryInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("DeletedReason")
                        .HasColumnType("TEXT");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("EntryId")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsFavorite")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("EntryId");

                    b.ToTable("EntryInfos");

                    b.HasDiscriminator<string>("Discriminator").HasValue("BaseEntryInfo");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Relaks.Models.FtsEntry", b =>
                {
                    b.Property<int>("RowId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("DeletedAt")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Match")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("FtsEntries");

                    b.Property<double?>("Rank")
                        .HasColumnType("REAL");

                    b.Property<string>("Snippet")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("RowId");

                    b.ToTable("FtsEntries", null, t =>
                        {
                            t.ExcludeFromMigrations();
                        });
                });

            modelBuilder.Entity("Relaks.Models.FtsEntryInfo", b =>
                {
                    b.Property<int>("RowId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("DeletedAt")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("EntryId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Match")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("FtsEntryInfos");

                    b.Property<double?>("Rank")
                        .HasColumnType("REAL");

                    b.Property<string>("Snippet")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("RowId");

                    b.ToTable("FtsEntryInfos", null, t =>
                        {
                            t.ExcludeFromMigrations();
                        });
                });

            modelBuilder.Entity("Relaks.Models.ECompany", b =>
                {
                    b.HasBaseType("Relaks.Models.BaseEntry");

                    b.ToTable("Entries");

                    b.HasDiscriminator().HasValue("ECompany");
                });

            modelBuilder.Entity("Relaks.Models.EMeet", b =>
                {
                    b.HasBaseType("Relaks.Models.BaseEntry");

                    b.ToTable("Entries");

                    b.HasDiscriminator().HasValue("EMeet");
                });

            modelBuilder.Entity("Relaks.Models.EPerson", b =>
                {
                    b.HasBaseType("Relaks.Models.BaseEntry");

                    b.ToTable("Entries");

                    b.HasDiscriminator().HasValue("EPerson");
                });

            modelBuilder.Entity("Relaks.Models.EiCustom", b =>
                {
                    b.HasBaseType("Relaks.Models.BaseEntryInfo");

                    b.Property<string>("CustomValue")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.ToTable("EntryInfos");

                    b.HasDiscriminator().HasValue("EiCustom");
                });

            modelBuilder.Entity("Relaks.Models.EiDate", b =>
                {
                    b.HasBaseType("Relaks.Models.BaseEntryInfo");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsOnlyDate")
                        .HasColumnType("INTEGER");

                    b.ToTable("EntryInfos");

                    b.HasDiscriminator().HasValue("EiDate");
                });

            modelBuilder.Entity("Relaks.Models.EiEmail", b =>
                {
                    b.HasBaseType("Relaks.Models.BaseEntryInfo");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.ToTable("EntryInfos");

                    b.HasDiscriminator().HasValue("EiEmail");
                });

            modelBuilder.Entity("Relaks.Models.EiPhone", b =>
                {
                    b.HasBaseType("Relaks.Models.BaseEntryInfo");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Region")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.ToTable("EntryInfos");

                    b.HasDiscriminator().HasValue("EiPhone");
                });

            modelBuilder.Entity("Relaks.Models.EiUrl", b =>
                {
                    b.HasBaseType("Relaks.Models.BaseEntryInfo");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.ToTable("EntryInfos");

                    b.HasDiscriminator().HasValue("EiUrl");
                });

            modelBuilder.Entity("Relaks.Models.BaseEntryInfo", b =>
                {
                    b.HasOne("Relaks.Models.BaseEntry", "Entry")
                        .WithMany("EntryInfos")
                        .HasForeignKey("EntryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Entry");
                });

            modelBuilder.Entity("Relaks.Models.BaseEntry", b =>
                {
                    b.Navigation("EntryInfos");
                });
#pragma warning restore 612, 618
        }
    }
}
