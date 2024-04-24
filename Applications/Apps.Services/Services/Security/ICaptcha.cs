namespace Apps.Services.Services.Security;
public interface ICaptcha
{
    Task<(byte[] Image, string ImageText)> GenerateAsync();
}
