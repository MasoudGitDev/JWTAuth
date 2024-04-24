using Apps.Services.Exceptions;
using Apps.Services.Models;
using MailKit.Net.Smtp;
using MimeKit;

namespace Apps.Services.Implementations.MsgSenders.Emails;
internal class EmailHelper(EmailConfigModel _config) {
    public async Task SetupAsync(MimeMessage message , bool useSSL = true) {
        using SmtpClient smtpClient = new();
        try {
            CancellationToken cancellation = new();
            await smtpClient.AuthenticateAsync(_config.userName , _config.appPassword , cancellation);
            smtpClient.AuthenticationMechanisms.Remove("XOAUTH2");
            await smtpClient.ConnectAsync(_config.From , (int) _config.Port , useSSL , cancellation);

            if(!smtpClient.IsConnected) {
                throw new SmtpException("The <SMTP> is not connected.");
            }

            if(!smtpClient.IsAuthenticated) {
                throw new SmtpException("The <SMTP> is not authenticated.");
            }

            await smtpClient.SendAsync(message , cancellation);

        }
        catch(Exception ex) {
            if(ex is SmtpException smtpEx) {
                Console.WriteLine(smtpEx.Code , smtpEx.Description);
            }
            else {
                Console.WriteLine(ex.Message);
            }
        }
        finally {
            await smtpClient.DisconnectAsync(true , new CancellationToken());
            smtpClient.Dispose();
        }
    }
}
