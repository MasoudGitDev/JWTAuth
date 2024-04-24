using Apps.Services.Enums;
using Apps.Services.Models;
using Apps.Services.Services;
using Shared.Auth.Extensions;

namespace Apps.Services.Implementations.MsgSenders.Emails;

internal class FakeEmailSender(EmailConfigModel _config) : IMessageSender {
    public SenderType SenderType => SenderType.Email;

    public LengthType LengthType => LengthType.Short;

    public async Task SendAsync(MessageModel message) {
        Console.WriteLine("Fake email was sent :\n" + message.ToJson());
        await Task.CompletedTask;
    }
}
