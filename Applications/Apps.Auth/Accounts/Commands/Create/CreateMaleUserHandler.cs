using Apps.Auth.Accounts.Commands.Models;
using Apps.Auth.Managers;
using Domains.Auth.AppUserEntity.Aggregate;
using Shared.Auth.Extensions;
using Shared.Auth.Models;

namespace Apps.Auth.Accounts.Commands.Create;
internal sealed class CreateMaleUserHandler : AccountManager<CreateAppUserModel , AccountResult> {

    public CreateMaleUserHandler(IServiceProvider serviceProvider) : base(serviceProvider) {

    }

    public override async Task<AccountResult> Handle(CreateAppUserModel request , CancellationToken cancellationToken) {
        await CheckUserNotExist(request.Email , request.UserName);
        return await CreateAsync(request);
    }

    private async Task CheckUserNotExist(string email , string userName) {
        ( await FindByEmailAsync(email) ).ThrowIfFound(nameof(email));
        ( await FindByUserName(userName) ).ThrowIfFound(nameof(userName));
    }

    // <warning> must be better exception
    // <warning> password must be a validate by rules
    private async Task<AccountResult> CreateAsync(CreateAppUserModel model) {
        var newUser = AppUser.Create(model.Gender , model.BirthDate);
        var result = await _userManager.CreateAsync(newUser);
        if(!result.Succeeded) {
            throw new Exception("System can not create the new user.");
        }
        result = await _userManager.AddPasswordAsync(newUser , model.Password);
        if(!result.Succeeded) {
            throw new Exception("System can not add the password correctly.");

        }
        return await _authService.GenerateTokenAsync(newUser.Id);
    }





}
