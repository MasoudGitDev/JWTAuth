using Apps.Services.Models;
using Apps.Services.Services;
using System.ComponentModel.Design;
namespace Apps.Services.Implementations.DevicesInfo;
//internal sealed class DeviceInfo(IDetectionService _detectionService) : IDeviceInfo {

//    public DeviceModel Info {
//        get {
//            var type = _detectionService.Device.Type.ToString();
//            var platformName = _detectionService.Platform.Name.ToString();
//            var platformVersion = _detectionService.Platform.Version.ToString();
//            var engineName = _detectionService.Engine.Name.ToString();
//            var browserName = _detectionService.Browser.Name.ToString();
//            var browserVersion = _detectionService.Browser.Version.ToString();
//            var crawlerName = _detectionService.Crawler.Name.ToString();
//            var crawlerVersion = _detectionService.Crawler.Version.ToString();
//            var isCrawler = _detectionService.Crawler.IsCrawler;
//            var userAgentLength = _detectionService.UserAgent.Length();
//            var userAgent = _detectionService.UserAgent.ToString();

//            return new(
//                type ,
//                platformName , platformVersion ,
//                engineName ,
//                browserName , browserVersion ,
//                isCrawler , crawlerName , crawlerVersion ,
//                userAgent , userAgentLength);
//        }
//    }
//}

internal sealed class DeviceInfo : IDeviceInfo {
    public DeviceModel Info => throw new NotImplementedException();
}