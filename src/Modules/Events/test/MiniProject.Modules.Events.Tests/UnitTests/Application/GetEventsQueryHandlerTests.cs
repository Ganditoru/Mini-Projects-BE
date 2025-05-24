
using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Dapper;
using FluentAssertions;
using Microsoft.Data.Sqlite;
using MiniProject.Modules.Events.Application.Abstractions;
using MiniProject.Modules.Events.Application.Events.GetEvents;
using MiniProject.Modules.Events.Domain.Abstractions;

namespace MiniProject.Modules.Events.Tests.UnitTests.Application;
public sealed class GetEventsQueryHandlerTests
{

    [Fact]
    public async Task Handle_ReturnsEvents_WhenEventsExist()
    {
        // Arrange
        var expected = new EventResponse(
            Guid.NewGuid(),
            Guid.NewGuid(),
            "Conference",
            "Annual conf",
            "Berlin",
            new DateTime(2025, 7, 10, 9, 0, 0, DateTimeKind.Utc),
            new DateTime(2025, 7, 10, 17, 0, 0, DateTimeKind.Utc)
        );
        var fakeConn = new FakeDbConnection(new[] { expected });
        var factory = new TestDbConnectionFactory(fakeConn);
        var handler = new GetEventsQueryHandler(factory);

        // Act
        Result<IReadOnlyCollection<EventResponse>> result = await handler.Handle(new GetEventsQuery(), CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value
              .Should().BeAssignableTo<IReadOnlyCollection<EventResponse>>()
              .Which.Should().ContainSingle()
              .Which.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task Handle_ReturnsEmptyList_WhenNoEventsExist()
    {
        // Arrange
        var fakeConn = new FakeDbConnection(Array.Empty<EventResponse>());
        var factory = new TestDbConnectionFactory(fakeConn);
        var handler = new GetEventsQueryHandler(factory);

        // Act
        Result<IReadOnlyCollection<EventResponse>> result = await handler.Handle(new GetEventsQuery(), CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value
              .Should().BeAssignableTo<IReadOnlyCollection<EventResponse>>()
              .Which.Should().BeEmpty();
    }

}


internal sealed class FakeDbConnection : DbConnection
{
    private readonly IEnumerable<EventResponse> _responses;
    public FakeDbConnection(IEnumerable<EventResponse> responses)
        => _responses = responses;

    // Must match Dapper’s extension method signature exactly:
    public Task<IEnumerable<T>> QueryAsync<T>(
        string sql,
        object? param = null,
        IDbTransaction? transaction = null,
        int? commandTimeout = null,
        CommandType? commandType = null)
    {
        _ = sql;
        _ = param;
        _ = transaction;
        _ = commandTimeout;
        _ = commandType;

        if (typeof(T) == typeof(EventResponse))
        {
            return Task.FromResult(_responses.Cast<T>());
        }

        return Task.FromResult(Enumerable.Empty<T>());
    }

    #region Boilerplate (match DbConnection exactly)
    [AllowNull]
    public override string ConnectionString { get; set; } = "";
    public override string Database => "";
    public override string DataSource => "";
    public override string ServerVersion => "";
    public override ConnectionState State => ConnectionState.Open;

    public override void ChangeDatabase(string databaseName) { }
    public override void Open() { }
    public override void Close() { }

    protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
        => throw new NotSupportedException();
    protected override DbCommand CreateDbCommand()
        => throw new NotSupportedException();
    #endregion
}
// 2) Factory that returns our fake connection
internal sealed class TestDbConnectionFactory : IDbConnectionFactory
{
    private readonly DbConnection _conn;
    public TestDbConnectionFactory(DbConnection conn) => _conn = conn;
    public ValueTask<DbConnection> OpenConnectionAsync()
        => new ValueTask<DbConnection>(_conn);
}