using Microsoft.Extensions.Configuration;
using Shared.Auth.Extensions;

namespace Infra.Auth.ServiceConfigs;
internal static class AppSettingsExtensions {

    public static string GetAuthDbConnectionString(this IConfiguration configuration) {
        return configuration.GetConnectionString("AuthDb")
            .ThrowIfNullOrWhiteSpace("The <AuthDb> connection string can not be <NullOrWhiteSpace>.");
    }

}
