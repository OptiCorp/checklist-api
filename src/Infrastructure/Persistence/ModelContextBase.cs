
using Domain.Entities.ChecklistAggregate;
using Domain.Entities.TemplateAggregate;
using Microsoft.EntityFrameworkCore;
using MobDeMob.Domain.Entities;
using MobDeMob.Domain.Entities.ChecklistAggregate;
using MobDeMob.Domain.ItemAggregate;

namespace MobDeMob.Infrastructure;

public class ModelContextBase : DbContext
{
    public ModelContextBase(DbContextOptions<ModelContextBase> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Mobilization>()
            .HasOne(m => m.Checklist)
            .WithOne(c => c.Mobilization)
            .HasForeignKey<Mobilization>(m => m.ChecklistId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Checklist>()
            .HasOne(c => c.Mobilization)
            .WithOne(m => m.Checklist)
            .HasForeignKey<Mobilization>(c => c.ChecklistId)
            .IsRequired();

        modelBuilder.Entity<ItemTemplate>()
            .HasMany(it => it.Questions)
            .WithOne()
            .HasForeignKey(nameof(ItemTemplate) + "Id") // Shadow property
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<QuestionTemplate>()
            .Property(qt => qt.Question)
            .IsRequired()
            .HasMaxLength(500); // Random value, pls change

        modelBuilder.Entity<ChecklistItem>()
            .HasMany(it => it.Questions)
            .WithOne()
            .HasForeignKey(q => q.ChecklistItemId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ChecklistItem>()
            .HasMany(c => c.Punches)
            .WithOne(p => p.ChecklistItem)
            .HasForeignKey(p => p.ChecklistItemId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ChecklistItem>()
            .HasIndex(ci => new {ci.ItemId, ci.ChecklistId})
            .IsUnique();

        modelBuilder.Entity<ChecklistItemQuestion>();

        //modelBuilder.Entity<Part>()
        //    .HasDiscriminator<PartType>("Type")
        //    .HasValue<Item>(PartType.Item)
        //    .HasValue<Unit>(PartType.Unit)
        //    .HasValue<Assembly>(PartType.Assembly)
        //    .HasValue<SubAssembly>(PartType.SubAssembly);

        //modelBuilder.Entity<Part>()
        //    .HasOne(p => p.ParentPart)
        //    .WithMany(p => p.Children)
        //    .HasForeignKey(p => p.PartParentId);

        //modelBuilder.Entity<ChecklistSectionTemplate>()
        //    .HasMany(s => s.SubSections)
        //    .WithOne(s => s.ParentChecklistSectionTemplate)
        //    .HasForeignKey(s => s.ParentChecklistSectionTemplateId)
        //    .OnDelete(DeleteBehavior.Restrict);



        // modelBuilder.Entity<ChecklistTemplate>() //many-to-many for ChecklistTemplate and ChecklistItem
        //     .HasMany(e => e.ChecklistItems)
        //     .WithMany(e => e.ChecklistTemplates)
        //     .UsingEntity<ChecklistTemplateChecklistItem>();

        //  modelBuilder.Entity<Item>() //Many-to-many for item and mobilization
        //     .HasMany(e => e.Mobilizations)
        //     .WithMany()
        //     .UsingEntity<ItemMobilization>();

        // modelBuilder.Entity<Item>() //on delete behaviour for an Item
        //     .HasOne(e => e.Parent)
        //     .WithMany(e => e.Children)
        //     .HasForeignKey(i => i.ParentId)
        //     .OnDelete(DeleteBehavior.Restrict);

        // modelBuilder.Entity<Checklist>()
        //     .HasOne(e => e.Item)
        //     .WithMany()
        //     .HasForeignKey(e => e.ItemId)
        //     .OnDelete(DeleteBehavior.Restrict);
    }


    //public DbSet<User> Users { get; set; } = null!;
    //public DbSet<PartTemplate> PartTemplates { get; set; } = null!;
    //public DbSet<Part> Parts { get; set; } = null!;
    //public DbSet<ChecklistSection> ChecklistSections { get; set; } = null!;
    public DbSet<Checklist> Checklists { get; set; } = null!;
    public DbSet<ChecklistItem> ChecklistItems { get; set; } = null!;
    public DbSet<ChecklistItemQuestion> ChecklistItemQuestions { get; set; } = null!;
    public DbSet<Mobilization> Mobilizations { get; set; } = null!;
    public DbSet<ItemTemplate> ItemTemplates { get; set; } = null!;

    public DbSet<Punch> Punches {get; set;} = null!;
    //public DbSet<Punch> Punches { get; set; } = null!;
}
