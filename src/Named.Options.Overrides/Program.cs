using Microsoft.Extensions.Options;
using Named.Options.Overrides.Config;

// Create
var builder = WebApplication.CreateBuilder(args);

// Add overrides file
builder.Configuration.AddJsonFile("appsettings.overrides.json");

// Configure
// The null named option is used to target all of the named instances instead of a specific named instance
// For further info, please refer https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options?view=aspnetcore-8.0
// The below setup can be moved into a generic extension as well
builder.Services.Configure<TestConfig>(null, builder.Configuration.GetSection("TestConfig"));
// Add named overrides as <Override2, Override3> and point them to specific sections in overrides config
builder.Services.Configure<TestConfig>("Override2", builder.Configuration.GetSection("TestConfig:Override2"));
builder.Services.Configure<TestConfig>("Override3", builder.Configuration.GetSection("TestConfig:Override3"));

// Build
var app = builder.Build();

// Extend
// Gets default value from config i.e appsettings.json
app.MapGet("/", (IOptionsMonitor<TestConfig> optionsMonitor) => optionsMonitor.CurrentValue);

// Gets specific version of value from config i.e appsettings.overrides.json
app.MapGet("/override2", (IOptionsMonitor<TestConfig> optionsMonitor) => optionsMonitor.Get("Override2"));
app.MapGet("/override3", (IOptionsMonitor<TestConfig> optionsMonitor) => optionsMonitor.Get("Override3"));

// Incorrect name, gets default value from config i.e appsettings.json
app.MapGet("/incorrect", (IOptionsMonitor<TestConfig> optionsMonitor) => optionsMonitor.Get("incorrect"));

// Run
app.Run();
