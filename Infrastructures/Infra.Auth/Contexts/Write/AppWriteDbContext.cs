using Domains.Auth.AppRoleEntity;
using Domains.Auth.AppUserEntity.Aggregate;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infra.Auth.Contexts.Write;
internal class AppWriteDbContext : IdentityDbContext<AppUser , AppRole , Guid> {
    public AppWriteDbContext(DbContextOptions<AppWriteDbContext> options) : base(options) {

    }

    protected override void OnModelCreating(ModelBuilder builder) {

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly() , ContextType);
        base.OnModelCreating(builder);



    }

    //public DbSet<MaleAppUser> MaleAppUsers { get; set; }
    //public DbSet<FemaleAppUser> FemaleAppUsers { get; set; }


    private static bool ContextType(Type type)
        => type.FullName?.Contains("Configs.Write") ?? false;
}
