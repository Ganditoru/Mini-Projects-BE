using MediatR;
using MiniProject.Modules.Authentification.Application.Users.Models;

namespace MiniProject.Modules.Authentification.Application.Users.GetUsers;
public sealed class GetUsersQuery(): IRequest<IReadOnlyCollection<UserResponse>>;
