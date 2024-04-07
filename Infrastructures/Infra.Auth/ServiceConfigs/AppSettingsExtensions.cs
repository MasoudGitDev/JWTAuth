using Microsoft.Extensions.Configuration;
using Shared.Auth.Extensions;
using Shared.Auth.Models;
using System.Runtime.CompilerServices;

namespace Infra.Auth.ServiceConfigs;
internal static class AppSettingsExtensions {    

    public static string GetAuthDbConnectionString(this IConfiguration configuration) {
        return configuration.GetConnectionString("AuthDb")
            .ThrowIfNullOrWhiteSpace("The <AuthDb> connection string can not be <NullOrWhiteSpace>.");
    }

    public static AuthTokenSettingsModel GetAuthTokenSettingsModel(this IConfiguration configuration) {
        return configuration.GetSection("AuthTokenSettingsModel").Get<AuthTokenSettingsModel>()
            .ThrowIfNull(nameof(AuthTokenSettingsModel));
    }

}
