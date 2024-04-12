using Shared.Auth.Constants.ApiAddresses;
using Shared.Auth.DTOs;
using Shared.Auth.Extensions;

namespace ClientApp.Services;

public interface IAccountService {
    Task<AccountResultDto> RegisterAsync(SignUpDto model);
    Task<AccountResultDto> LoginAsync(LoginDto model);
    Task<AccountResultDto> LoginByTokenAsync(string token);
    Task<AccountResultDto> DeleteAsync();
}

internal sealed class AccountManagerService(HttpClient _httpClient)
    : HttpClientManager(_httpClient.ThrowIfNull("<HttpClient>")), IAccountService {
    public async Task<AccountResultDto> DeleteAsync() {
        return await DeleteAsync(AccountAction.Delete);
    }

    public async Task<AccountResultDto> LoginAsync(LoginDto model) {
        return await PostAsync(AccountAction.Login , model);
    }

    public async Task<AccountResultDto> LoginByTokenAsync(string token) {
        return await PostAsync<string>($"{AccountAction.LoginByToken}/{token}" , null);
    }

    public async Task<AccountResultDto> RegisterAsync(SignUpDto model) {
        return await PostAsync(AccountAction.Register , model);
    }
}

