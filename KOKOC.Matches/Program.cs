using Erray.AssemblyScanning;
using KOKOC.Matches.Application;
using KOKOC.Matches.Domain.Repositories;
using KOKOC.Matches.Persistence.Endpoints;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped(typeof(IRepository<,>), typeof(GenericRepository<,>));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();
builder.Services.AddAuthentication();

builder.Services.ScanAndRegisterServices<AppAssemblyMark>(null);

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
