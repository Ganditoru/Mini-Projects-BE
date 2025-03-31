using System.Reflection;
using MiniProject.Modules.Authentification.Domain.Users;
using MiniProject.Modules.Authentification.Infrastructure;

namespace MiniProject.Modules.Authentification.ArchitectureTests.Abstractions;
internal abstract class BaseTest
{
    protected static readonly Assembly ApplicationAssembly = typeof(Authentification.Application.AssemblyReference).Assembly;

    protected static readonly Assembly DomainAssembly = typeof(User).Assembly;

    protected static readonly Assembly InfrastructureAssembly = typeof(AuthentificationModule).Assembly;

    protected static readonly Assembly PresentationAssembly = typeof(Authentification.Presentation.Users.UserEndpoints).Assembly;
}
