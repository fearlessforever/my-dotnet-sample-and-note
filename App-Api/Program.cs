using System.Diagnostics;
using Fearlessforever.Api.Core.AppCancelToken;
using Fearlessforever.Api.Core.CacheProviders;
using Fearlessforever.Api.Core.ConfigsLoader;
using Fearlessforever.Api.Core.FluentValidation;
using Fearlessforever.Api.Core.HandleException;
using Fearlessforever.Api.Core.Logging;
using Fearlessforever.Api.Core.Queue;
using Fearlessforever.Api.Core.RateLimiter;
using Fearlessforever.Api.Core.Routes;
using Fearlessforever.Api.Core.SignalR;
using Fearlessforever.Api.Modules.Sample;
using Fearlessforever.Api.Modules.SampleQueue;
using Fearlessforever.Databases;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddCoreConfigsLoader(builder.Environment.EnvironmentName);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//========================================================
// Register Module Service & Configuration
builder.Host.AddCoreLogging();
builder.Services.AddCoreAppCancelTokenProvider();
builder.Services.AddModuleSampleServices();
builder.Services.AddModuleSampleQueueServices();
builder.Services.AddCoreRateLimiter();
builder.Services.AddCoreHandleException();
builder.Services.AddCoreCacheProviders(builder.Configuration);
builder.Services.AddCoreQueueService(builder.Configuration);
builder.Services.AddCoreSignalR(builder.Configuration);
builder.Services.AddCoreDatabaseServices(builder.Configuration);
builder.Services.AddCoreFluentValidation();
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
// Enable serving static files
app.UseStaticFiles("/assets"); 
app.MapControllers();
//=========================================================
app.UseCoreAppCancelToken();
app.UseCoreConfigsLogHelper();
await app.UseCoreDatabasesApplyMigrationsAndSeedersAsync(AppCancelTokenService.CancelTokenSource.Token);

app.UseCoreRateLimiter();
app.UseCoreHandleException();
app.UseCoreRoutesMinimalApi(app.Configuration);
app.UseCoreQueueDashboard();
//=========================================================

app.Run();
