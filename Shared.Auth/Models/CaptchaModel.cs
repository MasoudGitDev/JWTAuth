namespace Shared.Auth.Models;
public class CaptchaModel {
    public CaptchaStatus Status { get; set; } = CaptchaStatus.None;
    public string FileName { get; set; } = String.Empty;
    /// <summary>
    /// The Value of 'Image' must be in base64string!
    /// </summary>
    public string Image { get; set; } = String.Empty;
    public string UserInput { get; set; } = String.Empty;

    public static CaptchaModel Empty => new();
}

public enum CaptchaStatus {
    None = 0 , Failed = 1 , Succeed = 2
}
