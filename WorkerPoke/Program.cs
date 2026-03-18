using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);


builder.Services.AddSingleton<PokemonService>();
builder.Services.AddSingleton<PokemonRepository>();
builder.Services.AddSingleton<PokemonFacade>();


builder.Services.AddHangfire(config =>
    config.UseMemoryStorage());

builder.Services.AddHangfireServer();

var app = builder.Build();

app.Start();

//app.UseHangfireDashboard();


RecurringJob.AddOrUpdate<PokemonService>(
    "pokemon-job",
    x => x.RunJob(),
    Cron.MinuteInterval(1)
);

app.Run();