using CouplesService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CouplesService.Infrastructure.Persistence.EntityConfigurations;

public sealed class CoupleConfiguration : IEntityTypeConfiguration<Couple>
{
    public void Configure(EntityTypeBuilder<Couple> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Status).HasMaxLength(255).IsRequired();
        builder.Property(x => x.TogetherSince);
        builder.Property(x => x.SeparatedAt);
        builder.Property(x => x.CreatedAt);
        builder.Property(x => x.UpdatedAt);
        
        builder
            .HasMany(x => x.Memberships)
            .WithOne(x => x.Couple)
            .HasForeignKey(x => x.CoupleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(x => x.Invitation)
            .WithOne(x => x.Couple)
            .HasForeignKey<Invitation>(x => x.CoupleId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder
            .Navigation(x => x.Memberships)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}