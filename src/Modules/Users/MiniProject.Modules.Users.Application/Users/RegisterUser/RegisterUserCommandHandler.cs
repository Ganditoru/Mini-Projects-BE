
using MediatR;
using MiniProject.Common.Messaging.Contracts.Abstract.Messaging;
using MiniProject.Common.Messaging.Contracts.IntegrationEvents;
using MiniProject.Modules.Users.Application.Abstraction;
using MiniProject.Modules.Users.Domain.Users;

namespace MiniProject.Modules.Users.Application.Users.RegisterUser;
internal sealed class RegisterUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IEventBus bus) : IRequestHandler<RegisterUserCommand, Guid>
{

    public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = User.Create(request.Name, request.Email, request.UserName, request.Password);

        userRepository.Insert(user);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        await bus.PublishAsync(
            new UserRegisteredIntegrationEvent(
                user.Id,
                DateTime.UtcNow,
                user.Id,
                user.Email,
                user.Name),
            cancellationToken);

        return user.Id;
    }
}
