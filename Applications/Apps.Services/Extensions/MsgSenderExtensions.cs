using Apps.Services.MsgSenders.Models;
using MimeKit;

namespace Apps.Services.Extensions;
internal static class MsgSenderExtensions {

    public static MimeMessage AsMimeMessage(
        this MessageModel message ,
        string mailBoxName = "mailBox"
        ) {
        var mimeMessage = new MimeMessage();
        mimeMessage.To.AddRange(message.To.Select(x=> new MailboxAddress(mailBoxName,x)));
        mimeMessage.Subject = message.Subject;
        mimeMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) {
            Text = message.Body
        };
        return mimeMessage;
    }

}
