﻿using Domains.Auth.AppUserEntity.Aggregate;
using Shared.Auth.Models;

namespace Apps.Auth.Accounts.Manager;
public interface IEmailManager {
    Task<AccountResult> ChangeAsync(AppUser appUser , string newEmail , string token);
    Task<AccountResult> RequestToChangeAsync(AppUser appUser , string newEmail , LinkModel model);
    Task<AccountResult> ConfirmAsync(AppUser appUser , string confirmationToken);
    Task<AccountResult> ResendConformationLink(AppUser appUser , LinkModel model);
}
