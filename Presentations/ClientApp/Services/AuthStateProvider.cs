using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Shared.Auth.DTOs;
using Shared.Auth.Enums;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace ClientApp.Services;

internal sealed class AuthStateProvider(
    HttpClient _httpClient ,
    ILocalStorageService _localStorage ,
    IAccountService _accountService)
    : AuthenticationStateProvider {

    private readonly ClaimsPrincipal _anonymous = new(new ClaimsIdentity());
    private const string _tokenKey = "JwtAuthApp_tokenKey";
    private readonly string _authenticationType = "Bearer";
    private readonly string _scheme = "Bearer";
    private readonly CancellationToken _cancelationToken = new();

    public override async Task<AuthenticationState> GetAuthenticationStateAsync() {
        try {
            var token =(await _localStorage.GetItemAsStringAsync(_tokenKey , _cancelationToken));
            return await CreateAuthState(( await _accountService.LoginByTokenAsync(token ?? "") ).AuthToken);
            // return await CreateAuthState(token);
        }
        catch(Exception ex) {
            Console.WriteLine(ex.Message);
            return new AuthenticationState(_anonymous);
        }
    }

    public async Task SetStateAsync(AccountResultDto? accountResult) {
        try {
            var state  =await CreateAuthState(accountResult?.AuthToken);
            NotifyAuthenticationStateChanged(Task.FromResult(state));
        }
        catch(Exception ex) {
            Console.WriteLine(ex.Message);
            await Task.CompletedTask;
        }
    }

    // ========================== private methods

    private async Task<AuthenticationState> CreateAuthState(string? token) {
        if(token is null) {
            await _localStorage.RemoveItemAsync(_tokenKey , _cancelationToken);
            CreateBearer();
            return new(_anonymous);
        }
        else {
            await _localStorage.SetItemAsStringAsync(_tokenKey , token , _cancelationToken);
            CreateBearer(token);

            return new(new ClaimsPrincipal(new ClaimsIdentity(GetClaims(token) , _authenticationType)));
        }
    }
    private void CreateBearer(string token = "<empty>")
       => _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(_scheme , token);

    private static List<Claim> GetClaims(string token) {
        var claims = new JwtSecurityTokenHandler().ReadJwtToken(token).Claims.ToList() ?? [];
        var displayName = claims.FirstOrDefault(x=>x.Type == AuthTokenType.DisplayName)?.Value ?? "<empty>";
        claims.Add(new(ClaimTypes.Name ,displayName ));
        return claims;
    }
}