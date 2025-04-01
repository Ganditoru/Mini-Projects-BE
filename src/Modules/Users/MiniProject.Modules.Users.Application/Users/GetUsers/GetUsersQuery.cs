using MediatR;

namespace MiniProject.Modules.Users.Application.Users.GetUsers;
public sealed class GetUsersQuery(): IRequest<IReadOnlyCollection<UserResponse>>;
