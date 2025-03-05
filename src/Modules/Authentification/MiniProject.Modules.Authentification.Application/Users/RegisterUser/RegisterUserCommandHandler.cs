
using MediatR;
using MiniProject.Modules.Authentification.Application.Abstraction;
using MiniProject.Modules.Authentification.Domain.Users;

namespace MiniProject.Modules.Authentification.Application.Users.RegisterUser;
internal sealed class RegisterUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork) : IRequestHandler<RegisterUserCommand, Guid>
{

    public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User(request.Name, request.Email, request.UserName, request.Password);

        userRepository.Insert(user);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}
