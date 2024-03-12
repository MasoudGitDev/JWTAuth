using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infra.Auth.Contexts.Write;
internal class AppWriteDbContextFactory(string connectionString) : IDesignTimeDbContextFactory<AppWriteDbContext> {
    public AppWriteDbContext CreateDbContext(string[] args) {

        var optionBuilder = new DbContextOptionsBuilder<AppWriteDbContext>();
        optionBuilder.UseSqlServer(connectionString);
        return new AppWriteDbContext(optionBuilder.Options);

    }
}
