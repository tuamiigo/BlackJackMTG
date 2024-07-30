using BlackJackMTG.Components;
using BlackJackMTG.Interfaces;
using BlackJackMTG.Models;
using BlackJackMTG.Services;
using BlackJackMTG.Controllers;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(3600); // Set the session timeout period here
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

//DB
builder.Services.AddDbContext<PostgresContext>(options =>
    options.UseNpgsql(builder.Configuration.GetValue<string>("Database:ConnectionString")));

builder.Services.AddHttpContextAccessor();



builder.Services.AddScoped<CardService>();
builder.Services.AddScoped<CurrencyService>(); 
builder.Services.AddScoped<CollectionService>();
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<GameController>();
builder.Services.AddScoped<StoreController>();
builder.Services.AddScoped<CollectionController>();
builder.Services.AddScoped<CardDetailsController>();
builder.Services.AddScoped<HomeController>();
var app = builder.Build();

app.UseSession();

app.Use(async (context, next) =>
{
    var session = context.Session;
    session.SetString("key", "value");

    await next.Invoke();
});



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
