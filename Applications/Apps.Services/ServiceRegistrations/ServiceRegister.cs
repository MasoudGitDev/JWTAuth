using Apps.Services.Implementations.DevicesInfo;
using Apps.Services.MsgSenders.Email;
using Apps.Services.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Apps.Services.ServiceRegistrations;
public static class ServiceRegister {

    public static IServiceCollection AddAppsServices(this IServiceCollection services) {
        services.AddScoped<IMessageSender , FakeEmailSender>();
        //services.AddScoped<IDeviceInfo , DeviceInfo>();
        return services;
    }
}
