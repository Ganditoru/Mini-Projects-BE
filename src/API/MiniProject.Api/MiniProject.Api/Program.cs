using MiniProject.Api.Extensions;
using MiniProject.Modules.Authentification.Infrastructure;
using MiniProject.Modules.Events.Infrastructure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentificationModule(builder.Configuration);
builder.Services.AddEventsModule(builder.Configuration);

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.ApplyMigrations();
}

AuthentificationModule.MapEndpoints(app);
EventsModule.MapEndpoints(app);

app.Run();
