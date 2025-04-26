using Microsoft.EntityFrameworkCore;

namespace Homework_SkillTree.Models
{
    public class masterDbContext : DbContext
    {
        public masterDbContext(DbContextOptions<masterDbContext> options) : base(options)
        {
        }

        public DbSet<BookKeeping> BookKeepings { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookKeeping>(entity => 
            { 
                entity.Property(d => d.Id).ValueGeneratedOnAdd();
                entity.Property(d => d.Category).HasMaxLength(2);
            });
        }
    }
}
