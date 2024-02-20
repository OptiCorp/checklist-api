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

            modelBuilder.Entity("Domain.Entities.ChecklistAggregate.ChecklistItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ChecklistId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateOnly>("Created")
                        .HasColumnType("date");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ItemId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<Guid>("TemplateId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ChecklistId");

                    b.HasIndex("TemplateId");

                    b.HasIndex("ItemId", "ChecklistId")
                        .IsUnique();

                    b.ToTable("ChecklistItems");
                });

            modelBuilder.Entity("Domain.Entities.ChecklistAggregate.ChecklistItemQuestion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Checked")
                        .HasColumnType("bit");

                    b.Property<Guid>("ChecklistItemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateOnly>("Created")
                        .HasColumnType("date");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("NotApplicable")
                        .HasColumnType("bit");

                    b.Property<Guid>("QuestionTemplateId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ChecklistItemId");

                    b.ToTable("ChecklistItemQuestions");
                });

            modelBuilder.Entity("Domain.Entities.TemplateAggregate.QuestionTemplate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateOnly>("Created")
                        .HasColumnType("date");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ItemTemplateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Question")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.HasIndex("ItemTemplateId");

                    b.ToTable("QuestionTemplate");
                });

            modelBuilder.Entity("MobDeMob.Domain.Entities.ChecklistAggregate.Checklist", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateOnly>("Created")
                        .HasColumnType("date");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Parts")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Checklists");
                });

            modelBuilder.Entity("MobDeMob.Domain.Entities.ChecklistAggregate.Punch", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ChecklistItemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateOnly>("Created")
                        .HasColumnType("date");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageBlobUris")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ChecklistItemId");

                    b.ToTable("Punches");
                });

            modelBuilder.Entity("MobDeMob.Domain.Entities.Mobilization", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ChecklistId")
                        .HasColumnType("uniqueidentifier");

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

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ChecklistId")
                        .IsUnique();

                    b.ToTable("Mobilizations");
                });

            modelBuilder.Entity("MobDeMob.Domain.ItemAggregate.ItemTemplate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateOnly>("Created")
                        .HasColumnType("date");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ItemId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

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

                    b.ToTable("ItemTemplates");
                });

            modelBuilder.Entity("Domain.Entities.ChecklistAggregate.ChecklistItem", b =>
                {
                    b.HasOne("MobDeMob.Domain.Entities.ChecklistAggregate.Checklist", null)
                        .WithMany("ChecklistItems")
                        .HasForeignKey("ChecklistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MobDeMob.Domain.ItemAggregate.ItemTemplate", "Template")
                        .WithMany()
                        .HasForeignKey("TemplateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Template");
                });

            modelBuilder.Entity("Domain.Entities.ChecklistAggregate.ChecklistItemQuestion", b =>
                {
                    b.HasOne("Domain.Entities.ChecklistAggregate.ChecklistItem", null)
                        .WithMany("Questions")
                        .HasForeignKey("ChecklistItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.TemplateAggregate.QuestionTemplate", b =>
                {
                    b.HasOne("MobDeMob.Domain.ItemAggregate.ItemTemplate", null)
                        .WithMany("Questions")
                        .HasForeignKey("ItemTemplateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MobDeMob.Domain.Entities.ChecklistAggregate.Punch", b =>
                {
                    b.HasOne("Domain.Entities.ChecklistAggregate.ChecklistItem", "ChecklistItem")
                        .WithMany("Punches")
                        .HasForeignKey("ChecklistItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChecklistItem");
                });

            modelBuilder.Entity("MobDeMob.Domain.Entities.Mobilization", b =>
                {
                    b.HasOne("MobDeMob.Domain.Entities.ChecklistAggregate.Checklist", "Checklist")
                        .WithOne("Mobilization")
                        .HasForeignKey("MobDeMob.Domain.Entities.Mobilization", "ChecklistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Checklist");
                });

            modelBuilder.Entity("Domain.Entities.ChecklistAggregate.ChecklistItem", b =>
                {
                    b.Navigation("Punches");

                    b.Navigation("Questions");
                });

            modelBuilder.Entity("MobDeMob.Domain.Entities.ChecklistAggregate.Checklist", b =>
                {
                    b.Navigation("ChecklistItems");

                    b.Navigation("Mobilization");
                });

            modelBuilder.Entity("MobDeMob.Domain.ItemAggregate.ItemTemplate", b =>
                {
                    b.Navigation("Questions");
                });
#pragma warning restore 612, 618
        }
    }
}
