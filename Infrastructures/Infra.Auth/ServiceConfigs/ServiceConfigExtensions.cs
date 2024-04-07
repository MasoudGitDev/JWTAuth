using Apps.Auth.Abstractions;
using Apps.Auth.Accounts.Manager;
using Apps.Auth.Services;
using Apps.Services.ServiceRegistrations;
using Domains.Auth.AppRoleEntity;
using Domains.Auth.AppUserEntity.Aggregate;
using Domains.Auth.AppUserEntity.Repos;
using Infra.Auth.Contexts;
using Infra.Auth.Contexts.Write;
using Infra.Auth.Implements.Accounts;
using Infra.Auth.Implements.Managers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Shared.Auth.Enums;
using Shared.Auth.Extensions;
using Shared.Auth.Models;
using System.Text;

namespace Infra.Auth.ServiceConfigs;
public static class ServiceConfigExtensions {
    public static IServiceCollection Add_InfraAuth_Services(this IServiceCollection services) {

        var configuration = services
            .BuildServiceProvider().CreateScope()
            .ServiceProvider.GetRequiredService<IConfiguration>();

        var model = GetTokenSettingValue(configuration);
        services.AddScoped(_ => new AuthTokenSettingsModel(
            model.secretKey , model.issuer , model.audience , model.expire));

        services.AddAppsServices();
        services.AddScoped<IAuthService , DefaultJwtService>();
        services.AddScoped<IAppUserQueries , AppUserQueries>();
        services.AddScoped<IPasswordManager , PasswordManager>();
        services.AddScoped<IEmailManager , EmailManager>();
        services.AddScoped<IAccountManager , AccountManager>();
        services.AddScoped<IAccountUOW , AccountUOW>();
        services.AddScoped<IClaimsGenerator , ClaimsGenerator>();

        services.AddDbContext<AppWriteDbContext>(opt => {
            opt.UseSqlServer(DbSettings.connectionString);
        });

        services.AddIdentity<AppUser , AppRole>(opt => {
            opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            opt.User.RequireUniqueEmail = true;
            opt.Password.RequiredLength = 6;
            opt.Password.RequireLowercase = true;
            opt.Password.RequireUppercase = true;
            opt.Password.RequireDigit = true;
            opt.Password.RequiredUniqueChars = 1;
            opt.SignIn.RequireConfirmedEmail = true;
        })
            .AddEntityFrameworkStores<AppWriteDbContext>()
            .AddDefaultTokenProviders();



        string bearer = "Bearer";
        services.AddAuthentication(opt => {
            opt.DefaultScheme = bearer;
            opt.DefaultChallengeScheme = bearer;
            opt.DefaultAuthenticateScheme = bearer;
        }).AddJwtBearer(bearer , opt => {
            var model = configuration.GetSection("AuthTokenSettingsModel");
            var key = Encoding.UTF8.GetBytes(model["SecretKey"]?? "");
            opt.TokenValidationParameters = new TokenValidationParameters {
                ValidateIssuer = true ,
                ValidateAudience = true ,
                ValidateLifetime = true ,
                ValidateIssuerSigningKey = true ,
                RequireExpirationTime = true ,
                LogValidationExceptions = true ,
                ValidIssuer = model["Issuer"] ,
                ValidAudience = model["Audience"] ,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };

            opt.Events = new JwtBearerEvents {
                OnTokenValidated = async (ctx) => {
                    var claims =  ctx.Principal?.Claims;
                    var userId = (claims?.Where(x=>x.Type == AuthTokenType.UserId)
                    .FirstOrDefault()?.Value)
                    .ThrowIfNullOrWhiteSpace("UserId");
                    var signInManager = ctx.HttpContext.RequestServices.CreateScope()
                        .ServiceProvider
                        .GetRequiredService<SignInManager<AppUser>>();
                    var user = (await signInManager.UserManager.FindByIdAsync(userId))
                        .ThrowIfNull("Invalid User");

                    ctx.Principal = await signInManager.CreateUserPrincipalAsync(user);               
                } ,
                OnAuthenticationFailed = async (ctx) => {
                    Console.WriteLine(ctx.Exception.Message);
                    await Task.CompletedTask;
                } ,
                OnForbidden = async (ctx) => {
                    Console.WriteLine("Forbidden");
                    await Task.CompletedTask;
                }
            };
        });

        


        return services;
    }

    private static (
        string secretKey,
        string issuer,
        string audience,
        double expire) GetTokenSettingValue(IConfiguration configuration) {
        var model =  configuration.GetAuthTokenSettingsModel();
        return (model.SecretKey, model.Issuer, model.Audience, model.ExpireMinutes);
    }

}
