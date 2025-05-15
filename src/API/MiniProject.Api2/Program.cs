using Microsoft.AspNetCore.Server.Kestrel.Core;
using MiniProject.Api2.Extensions;
using MiniProject.Modules.Attendance.Infrastructure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5025, lo =>
    {
        lo.UseHttps();
        lo.Protocols = HttpProtocols.Http1;
    });
    options.ListenAnyIP(5026, lo =>
    {
        lo.Protocols = HttpProtocols.Http2;
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

AttendancesModule.MapEndpoints(app);

app.Run();

