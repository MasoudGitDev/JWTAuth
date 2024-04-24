using Apps.Services.Enums;
using Apps.Services.Models;

namespace Apps.Services.Services;
public interface IMessageSender
{
    public SenderType SenderType { get; }
    public LengthType LengthType { get; }
    Task SendAsync(MessageModel message);
}