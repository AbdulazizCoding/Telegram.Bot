using fastfood_order.Data;
using fastfood_order.Services;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Polling;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BotDbContext>(
    options => options.UseSqlite(builder.Configuration.GetConnectionString("BotConnection")));

var token = builder.Configuration.GetValue("BotToken", string.Empty);

builder.Services.AddSingleton(new TelegramBotClient(token));
builder.Services.AddSingleton<IUpdateHandler, BotUpdateHandler>();
builder.Services.AddHostedService<BotBackgroundService>();

builder.Services.AddScoped<UserService>();

builder.Services.AddLocalization();

var app = builder.Build();

var supportedCultures = new[] { "uz-Uz", "en-Us", "ru-Ru" };
var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
    .SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);
app.UseRequestLocalization(localizationOptions);

app.Run();