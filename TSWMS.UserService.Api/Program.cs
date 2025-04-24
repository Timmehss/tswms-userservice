#region Usings




#endregion

using Microsoft.EntityFrameworkCore;
using TSWMS.UserService.Api.MappingProfiles;
using TSWMS.UserService.Api.Middlewares;
using TSWMS.UserService.Configurations;
using TSWMS.UserService.Data;
using TSWMS.UserService.Shared.Helpers;
using TSWMS.UserService.Shared.Options;

var builder = WebApplication.CreateBuilder(args);

var developmentEnvironments = new string[] { "Development", "Production" };

// Get Environment
var environment = builder.Environment.EnvironmentName;

// Configure App Configuration
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true);

// Add Cors Policie
builder.Services.AddCors(o => o.AddPolicy("TSWMSPolicy", builder =>
{
    builder.SetIsOriginAllowed((host) => true)
           .AllowAnyMethod()
           .AllowAnyHeader()
           .AllowCredentials();
}));

// Configure AutoMapper Profiles
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<UserMappingProfile>();
});

// Configure EntityFramework UserDbContext
builder.Services.ConfigureUserDbContext(builder.Configuration);

// Configure dependency injection for managers
builder.Services.ConfigureManagers();

// Configure dependency injection for repositories
builder.Services.ConfigureRepositories();

// Additional service registrations
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<AesEncryptionHelper>();

// Encryption key
var encryptionKey = Environment.GetEnvironmentVariable("EncryptionKey");

if (string.IsNullOrEmpty(encryptionKey))
{
    throw new InvalidOperationException("EncryptionKey is missing!");
}

// IV key
var encryptionIV = Environment.GetEnvironmentVariable("EncryptionIV");

if (string.IsNullOrEmpty(encryptionIV))
{
    throw new InvalidOperationException("EncryptionIV is missing!");
}

builder.Services.Configure<EncryptionOptions>(options =>
{
    options.Key = encryptionKey;
    options.IV = encryptionIV;
});

var app = builder.Build();

// Apply Database Migrations if it's not in "Test" environment
if (environment != "Test")
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var dbContext = services.GetRequiredService<UsersDbContext>();

        // Apply pending migrations or create the database if it doesn't exist
        dbContext.Database.Migrate();
    }
}

app.UseCors("TSWMSPolicy");

// Exception handling middleware
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();