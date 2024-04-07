using Apps.Services.Models;
using Infra.Auth.ServiceConfigs;
using Microsoft.OpenApi.Models;
using Shared.Auth.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var emailConfigModel =
    (builder.Configuration.GetSection("EmailConfigModel").Get<EmailConfigModel>())
    .ThrowIfNull(nameof(EmailConfigModel));

builder.Services.AddSingleton(emailConfigModel);
builder.Services.Add_InfraAuth_Services();


builder.Services.AddSwaggerGen(opt => {
    opt.SwaggerDoc("v1" , new OpenApiInfo() { Title = "Jwt Authentication" });
    var openApiSecurityScheme = new OpenApiSecurityScheme(){
        Scheme = "Bearer" ,
        BearerFormat = "JWT" ,
        Description = "Put your jwt token in this textbox below !" ,
        In = ParameterLocation.Header ,
        Name = "Jwt Authentication" ,
        Type = SecuritySchemeType.Http ,
        Reference = new OpenApiReference{
            Id = "Bearer" ,
            Type = ReferenceType.SecurityScheme
        }
    };
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement() {

        {openApiSecurityScheme , Array.Empty<string>() }
    });
    opt.AddSecurityDefinition("Bearer" , openApiSecurityScheme);

});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();





var app = builder.Build();
// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();


app.Run();
