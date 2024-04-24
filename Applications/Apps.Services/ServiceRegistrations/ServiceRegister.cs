using Apps.Services.Implementations;
using Apps.Services.Implementations.MsgSenders.Emails;
using Apps.Services.Services;
using Apps.Services.Services.Security;
using Microsoft.Extensions.DependencyInjection;

namespace Apps.Services.ServiceRegistrations;
public static class ServiceRegister {

    public static IServiceCollection AddAppsServices(this IServiceCollection services) {
        services.AddScoped<IMessageSender , FakeEmailSender>();
        services.AddScoped<ICaptcha , CustomCaptcha>();
        return services;
    }
}
