
using Microsoft.EntityFrameworkCore;
using MobDeMob.Domain.Entities;
using MobDeMob.Domain.Entities.ChecklistAggregate;
using MobDeMob.Domain.Entities.ItemAggregate;
using MobDeMob.Domain.Entities.Mobilization;

namespace MobDeMob.Infrastructure;

public class ModelContextBase : DbContext
{
    public ModelContextBase(DbContextOptions<ModelContextBase> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

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


    public DbSet<User> Users { get; set; } = null!;
    public DbSet<ItemTemplate> ItemTemplates { get; set; } = null!;
    public DbSet<Item> Items { get; set; } = null!;
    public DbSet<ChecklistSection> ChecklistSections { get; set; } = null!;
    public DbSet<MobDeMob.Domain.Entities.ChecklistAggregate.Checklist> Checklists { get; set; } = null!; //TODO:
    public DbSet<ChecklistSectionTemplate> ChecklistSectionTemplate { get; set; } = null!;
    public DbSet<Mobilization> Mobilizations { get; set; } = null!;
    public DbSet<Punch> Punches { get; set; } = null!;
}
