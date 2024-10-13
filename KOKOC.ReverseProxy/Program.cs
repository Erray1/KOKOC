using KOKOC.ReverseProxy.Application;
using KOKOC.ReverseProxy.Infrastructure;
using KOKOC.ReverseProxy.Infrastructure.RolesSeeding;
using KOKOC.ReverseProxy.Persistence.Endpoints;
using KOKOC.ReverseProxy.Persistence.ServicesRegistration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text;
using Erray.AssemblyScanning;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));
builder.Services.AddControllers();
builder.Services.AddDbContext<AppIdentityDbContext>(cfg =>
{
    cfg.UseNpgsql(builder.Configuration.GetConnectionString("IdentityDB"), options =>
    {

    });
});
builder.Services.AddSeedingServices();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddCookie(cfg =>
    {
        cfg.LoginPath = "/login";
        cfg.LogoutPath = "/logout";
    })
    .AddVkontakte(cfg =>
    {
        cfg.ClientId = builder.Configuration.GetSection("Authentication:VK:ClientId").Value!;
        cfg.ClientSecret = builder.Configuration.GetSection("Authentication:VK:ClientSecret").Value!;
        cfg.Scope.Add("email");
        cfg.SaveTokens = true;
        cfg.ClaimActions.MapJsonKey(ClaimTypes.Email, "email");
    })
    .AddJwtBearer(cfg =>
    {
        cfg.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration.GetSection("Autnentication:Inner:JWTIssuer").Value!,
            ValidAudience = builder.Configuration.GetSection("Autnentication:Inner:JWTAudience").Value!,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Inner:JWTKey").Value!))
        };
    }); 
builder.Services.AddAuthorization(cfg =>
{
    
});

builder.Services.AddSwaggerGen(opt =>
{
    opt.AddSecurityDefinition("Bearer", new()
    {
        In = ParameterLocation.Header,
        Description = "Введите токен",
        Name = "Авторизация",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.RegisterIdentityServices();
builder.Services.ScanAndRegisterServices<AppAssemblyMark>(null);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapReverseProxy();

app.MapSwagger();
app.UseSwaggerUI();

app.MapTelegramLogin();
app.MapControllers();
app.Run();

