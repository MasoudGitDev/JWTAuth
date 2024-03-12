using MediatR;
using Shared.Auth.Enums;
using Shared.Auth.Models;

namespace Apps.Auth.Accounts.Commands.Models;
public class CreateAppUserModel :IRequest<AccountResult> {

    required public string Email { get; set; }
    required public string UserName { get; set; }
    required public string Password { get; set; }

    required public DateTime BirthDate { get; set; } = DateTime.UtcNow;
    required public Gender Gender { get; set; } = Gender.Male;
}
