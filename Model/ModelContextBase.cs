
using Microsoft.EntityFrameworkCore;
using Model.Entities;

namespace Model.Context;

public class ModelContextBase : DbContext
{
    public ModelContextBase(DbContextOptions<ModelContextBase> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ChecklistTemplate>() //many-to-many for ChecklistTemplate and ChecklistItem
            .HasMany(e => e.ChecklistItems)
            .WithMany(e => e.ChecklistTemplates)
            .UsingEntity<ChecklistTemplateChecklistItem>();

         modelBuilder.Entity<Item>() //Many-to-many for item and mobilization
            .HasMany(e => e.Mobilizations)
            .WithMany(e => e.Items)
            .UsingEntity<ItemMobilization>();

        modelBuilder.Entity<Item>() //on delete behaviour for an Item
            .HasOne(e => e.Parent)
            .WithMany(e => e.Children)
            .HasForeignKey(i => i.ParentId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Checklist>()
            .HasOne(e => e.Item)
            .WithMany()
            .HasForeignKey(e => e.ItemId)
            .OnDelete(DeleteBehavior.Restrict);
    }


    public DbSet<User> Users { get; set; } = null!;
    public DbSet<ItemTemplate> ItemTemplates { get; set; } = null!;
    public DbSet<Item> Items { get; set; } = null!;
    public DbSet<ChecklistTemplate> ChecklistTemplates { get; set; } = null!;
    public DbSet<Checklist> Checklists { get; set; } = null!;
    public DbSet<ChecklistItem> ChecklistItems { get; set; } = null!;
    public DbSet<Mobilization> Mobilizations { get; set; } = null!;
    public DbSet<Punch> Puches { get; set; } = null!;



}
