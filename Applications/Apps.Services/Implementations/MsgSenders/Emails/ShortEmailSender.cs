using Apps.Services.Enums;
using Apps.Services.Exceptions;
using Apps.Services.Extensions;
using Apps.Services.Models;
using Apps.Services.Services;

namespace Apps.Services.Implementations.MsgSenders.Emails;
internal class ShortEmailSender(EmailConfigModel _config) : IMessageSender {
    public SenderType SenderType => SenderType.Email;

    public LengthType LengthType => LengthType.Short;

    public async Task SendAsync(MessageModel message) {
        if(message.Body.Length >= 50) {
            throw new MsgSenderException("The length of the message must be lower than or equal to 50.");
        }
        await new EmailHelper(_config)
            .SetupAsync(message.AsMimeMessage());
    }
}
