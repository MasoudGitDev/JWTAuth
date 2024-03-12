﻿using Domains.Auth.AppUserEntity.Aggregate;
using Domains.Auth.AppUserEntity.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Auth.Extensions;

namespace Infra.Auth.Configs.Write;
internal class AppUsers_Write_Configs :
    IEntityTypeConfiguration<MaleAppUser> ,
    IEntityTypeConfiguration<FemaleAppUser> {

    
    public void Configure(EntityTypeBuilder<MaleAppUser> builder) {
        builder.HasIndex(x => x.Id).IsUnique();
        builder.Property(p => p.Id).IsRequired();

        builder.Property(p => p.Address)
            .IsRequired(false)
            .HasConversion(x => x.ToJson() , y => y.FromJsonToType<Address>());

        builder.Property(p => p.LoginInfo)
           .IsRequired()
           .HasConversion(x => x.ToJson() , y => y.FromJsonToType<LoginInfo>().ThrowIfNull(nameof(LoginInfo)));

    }

    public void Configure(EntityTypeBuilder<FemaleAppUser> builder) {
        builder.HasIndex(x => x.Id).IsUnique();
        builder.Property(p => p.Id).IsRequired();

        builder.Property(p => p.Address)
            .IsRequired(false)
            .HasConversion(x => x.ToJson() , y => y.FromJsonToType<Address>());

        builder.Property(p => p.LoginInfo)
           .IsRequired()
           .HasConversion(x => x.ToJson() , y => y.FromJsonToType<LoginInfo>().ThrowIfNull(nameof(LoginInfo)));
    }
}
