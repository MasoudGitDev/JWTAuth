namespace Apps.Services.Models;
public record DeviceModel(
    string Type ,
    string PlatformName ,
    string PlatformVersion ,
    string EngineName ,
    string BrowserName ,
    string BrowserVersion ,
    bool IsCrawler ,
    string CrawlerName ,
    string CrawlerVersion ,
    string UserAgent ,
    int UserAgentLength
);
