using Shared.Auth.Constants.ApiAddresses;
using Shared.Auth.DTOs;

namespace ClientApp.Services;

public interface IPasswordManagerService {
    Task<AccountResultDto> ChangeAsync(ChangePasswordDto model);
    Task<Result> ForgotAsync(string email);
    Task<AccountResultDto> ResetAsync(ResetPasswordDto model);
}

public class PasswordManagerService(HttpClient _httpClient) :
    HttpClientManager(_httpClient), IPasswordManagerService {
    public async Task<AccountResultDto> ChangeAsync(ChangePasswordDto model) {
        return await PutAsync(PasswordAction.Change , model);
    }
    public async Task<Result> ForgotAsync(string email) 
        => await PostRouteAsync<Result>(PasswordAction.Forgot , email);

    public async Task<AccountResultDto> ResetAsync(ResetPasswordDto model) {
        return await PutAsync(PasswordAction.Reset , model);
    }
}
