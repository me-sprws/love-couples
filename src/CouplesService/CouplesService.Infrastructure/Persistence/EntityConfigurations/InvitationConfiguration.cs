using CouplesService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CouplesService.Infrastructure.Persistence.EntityConfigurations;

public sealed class InvitationConfiguration : IEntityTypeConfiguration<Invitation>
{
    public void Configure(EntityTypeBuilder<Invitation> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Code).HasMaxLength(10).IsRequired();
        builder.Property(x => x.CreatedAt);
        builder.Property(x => x.UpdatedAt);

        builder
            .HasOne(x => x.Couple)
            .WithOne(x => x.Invitation)
            .OnDelete(DeleteBehavior.Cascade);
    }
}