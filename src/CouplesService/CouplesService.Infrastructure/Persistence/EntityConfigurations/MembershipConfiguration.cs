using CouplesService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CouplesService.Infrastructure.Persistence.EntityConfigurations;

public sealed class MembershipConfiguration : IEntityTypeConfiguration<Membership>
{
    public void Configure(EntityTypeBuilder<Membership> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.CreatedAt);
        builder.Property(x => x.UpdatedAt);

        builder
            .HasOne(x => x.User)
            .WithMany(x => x.Memberships)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder
            .HasOne(x => x.Couple)
            .WithMany(x => x.Memberships)
            .HasForeignKey(x => x.CoupleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}