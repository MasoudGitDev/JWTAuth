namespace Apps.Services.Models;
public class MessageModel {
    public IEnumerable<string> To { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
}
