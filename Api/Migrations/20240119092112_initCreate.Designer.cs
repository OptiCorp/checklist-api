﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Model.Context;

#nullable disable

namespace Api.Migrations
{
    [DbContext(typeof(ModelContextBase))]
    [Migration("20240119092112_initCreate")]
    partial class initCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Model.Entities.Checklist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ChecklistTemplateId")
                        .HasColumnType("int");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<int>("MobilizationId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ChecklistTemplateId");

                    b.HasIndex("ItemId");

                    b.HasIndex("MobilizationId");

                    b.ToTable("Checklists");
                });

            modelBuilder.Entity("Model.Entities.ChecklistChecklistItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ChecklistId")
                        .HasColumnType("int");

                    b.Property<int>("ChecklistItemId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ChecklistId");

                    b.HasIndex("ChecklistItemId");

                    b.ToTable("ChecklistChecklistItem");
                });

            modelBuilder.Entity("Model.Entities.ChecklistItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Question")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.ToTable("ChecklistItems");
                });

            modelBuilder.Entity("Model.Entities.ChecklistTemplate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ItemTemplateId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ItemTemplateId")
                        .IsUnique();

                    b.ToTable("ChecklistTemplates");
                });

            modelBuilder.Entity("Model.Entities.ChecklistTemplateChecklistItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ChecklistItemId")
                        .HasColumnType("int");

                    b.Property<int>("ChecklistTemplateId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ChecklistItemId");

                    b.HasIndex("ChecklistTemplateId");

                    b.ToTable("ChecklistTemplateChecklistItem");
                });

            modelBuilder.Entity("Model.Entities.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Comment")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("CreatedById")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly>("CreatedDate")
                        .HasColumnType("date");

                    b.Property<int>("ItemTemplateId")
                        .HasColumnType("int");

                    b.Property<int>("ParentId")
                        .HasColumnType("int");

                    b.Property<string>("SerialNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly?>("UpdatedDate")
                        .HasColumnType("date");

                    b.Property<string>("WpId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ItemTemplateId");

                    b.HasIndex("ParentId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("Model.Entities.ItemMobilization", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<int>("MobilizationId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.HasIndex("MobilizationId");

                    b.ToTable("ItemMobilization");
                });

            modelBuilder.Entity("Model.Entities.ItemTemplate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CreatedById")
                        .HasColumnType("int");

                    b.Property<DateOnly>("CreatedDate")
                        .HasColumnType("date");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Revision")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly?>("UpdatedDate")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.ToTable("ItemTemplates");
                });

            modelBuilder.Entity("Model.Entities.Mobilization", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("CreatedDate")
                        .HasColumnType("date");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MobilizationType")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Mobilizations");
                });

            modelBuilder.Entity("Model.Entities.Punch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ChecklistChecklistItemId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly>("PunchCreated")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.HasIndex("ChecklistChecklistItemId")
                        .IsUnique();

                    b.ToTable("Puches");
                });

            modelBuilder.Entity("Model.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AzureAdUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly>("CreatedDate")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("UmId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly?>("UpdatedDate")
                        .HasColumnType("date");

                    b.Property<string>("UserRole")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Model.Entities.Checklist", b =>
                {
                    b.HasOne("Model.Entities.ChecklistTemplate", "ChecklistTemplate")
                        .WithMany()
                        .HasForeignKey("ChecklistTemplateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Model.Entities.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Model.Entities.Mobilization", "Mobilization")
                        .WithMany()
                        .HasForeignKey("MobilizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChecklistTemplate");

                    b.Navigation("Item");

                    b.Navigation("Mobilization");
                });

            modelBuilder.Entity("Model.Entities.ChecklistChecklistItem", b =>
                {
                    b.HasOne("Model.Entities.Checklist", "Checklist")
                        .WithMany("ChecklistChecklistItems")
                        .HasForeignKey("ChecklistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Model.Entities.ChecklistItem", "ChecklistItem")
                        .WithMany()
                        .HasForeignKey("ChecklistItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Checklist");

                    b.Navigation("ChecklistItem");
                });

            modelBuilder.Entity("Model.Entities.ChecklistTemplate", b =>
                {
                    b.HasOne("Model.Entities.ItemTemplate", "ItemTemplate")
                        .WithOne("ChecklistTemplate")
                        .HasForeignKey("Model.Entities.ChecklistTemplate", "ItemTemplateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ItemTemplate");
                });

            modelBuilder.Entity("Model.Entities.ChecklistTemplateChecklistItem", b =>
                {
                    b.HasOne("Model.Entities.ChecklistItem", "ChecklistItem")
                        .WithMany("ChecklistTemplateChecklistItems")
                        .HasForeignKey("ChecklistItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Model.Entities.ChecklistTemplate", "ChecklistTemplate")
                        .WithMany("ChecklistTemplateChecklistItems")
                        .HasForeignKey("ChecklistTemplateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChecklistItem");

                    b.Navigation("ChecklistTemplate");
                });

            modelBuilder.Entity("Model.Entities.Item", b =>
                {
                    b.HasOne("Model.Entities.ItemTemplate", "ItemTemplate")
                        .WithMany()
                        .HasForeignKey("ItemTemplateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Model.Entities.Item", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ItemTemplate");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("Model.Entities.ItemMobilization", b =>
                {
                    b.HasOne("Model.Entities.Item", "Item")
                        .WithMany("ItemMobilizations")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Model.Entities.Mobilization", "Mobilization")
                        .WithMany("ItemMobilizations")
                        .HasForeignKey("MobilizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("Mobilization");
                });

            modelBuilder.Entity("Model.Entities.ItemTemplate", b =>
                {
                    b.HasOne("Model.Entities.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedBy");
                });

            modelBuilder.Entity("Model.Entities.Punch", b =>
                {
                    b.HasOne("Model.Entities.ChecklistChecklistItem", "ChecklistChecklistItem")
                        .WithOne("Puch")
                        .HasForeignKey("Model.Entities.Punch", "ChecklistChecklistItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChecklistChecklistItem");
                });

            modelBuilder.Entity("Model.Entities.Checklist", b =>
                {
                    b.Navigation("ChecklistChecklistItems");
                });

            modelBuilder.Entity("Model.Entities.ChecklistChecklistItem", b =>
                {
                    b.Navigation("Puch");
                });

            modelBuilder.Entity("Model.Entities.ChecklistItem", b =>
                {
                    b.Navigation("ChecklistTemplateChecklistItems");
                });

            modelBuilder.Entity("Model.Entities.ChecklistTemplate", b =>
                {
                    b.Navigation("ChecklistTemplateChecklistItems");
                });

            modelBuilder.Entity("Model.Entities.Item", b =>
                {
                    b.Navigation("Children");

                    b.Navigation("ItemMobilizations");
                });

            modelBuilder.Entity("Model.Entities.ItemTemplate", b =>
                {
                    b.Navigation("ChecklistTemplate");
                });

            modelBuilder.Entity("Model.Entities.Mobilization", b =>
                {
                    b.Navigation("ItemMobilizations");
                });
#pragma warning restore 612, 618
        }
    }
}
