using Microsoft.EntityFrameworkCore;
using Semprg_BlazorIntroductory.API.Model;

namespace Semprg_BlazorIntroductory.Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) 
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StoryNode>(b =>
        {
            b.HasKey(s => s.Id);
            b.Property(s => s.Id)
                .ValueGeneratedOnAdd();

            b.Property(s => s.StoryContent)
                .HasMaxLength(200);

            b.HasMany(s => s.ActionsChildren)
                .WithOne(a => a.StoryNodeParent);

            b.HasOne(s => s.ParentAction)
                .WithOne(a => a.Consequence);

            b.Property(s => s.HasReachedMaxActions);
        });

        modelBuilder.Entity<StoryAction>(b =>
        {
            b.HasKey(a => a.Id);
            b.Property(a => a.Id)
                .ValueGeneratedOnAdd();

            b.Property(a => a.ActionContent)
                .HasMaxLength(400);

            b.HasOne(a => a.Consequence)
                .WithOne(s => s.ParentAction)
                .HasForeignKey<StoryNode>(s => s.ParentActionId);
        });
    }

    public DbSet<StoryNode> StoryNodes { get; set; } = null!;
    public DbSet<StoryAction> StoryActions { get; set; } = null!;
}