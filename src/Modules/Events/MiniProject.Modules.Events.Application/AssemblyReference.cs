using System.Reflection;
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("MiniProject.Modules.Events.Tests")]

namespace MiniProject.Modules.Events.Application;
public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
