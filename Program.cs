using Azure.Identity;
using Microsoft.Azure.Cosmos;
using wedding_site.Components;
using wedding_site.Data;
using wedding_site.Domain;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddSingleton<IRsvpRepo,RsvpRepo>();
builder.Services.AddSingleton(provider => {
    return new CosmosClient(
        Environment.GetEnvironmentVariable("COSMOS_CONNECTIONSTRING"));
        //Environment.GetEnvironmentVariable("COSMOS_ENDPOINT"),
        //new DefaultAzureCredential());
});

builder.Logging.AddConsole();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
