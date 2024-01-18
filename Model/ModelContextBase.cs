
using Microsoft.EntityFrameworkCore;
using Model.Entities;

namespace Model.Context;

public class ModelContextBase : DbContext
{
    public ModelContextBase(DbContextOptions<ModelContextBase> options) : base(options)
    {

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
