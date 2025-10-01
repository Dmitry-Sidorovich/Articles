using Articles.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Articles.Infrastructure.DataAccess;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    { }
    
    public DbSet<Article> Articles { get; set; }
    
    public DbSet<User> Users { get; set; }
}