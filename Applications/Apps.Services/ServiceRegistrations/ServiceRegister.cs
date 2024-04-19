using Apps.Services.Implementations;
using Apps.Services.MsgSenders.Email;
using Apps.Services.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Apps.Services.ServiceRegistrations;
public static class ServiceRegister {

    public static IServiceCollection AddAppsServices(this IServiceCollection services) {
        services.AddScoped<IMessageSender , FakeEmailSender>();
        services.AddScoped<ICaptcha , CustomCaptcha>();
        return services;
    }
}
