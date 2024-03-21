
using Domain.Entities;
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
            .HasOne(m => m.ChecklistCollection)
            .WithOne(c => c.Mobilization)
            .HasForeignKey<Mobilization>(m => m.ChecklistCollectionId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ChecklistCollection>()
            .HasOne(c => c.Mobilization)
            .WithOne(m => m.ChecklistCollection)
            .HasForeignKey<Mobilization>(m => m.ChecklistCollectionId)
            .IsRequired();

        modelBuilder.Entity<ChecklistTemplate>()
            .HasOne(ct => ct.ItemTemplate)
            .WithOne(it => it.ChecklistTemplate)
            // .HasForeignKey(nameof(ItemTemplate) + "Id")
            .HasForeignKey<ChecklistTemplate>(ct => ct.ItemTemplateId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ItemTemplate>()
            .HasMany(itt => itt.Items)
            .WithOne(it => it.ItemTemplate)
            .HasForeignKey(it => it.ItemTemplateId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ChecklistTemplate>()
            .HasMany(ct => ct.Questions)
            .WithOne()
            .HasForeignKey(nameof(ChecklistTemplate) + "Id")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<QuestionTemplate>()
            .Property(qt => qt.Question)
            .IsRequired()
            .HasMaxLength(250); // Random value, pls change


        modelBuilder.Entity<Checklist>()
            .HasMany(it => it.Questions)
            .WithOne()
            .HasForeignKey(q => q.ChecklistId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Checklist>()
            .HasMany(c => c.Punches)
            .WithOne(p => p.Checklist)
            .HasForeignKey(p => p.ChecklistId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Checklist>()
            .HasOne(c => c.ItemTemplate)
            .WithOne()
            .HasForeignKey<Checklist>(c => c.ItemTemplateId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Checklist>()
            .HasIndex(ci => new { ci.ItemTemplateId, ci.ChecklistCollectionId })
            .IsUnique();

        modelBuilder.Entity<ChecklistQuestion>()
            .HasOne(cq => cq.QuestionTemplate)
            .WithOne()
            .HasForeignKey<ChecklistQuestion>(cq => cq.QuestionTemplateId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ChecklistQuestion>()
            .ToTable(t => t.HasCheckConstraint("CK_ChecklistQuestions_CheckedNotApplicable",
                    "([Checked] = 1 AND [NotApplicable] = 0) OR ([Checked] = 0 AND [NotApplicable] = 1) OR ([Checked] = 0 AND [NotApplicable] = 0)"));

        modelBuilder.Entity<Item>()
            .Property(i => i.Id)
            .IsRequired()
            .HasMaxLength(50);

    }

    public DbSet<ChecklistCollection> ChecklistCollections { get; set; } = null!;
    public DbSet<Mobilization> Mobilizations { get; set; } = null!;
    public DbSet<Checklist> Checklists { get; set; } = null!;
    public DbSet<ChecklistQuestion> ChecklistQuestions { get; set; } = null!;
    public DbSet<ItemTemplate> ItemTemplates { get; set; } = null!;
    public DbSet<Punch> Punches { get; set; } = null!;
    public DbSet<PunchFile> PunchFiles { get; set; } = null!;
    public DbSet<ChecklistTemplate> ChecklistTemplate { get; set; } = null!;

    public DbSet<Item> Items { get; set; } = null!;

    public DbSet<QuestionTemplate> QuestionTemplates { get; set; } = null!;

}
