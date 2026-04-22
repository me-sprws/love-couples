using CouplesService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CouplesService.Infrastructure.Persistence.EntityConfigurations;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).HasMaxLength(35).IsRequired();
        builder.Property(x => x.Country).HasMaxLength(15);
        builder.Property(x => x.BirthDate);
        builder.Property(x => x.CreatedAt);
        builder.Property(x => x.UpdatedAt);
        
        builder
            .HasMany(x => x.Memberships)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}