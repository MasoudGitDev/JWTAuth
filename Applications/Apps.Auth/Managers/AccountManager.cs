using Apps.Auth.Abstractions;
using Domains.Auth.AppUserEntity.Aggregate;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Shared.Auth.Abstractions;
using Shared.Auth.Models;

namespace Apps.Auth.Managers;
internal abstract partial class AccountManager<TModel, TReturn> : IRequestHandler<TModel , TReturn> 
    where TModel :IRequest<TReturn>
    where TReturn:IRequestResult {
    public abstract Task<TReturn> Handle(TModel request , CancellationToken cancellationToken);

    protected readonly SignInManager<AppUser> _signInManager;
    protected readonly UserManager<AppUser> _userManager;    
    protected readonly IAuthService _authService;

    public AccountManager(IServiceProvider serviceProvider)
    {
        _signInManager = serviceProvider.GetRequiredService<SignInManager<AppUser>>();
        _userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
        _authService = serviceProvider.GetRequiredService<IAuthService>();
    }

    protected Dictionary<string , string> EmptyClaims => new();
}
