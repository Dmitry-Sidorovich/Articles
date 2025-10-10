using Articles.Domain.Entities;
using Articles.Infrastructure.DataAccess.Contexts.Articles.Configurations;
using Articles.Infrastructure.DataAccess.Contexts.Files.Configurations;
using Articles.Infrastructure.DataAccess.Contexts.Users.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Articles.Infrastructure.DataAccess;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    { }
    
    public DbSet<Article> Articles { get; set; }
    
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new ArticleConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new FileConfiguration());
    }
}