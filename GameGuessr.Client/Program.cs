using GameGuessr.Client;
using GameGuessr.Client.Application;
using GameGuessr.Client.Infrastructure;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddSingleton<QuizParams>();
builder.Services.AddSingleton<RankingService>();
builder.Services.AddSingleton<LoaderService>();

await builder.Build().RunAsync();
