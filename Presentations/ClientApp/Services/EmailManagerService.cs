using Shared.Auth.Constants.ApiAddresses;
using Shared.Auth.DTOs;

namespace ClientApp.Services;

public interface IEmailService {

    Task<AccountResultDto> ChangeEmailAsync(string email);
    Task<AccountResultDto> ConfirmAsync(EmailConfirmationDto model);
    Task<AccountResultDto> ResendCodeAsync();
}

public class EmailManagerService(HttpClient _httpClient)
    : HttpClientManager(_httpClient), IEmailService {
    public async Task<AccountResultDto> ChangeEmailAsync(string email) {
        return await PutAsync(ServerEmailAction.ChangeEmail , email);
    }

    public async Task<AccountResultDto> ConfirmAsync(EmailConfirmationDto model) {
        return await PutAsync(ServerEmailAction.Confirm , model);
    }

    public async Task<AccountResultDto> ResendCodeAsync() {
        return await PostAsync<string>(ServerEmailAction.ResendCode , null);
    }
}
