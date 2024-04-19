namespace Apps.Services.Services;
public interface ICaptcha {
    Task<(byte[] Image , string ImageText)> GenerateAsync();
}
