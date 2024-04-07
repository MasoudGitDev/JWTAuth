﻿using Domains.Auth.AppUserEntity.Aggregate;
using Shared.Auth.Enums;
using Shared.Auth.Models;

namespace Apps.Auth.Accounts.Manager;
public interface IAccountManager {
    Task<AccountResult> LoginByTokenAsync(string authToken);

    Task<AccountResult> SignInAsync(
        LoginType loginType ,string loginName , string password, bool isPersistent , bool lockoutOnFailure);

    Task<AccountResult> RegisterAsync(AppUser appUserModel , string password , LinkModel model);
}
