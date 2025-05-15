
using MediatR;
using MiniProject.Common.Messaging.Contracts.IntegrationEvents;
using MiniProject.Modules.Users.Application.Abstraction;
using MiniProject.Modules.Users.Domain.Outbox;
using MiniProject.Modules.Users.Domain.Users;
using Newtonsoft.Json;

namespace MiniProject.Modules.Users.Application.Users.RegisterUser;
internal sealed class RegisterUserCommandHandler(IUserRepository userRepository, IOutboxMessageRepository outboxMessageRepository, IUnitOfWork unitOfWork) : IRequestHandler<RegisterUserCommand, Guid>
{

    public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = User.Create(request.Name, request.Email, request.UserName, request.Password);

        var userRegisteredIntegrationEvent = new UserRegisteredIntegrationEvent(
               user.Id,
               DateTime.UtcNow,
               user.Id,
               user.Email,
               user.Name);

        var outboxMessage = new OutboxMessage()
        {
            Id = Guid.NewGuid(),
            Type = userRegisteredIntegrationEvent.GetType().Name,
            Content = JsonConvert.SerializeObject(userRegisteredIntegrationEvent, SerializerSettings.Instance),
            OccurredOnUtc = DateTime.UtcNow
        };

        userRepository.Insert(user);
        outboxMessageRepository.Insert(outboxMessage);


        await unitOfWork.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}
