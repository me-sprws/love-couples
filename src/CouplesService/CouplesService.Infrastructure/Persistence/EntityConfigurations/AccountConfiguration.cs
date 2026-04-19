using CouplesService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CouplesService.Infrastructure.Persistence.EntityConfigurations;

public sealed class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.HasKey(e => e.Id);

        builder
            .HasOne(x => x.User)
            .WithMany(x => x.Accounts)
            .HasForeignKey(e => e.UserId);
        
        builder
            .Property(x => x.Username)
            .IsRequired()
            .HasMaxLength(200);
        
        builder
            .Property(x => x.ExternalId)
            .IsRequired()
            .HasMaxLength(200);
        
        builder
            .HasDiscriminator<string>("AccountType")
            .HasValue<GoogleAccount>("Google")
            .HasValue<ServiceAccount>("Service");
    }
}

public sealed class EmailAccountConfiguration : IEntityTypeConfiguration<EmailAccount>
{
    public void Configure(EntityTypeBuilder<EmailAccount> builder)
    {
        builder
            .Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(254);
    }
}