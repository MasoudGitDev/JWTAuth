using Domains.Auth.AppRoleEntity;
using Domains.Auth.AppUserEntity.Aggregate;
using Infra.Auth.Contexts.Write;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Auth.ServiceConfigs;
public static class ServiceConfigExtensions {

    public static IServiceCollection Add_InfraAuth_Services(this IServiceCollection services) {

        var configuration = services
            .BuildServiceProvider().CreateScope()
            .ServiceProvider.GetRequiredService<IConfiguration>();

        services.AddTransient(_ => new AppWriteDbContextFactory(configuration.GetAuthDbConnectionString()));

        

        services.AddDbContext<AppWriteDbContext>(opt => {
         
        });

        services.AddIdentityCore<AppUser>()
            .AddRoles<AppRole>()
            .AddEntityFrameworkStores<AppWriteDbContext>();


        return services;
    }

}
