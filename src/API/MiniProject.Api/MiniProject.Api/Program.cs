using MassTransit.RabbitMqTransport.Topology;
using MassTransit;
using MiniProject.Api.Extensions;
using MiniProject.Modules.Attendances.Infrastructure;
using MiniProject.Modules.Events.Infrastructure;
using MiniProject.Modules.Ticketing.Infrastructure;
using MiniProject.Modules.Users.Infrastructure;
using MiniProject.Modules.Ticketing.Presentation.Customers;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddUsersModule(builder.Configuration);
builder.Services.AddEventsModule(builder.Configuration);
builder.Services.AddTicketingModule(builder.Configuration);
builder.Services.AddAttendanceModule();

builder.Services.AddRabbitMqMessaging(builder.Configuration);

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
