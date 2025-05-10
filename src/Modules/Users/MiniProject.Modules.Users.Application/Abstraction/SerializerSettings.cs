using Newtonsoft.Json;

namespace MiniProject.Modules.Users.Application.Abstraction;
public static class SerializerSettings
{
    public static readonly JsonSerializerSettings Instance = new()
    {
        TypeNameHandling = TypeNameHandling.All,
        MetadataPropertyHandling = MetadataPropertyHandling.ReadAhead
    };
}

