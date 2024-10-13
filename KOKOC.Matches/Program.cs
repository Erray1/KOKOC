using Erray.AssemblyScanning;
using KOKOC.Matches.Application;
using KOKOC.Matches.Domain.Repositories;
using KOKOC.Matches.Infrastructure;
using KOKOC.Matches.Persistence.Endpoints;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped(typeof(IRepository<,>), typeof(GenericRepository<,>));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();
builder.Services.AddAuthentication();
builder.Services.AddDbContext<MatchesDbContext>(cfg =>
{
    cfg.UseNpgsql(builder.Configuration.GetConnectionString("MatchesDB"));
});

builder.Services.ScanAndRegisterServices<AppAssemblyMark>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapEndpoints();

app.Run();
