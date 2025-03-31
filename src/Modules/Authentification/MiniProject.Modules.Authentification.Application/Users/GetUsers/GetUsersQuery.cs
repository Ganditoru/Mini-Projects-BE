using MediatR;

namespace MiniProject.Modules.Authentification.Application.Users.GetUsers;
public sealed class GetUsersQuery(): IRequest<IReadOnlyCollection<UserResponse>>;
