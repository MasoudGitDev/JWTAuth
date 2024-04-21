namespace Shared.Auth.DTOs;
public class CaptchaValidationDto {

    public static CaptchaValidationDto New(string fileName , string userInput)
        => new(){ UserInput = userInput , FileName = fileName };

    public  string FileName { get; set; }
    public  string UserInput { get; set; } 
}
