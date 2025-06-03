using MediatR;
using MiniProject.Modules.Users.Application.Abstraction;
using MiniProject.Modules.Users.Domain.Users;

namespace MiniProject.Modules.Users.Application.Users.Login;
internal sealed class LgoinCommandHandler(IUserRepository userResponse, IJwtProvider jwtProvider) : IRequestHandler<LoginCommand, string?>
{
    public async Task<string?> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        // Get member
        User? user = await userResponse.GetByEmailAsync(request.Email, cancellationToken);

        if (user is null)
        {
            return "";
        }

        // Generate JWT
        string token = jwtProvider.Generate(user);

        // Return JWT
        return token;
    }
}
