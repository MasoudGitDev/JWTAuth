using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infra.Auth.Contexts.Write;

internal class AppWriteDbContextFactory : IDesignTimeDbContextFactory<AppWriteDbContext> { 

    public AppWriteDbContext CreateDbContext(string[] args) {

        var optionBuilder = new DbContextOptionsBuilder<AppWriteDbContext>();
        optionBuilder.UseSqlServer(DbSettings.connectionString);
        return new AppWriteDbContext(optionBuilder.Options);

    }
}
