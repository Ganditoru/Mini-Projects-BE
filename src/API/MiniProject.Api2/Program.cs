using Microsoft.AspNetCore.Server.Kestrel.Core;
using MiniProject.Api2.Extensions;
using MiniProject.Modules.Attendances.Infrastructure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5025, lo =>
    {
        lo.UseHttps();
        lo.Protocols = HttpProtocols.Http1;
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAttendanceModule(builder.Configuration);

builder.Services.AddRabbitMqMessaging(builder.Configuration);

WebApplication app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.ApplyMigrations();
}

app.UseHttpsRedirection();

AttendancesModule.MapEndpoints(app);

app.Run();

