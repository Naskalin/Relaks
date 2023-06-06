﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Relaks.Database;

#nullable disable

namespace Relaks.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230606131219_CreateDatasetTemplateTable")]
    partial class CreateDatasetTemplateTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("NOCASE")
                .HasAnnotation("ProductVersion", "7.0.5");

            modelBuilder.Entity("BaseFileBaseFileTag", b =>
                {
                    b.Property<Guid>("FilesId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("TagsId")
                        .HasColumnType("TEXT");

                    b.HasKey("FilesId", "TagsId");

                    b.HasIndex("TagsId");

                    b.ToTable("BaseFileBaseFileTag");
                });

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

                    b.Property<bool>("EndAtWithTime")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("StartAt")
                        .HasColumnType("TEXT");

                    b.Property<bool>("StartAtWithTime")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Thumbnail")
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

            modelBuilder.Entity("Relaks.Models.BaseFile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("CategoryId")
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

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Filename")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Keyword")
                        .HasColumnType("TEXT");

                    b.Property<string>("MimeType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Files");

                    b.HasDiscriminator<string>("Discriminator").HasValue("BaseFile");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Relaks.Models.BaseFileCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("TreePath")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("FileCategories");

                    b.HasDiscriminator<string>("Discriminator").HasValue("BaseFileCategory");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Relaks.Models.BaseFileTag", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("FileTags");

                    b.HasDiscriminator<string>("Discriminator").HasValue("BaseFileTag");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Relaks.Models.Country", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Alpha2")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("TEXT");

                    b.Property<string>("Capital")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("Continent")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("Native")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("TitleEn")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("TitleRu")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("Relaks.Models.DatasetTemplate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("DatasetValue")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("DatasetTemplates");
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

            modelBuilder.Entity("Relaks.Models.FtsFile", b =>
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
                        .HasColumnName("FtsFiles");

                    b.Property<double?>("Rank")
                        .HasColumnType("REAL");

                    b.Property<string>("Snippet")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("RowId");

                    b.ToTable("FtsFiles", null, t =>
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

            modelBuilder.Entity("Relaks.Models.EiDataset", b =>
                {
                    b.HasBaseType("Relaks.Models.BaseEntryInfo");

                    b.Property<string>("DatasetValue")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.ToTable("EntryInfos");

                    b.HasDiscriminator().HasValue("EiDataset");
                });

            modelBuilder.Entity("Relaks.Models.EiDate", b =>
                {
                    b.HasBaseType("Relaks.Models.BaseEntryInfo");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<bool>("WithTime")
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

            modelBuilder.Entity("Relaks.Models.EntryFile", b =>
                {
                    b.HasBaseType("Relaks.Models.BaseFile");

                    b.Property<Guid>("EntryId")
                        .HasColumnType("TEXT");

                    b.HasIndex("EntryId");

                    b.ToTable("Files");

                    b.HasDiscriminator().HasValue("EntryFile");
                });

            modelBuilder.Entity("Relaks.Models.EntryFileCategory", b =>
                {
                    b.HasBaseType("Relaks.Models.BaseFileCategory");

                    b.Property<Guid>("EntryId")
                        .HasColumnType("TEXT");

                    b.HasIndex("EntryId");

                    b.ToTable("FileCategories");

                    b.HasDiscriminator().HasValue("EntryFileCategory");
                });

            modelBuilder.Entity("Relaks.Models.EntryFileTag", b =>
                {
                    b.HasBaseType("Relaks.Models.BaseFileTag");

                    b.Property<Guid>("EntryId")
                        .HasColumnType("TEXT");

                    b.HasIndex("EntryId");

                    b.ToTable("FileTags");

                    b.HasDiscriminator().HasValue("EntryFileTag");
                });

            modelBuilder.Entity("BaseFileBaseFileTag", b =>
                {
                    b.HasOne("Relaks.Models.BaseFile", null)
                        .WithMany()
                        .HasForeignKey("FilesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Relaks.Models.BaseFileTag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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

            modelBuilder.Entity("Relaks.Models.BaseFile", b =>
                {
                    b.HasOne("Relaks.Models.BaseFileCategory", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Relaks.Models.BaseFileCategory", b =>
                {
                    b.HasOne("Relaks.Models.BaseFileCategory", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("Relaks.Models.EntryFile", b =>
                {
                    b.HasOne("Relaks.Models.BaseEntry", "Entry")
                        .WithMany()
                        .HasForeignKey("EntryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Entry");
                });

            modelBuilder.Entity("Relaks.Models.EntryFileCategory", b =>
                {
                    b.HasOne("Relaks.Models.BaseEntry", "Entry")
                        .WithMany()
                        .HasForeignKey("EntryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Entry");
                });

            modelBuilder.Entity("Relaks.Models.EntryFileTag", b =>
                {
                    b.HasOne("Relaks.Models.BaseEntry", "Entry")
                        .WithMany()
                        .HasForeignKey("EntryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Entry");
                });

            modelBuilder.Entity("Relaks.Models.BaseEntry", b =>
                {
                    b.Navigation("EntryInfos");
                });

            modelBuilder.Entity("Relaks.Models.BaseFileCategory", b =>
                {
                    b.Navigation("Children");
                });
#pragma warning restore 612, 618
        }
    }
}