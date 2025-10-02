using Articles.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Articles.Infrastructure.DataAccess.Contexts.Users.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
       builder.HasKey(x => x.Id);
       builder.Property(x => x.Name).HasMaxLength(128).IsRequired();
       
       builder.HasMany(x => x.Articles).WithOne(x => x.User).HasForeignKey(x => x.UserId).IsRequired().OnDelete(DeleteBehavior.Cascade);
    }
}