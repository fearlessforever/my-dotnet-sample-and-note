using Fearlessforever.Api.Core.CacheProviders;
using Fearlessforever.Api.Core.ConfigsLoader;
using Fearlessforever.Api.Core.HandleException;
using Fearlessforever.Api.Core.Logging;
using Fearlessforever.Api.Core.Queue;
using Fearlessforever.Api.Core.RateLimiter;
using Fearlessforever.Api.Core.Routes;
using Fearlessforever.Api.Modules.Sample;
using Fearlessforever.Api.Modules.SampleQueue;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddCoreConfigsLoader(builder.Environment.EnvironmentName);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//========================================================
// Register Module Service & Configuration
builder.Services.AddModuleSampleServices();
builder.Services.AddModuleSampleQueueServices();
builder.Services.AddCoreRateLimiter();
builder.Services.AddCoreHandleException();
builder.Services.AddCoreCacheProviders(builder.Configuration);
builder.Services.AddCoreQueueService(builder.Configuration);
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
app.UseCoreConfigsLogHelper();
app.UseCoreRateLimiter();
app.UseCoreHandleException();
app.UseCoreRoutesMinimalApi();
app.UseCoreQueueDashboard();
//=========================================================

app.Run();
