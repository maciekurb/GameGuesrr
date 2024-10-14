using GameGuessr.Api.Application.Services;
using GameGuessr.Api.Extensions;
using GameGuessr.Api.Infrastructure;
using GameGuessr.Api.Infrastructure.Settings;
using GameGuessr.Api.Middleware;
using Hangfire;
using Hangfire.Dashboard;
using Hangfire.PostgreSql;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile(@"appsettings.json", true, true)
    .AddJsonFile($"appsettings.{environmentName}.json", true, true)
    .AddEnvironmentVariables()
    .Build();

// Add services to the container.
builder.Services.AddHttpClient();
builder.Services.AddMediatR(AppDomain.CurrentDomain.Load("GameGuessr.Api.Application"));

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.EnableSensitiveDataLogging();
    options.UseNpgsql(builder.Configuration.GetConnectionString("GameGuessr"));
});
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddHangfire(configuration => 
{
    configuration.UsePostgreSqlStorage(builder.Configuration.GetConnectionString("GameGuessr"));
});
builder.Services.AddHangfireServer();

builder.Services.AddInjectables();

builder.Services.Configure<KeysSettings>(builder.Configuration.GetSection("Keys"));

var app = builder.Build();

var logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation(builder.Configuration.GetConnectionString("GameGuessr"));

app.ApplyMigrations();

app.UseBasicAuthenticationForRestrictedRoutes();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseHangfireDashboard(options: new DashboardOptions
{
    Authorization = new[] { new AllowAllFilter() },
    IgnoreAntiforgeryToken = true
});

app.UseHanfireService(app.Services.GetRequiredService<ILogger<HangfireService>>());

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");


app.Run();

