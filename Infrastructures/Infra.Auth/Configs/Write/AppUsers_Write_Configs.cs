using Domains.Auth.AppUserEntity.Aggregate;
using Domains.Auth.AppUserEntity.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Auth.Extensions;

namespace Infra.Auth.Configs.Write;
internal class AppUsers_Write_Configs :
    IEntityTypeConfiguration<AppUser> {
    public void Configure(EntityTypeBuilder<AppUser> builder) {
        builder.HasIndex(x => x.Id).IsUnique();
        builder.Property(p => p.Id).IsRequired();
        builder.Property(p => p.Email).IsRequired();

        builder.Property(p => p.Address)
            .IsRequired(false)
            .HasConversion(x => x.ToJson() , y => y.FromJsonToType<Address>());

        builder.Property(p => p.LoginInfo)
           .IsRequired()
           .HasConversion(x => x.ToJson() , y => y.FromJsonToType<LoginInfo>().ThrowIfNull(nameof(LoginInfo)));

        builder.Property(x => x.SystemLock)
            .HasConversion(x => x.ToJson() , y => y.FromJsonToType<LockInfo?>());

        builder.Property(x => x.OwnerLock)
            .HasConversion(x => x.ToJson() , y => y.FromJsonToType<LockInfo?>());
    }
}