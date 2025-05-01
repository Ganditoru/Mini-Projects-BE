using MassTransit;
using MiniProject.Api.Extensions;
using MiniProject.Modules.Attendances.Infrastructure;
using MiniProject.Modules.Events.Infrastructure;
using MiniProject.Modules.Users.Infrastructure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddUsersModule(builder.Configuration);
builder.Services.AddEventsModule(builder.Configuration);
builder.Services.AddAttendanceModule();


WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.ApplyMigrations();
}

UsersModule.MapEndpoints(app);
EventsModule.MapEndpoints(app);
AttendancesModule.MapEndpoints(app);

app.Run();
