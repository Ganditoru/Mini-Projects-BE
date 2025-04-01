using System.Runtime.InteropServices;
using MediatR;
using MiniProject.Modules.Users.Application.Abstraction;
using MiniProject.Modules.Users.Domain.Notifications;
using MiniProject.Modules.Users.Domain.Users;

namespace MiniProject.Modules.Users.Application.Notifications.AddNotificationToUserCommand;

internal sealed class AddNotificationToUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork) : IRequestHandler<AddNotificationToUserCommand, Guid>
{
    public async Task<Guid> Handle(AddNotificationToUserCommand request, CancellationToken cancellationToken)
    {
        // Get user by id
        User? user = userRepository.FindById(request.UserId);

        if (user == null)
        {
            return await Task.FromResult(Guid.Empty);
        }

        var notification = Notification.Create(
            request.Title,
            request.Description
        );

        user.AddNotification(notification);
        userRepository.Update(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return notification.Id;
    }
}
