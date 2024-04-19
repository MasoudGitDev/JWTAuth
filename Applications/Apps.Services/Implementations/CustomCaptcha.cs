using Apps.Services.Services;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;


namespace Apps.Services.Implementations;
internal class CustomCaptcha : ICaptcha {
    public async Task<(byte[] Image , string ImageText)> GenerateAsync() 
        => await GenerateCaptchaImageAsync(TextModel.Default(GenerateValidText(5)));

    private string GenerateValidText(byte length) {
        string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        var random = new Random();
        var charList = new char[length];
        for(int i = 0 ; i < charList.Length ; i++) {
            charList[i] = validChars[random.Next(validChars.Length)];
        }
        return new string(charList);
    }


    public async Task<(byte[] Image, string imageText)> GenerateCaptchaImageAsync(TextModel model) {
        var (content, width, height, fontName, fontSize) = model; 
        var image =  (new Image<Rgba32>(model.Width , model.Height ));
        image.Mutate(x=> x.DrawText(
            content,
            SystemFonts.CreateFont(fontName,  fontSize),
            Color.Black , 
            new PointF(10,10)
        ));
        using var stream = new MemoryStream();
        image.SaveAsPng(stream);
        return await Task.FromResult((stream.ToArray(), model.Content));
    }

    public record TextModel(
        string Content ,
        int Width = 250 ,
        int Height = 50 ,
        string FontName = "Arial" ,
        int FontSize = 24) {

        public static TextModel Default(string content) => new(content , 250 , 50 , "Arial" , 24);
    }

}
