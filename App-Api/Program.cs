using Fearlessforever.Api.Core.Logging;
using Fearlessforever.Api.Core.RateLimiter;
using Fearlessforever.Api.Modules.Sample;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//========================================================
// Register Module Service & Configuration
builder.Services.AddModuleSampleServices();
builder.Services.AddCoreRateLimiter();
builder.Host.AddCoreLogging();
//========================================================


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.UseAuthorization();

app.MapControllers();
//=========================================================
app.UseCoreRateLimiter();
//=========================================================

app.Run();
