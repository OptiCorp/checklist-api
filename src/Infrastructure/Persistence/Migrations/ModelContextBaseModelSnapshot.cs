﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MobDeMob.Infrastructure;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(ModelContextBase))]
    partial class ModelContextBaseModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MobDeMob.Domain.Entities.ChecklistAggregate.Checklist", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateOnly>("Created")
                        .HasColumnType("date");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Checklists");
                });

            modelBuilder.Entity("MobDeMob.Domain.Entities.ChecklistAggregate.ChecklistSection", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ChecklistId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ChecklistSectionId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ChecklistSectionTemplateId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateOnly>("Created")
                        .HasColumnType("date");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsValidated")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PartId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ChecklistId");

                    b.HasIndex("ChecklistSectionId");

                    b.HasIndex("ChecklistSectionTemplateId");

                    b.HasIndex("PartId");

                    b.ToTable("ChecklistSections");
                });

            modelBuilder.Entity("MobDeMob.Domain.Entities.ChecklistAggregate.ChecklistSectionTemplate", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ChecklistQuestion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ChecklistSectionTemplateId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateOnly>("Created")
                        .HasColumnType("date");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ChecklistSectionTemplateId");

                    b.ToTable("ChecklistSectionTemplate");
                });

            modelBuilder.Entity("MobDeMob.Domain.Entities.ChecklistAggregate.Punch", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateOnly>("Created")
                        .HasColumnType("date");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SectionId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("SectionId");

                    b.ToTable("Punches");
                });

            modelBuilder.Entity("MobDeMob.Domain.Entities.Mobilization.Mobilization", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ChecklistId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateOnly>("Created")
                        .HasColumnType("date");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ChecklistId");

                    b.ToTable("Mobilizations");
                });

            modelBuilder.Entity("MobDeMob.Domain.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AzureAdUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly>("Created")
                        .HasColumnType("date");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("UmId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserRole")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MobDeMob.Domain.ItemAggregate.Part", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ChecklistId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateOnly>("Created")
                        .HasColumnType("date");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PartId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PartTemplateId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("SerialNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<string>("WpId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ChecklistId");

                    b.HasIndex("PartId");

                    b.HasIndex("PartTemplateId");

                    b.ToTable("Parts");

                    b.HasDiscriminator<int>("Type");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("MobDeMob.Domain.ItemAggregate.PartTemplate", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateOnly>("Created")
                        .HasColumnType("date");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ItemCheckListTemplateId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Revision")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ItemCheckListTemplateId");

                    b.ToTable("PartTemplates");
                });

            modelBuilder.Entity("MobDeMob.Domain.ItemAggregate.Assembly", b =>
                {
                    b.HasBaseType("MobDeMob.Domain.ItemAggregate.Part");

                    b.HasDiscriminator().HasValue(2);
                });

            modelBuilder.Entity("MobDeMob.Domain.ItemAggregate.SubAssembly", b =>
                {
                    b.HasBaseType("MobDeMob.Domain.ItemAggregate.Part");

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("MobDeMob.Domain.ItemAggregate.Unit", b =>
                {
                    b.HasBaseType("MobDeMob.Domain.ItemAggregate.Part");

                    b.HasDiscriminator().HasValue(3);
                });

            modelBuilder.Entity("MobDeMob.Domain.Entities.ChecklistAggregate.ChecklistSection", b =>
                {
                    b.HasOne("MobDeMob.Domain.Entities.ChecklistAggregate.Checklist", null)
                        .WithMany("ChecklistSections")
                        .HasForeignKey("ChecklistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MobDeMob.Domain.Entities.ChecklistAggregate.ChecklistSection", null)
                        .WithMany("SubSections")
                        .HasForeignKey("ChecklistSectionId");

                    b.HasOne("MobDeMob.Domain.Entities.ChecklistAggregate.ChecklistSectionTemplate", "ChecklistSectionTemplate")
                        .WithMany()
                        .HasForeignKey("ChecklistSectionTemplateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MobDeMob.Domain.ItemAggregate.Part", "Part")
                        .WithMany()
                        .HasForeignKey("PartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChecklistSectionTemplate");

                    b.Navigation("Part");
                });

            modelBuilder.Entity("MobDeMob.Domain.Entities.ChecklistAggregate.ChecklistSectionTemplate", b =>
                {
                    b.HasOne("MobDeMob.Domain.Entities.ChecklistAggregate.ChecklistSectionTemplate", null)
                        .WithMany("SubSections")
                        .HasForeignKey("ChecklistSectionTemplateId");
                });

            modelBuilder.Entity("MobDeMob.Domain.Entities.ChecklistAggregate.Punch", b =>
                {
                    b.HasOne("MobDeMob.Domain.Entities.ChecklistAggregate.ChecklistSection", "Section")
                        .WithMany("Punches")
                        .HasForeignKey("SectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Section");
                });

            modelBuilder.Entity("MobDeMob.Domain.Entities.Mobilization.Mobilization", b =>
                {
                    b.HasOne("MobDeMob.Domain.Entities.ChecklistAggregate.Checklist", "Checklist")
                        .WithMany()
                        .HasForeignKey("ChecklistId");

                    b.Navigation("Checklist");
                });

            modelBuilder.Entity("MobDeMob.Domain.ItemAggregate.Part", b =>
                {
                    b.HasOne("MobDeMob.Domain.Entities.ChecklistAggregate.Checklist", null)
                        .WithMany("Parts")
                        .HasForeignKey("ChecklistId");

                    b.HasOne("MobDeMob.Domain.ItemAggregate.Part", null)
                        .WithMany("Children")
                        .HasForeignKey("PartId");

                    b.HasOne("MobDeMob.Domain.ItemAggregate.PartTemplate", "PartTemplate")
                        .WithMany("Parts")
                        .HasForeignKey("PartTemplateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PartTemplate");
                });

            modelBuilder.Entity("MobDeMob.Domain.ItemAggregate.PartTemplate", b =>
                {
                    b.HasOne("MobDeMob.Domain.Entities.ChecklistAggregate.ChecklistSectionTemplate", "ItemCheckListTemplate")
                        .WithMany()
                        .HasForeignKey("ItemCheckListTemplateId");

                    b.Navigation("ItemCheckListTemplate");
                });

            modelBuilder.Entity("MobDeMob.Domain.Entities.ChecklistAggregate.Checklist", b =>
                {
                    b.Navigation("ChecklistSections");

                    b.Navigation("Parts");
                });

            modelBuilder.Entity("MobDeMob.Domain.Entities.ChecklistAggregate.ChecklistSection", b =>
                {
                    b.Navigation("Punches");

                    b.Navigation("SubSections");
                });

            modelBuilder.Entity("MobDeMob.Domain.Entities.ChecklistAggregate.ChecklistSectionTemplate", b =>
                {
                    b.Navigation("SubSections");
                });

            modelBuilder.Entity("MobDeMob.Domain.ItemAggregate.Part", b =>
                {
                    b.Navigation("Children");
                });

            modelBuilder.Entity("MobDeMob.Domain.ItemAggregate.PartTemplate", b =>
                {
                    b.Navigation("Parts");
                });
#pragma warning restore 612, 618
        }
    }
}
