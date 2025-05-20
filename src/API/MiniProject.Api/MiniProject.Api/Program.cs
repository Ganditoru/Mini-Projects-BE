using MiniProject.Api.Extensions;
using MiniProject.Modules.Events.Infrastructure;
using MiniProject.Modules.Ticketing.Infrastructure;
using MiniProject.Modules.Users.Infrastructure;
using Microsoft.AspNetCore.Server.Kestrel.Core;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5021, lo =>
    {
        lo.UseHttps();                        
        lo.Protocols = HttpProtocols.Http1; 
    });
    options.ListenAnyIP(5022, lo =>
    {                           
        lo.Protocols = HttpProtocols.Http2;
    });
    options.ListenAnyIP(5023, lo =>
    {
        lo.Protocols = HttpProtocols.Http1;
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddUsersModule(builder.Configuration);
builder.Services.AddEventsModule(builder.Configuration);
builder.Services.AddTicketingModule(builder.Configuration);

builder.Services.AddRabbitMqMessaging(builder.Configuration);

WebApplication app = builder.Build();

app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI( c =>
    {
        c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
        c.DefaultModelsExpandDepth(-1);
    });

    app.ApplyMigrations();
}

UsersModule.MapEndpoints(app);
EventsModule.MapEndpoints(app);

app.Run();
