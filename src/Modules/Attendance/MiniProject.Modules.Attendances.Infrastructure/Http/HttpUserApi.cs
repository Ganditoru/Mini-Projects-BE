using System.Net.Http.Json;
using MiniProject.Common.Messaging.Contracts.User;

namespace MiniProject.Modules.Attendances.Infrastructure.Http;
public class HttpUserApi(HttpClient http) : IHttpUsersApi
{
    public async Task<IEnumerable<CommonUserResponse>> GetUsersAsync(CancellationToken cancellationToken = default)
    {
        return await http.GetFromJsonAsync<IEnumerable<CommonUserResponse>>("/users", cancellationToken);
    }
}
