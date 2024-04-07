using Apps.Services.Enums;

namespace Apps.Services.Models;
public record EmailConfigModel(    
    string From ,
    string userName ,
    string appPassword, 
    string Smtp = "smtp.gmail.com" ,
    SmtpPort Port = SmtpPort.P587_Default
    );
