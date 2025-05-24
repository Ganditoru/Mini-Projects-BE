using FluentAssertions;
using MiniProject.Modules.Events.Domain.Abstractions;
using MiniProject.Modules.Events.Domain.Categories;
using MiniProject.Modules.Events.Domain.Events;

namespace MiniProject.Modules.Events.Tests.UnitTests.Domain;

public sealed class EventTests
{
    // Create(...)
    [Fact]
    public void Create_ValidParameters_ReturnsSuccess()
    {
        var cat = Category.Create("Cat");
        DateTime start = DateTime.UtcNow.AddDays(1);
        DateTime end = start.AddHours(1);

        Result<Event> result = Event.Create(cat, "T", "D", "L", start, end);

        result.IsSuccess.Should().BeTrue();
        Event e = result.Value;
        e.Title.Should().Be("T");
        e.StartsAtUtc.Should().Be(start);
        e.EndsAtUtc.Should().Be(end);
        e.Status.Should().Be(EventStatus.Draft);
    }

    [Fact]
    public void Create_EndBeforeStart_ReturnsFailure()
    {
        var cat = Category.Create("Cat");
        DateTime start = DateTime.UtcNow.AddDays(1);
        DateTime end = start.AddDays(-1);

        Result<Event> result = Event.Create(cat, "T", "D", "L", start, end);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(EventErrors.EndDatePrecedesStartDate);
    }

    // Publish()
    [Fact]
    public void Publish_WhenDraft_SetsStatusToPublished()
    {
        var cat = Category.Create("Cat");
        Event evt = Event.Create(cat, "T", "D", "L", DateTime.UtcNow.AddDays(1), null).Value;

        Result result = evt.Publish();

        result.IsSuccess.Should().BeTrue();
        evt.Status.Should().Be(EventStatus.Published);
    }

    [Fact]
    public void Publish_WhenNotDraft_ReturnsFailure()
    {
        var cat = Category.Create("Cat");
        Event evt = Event.Create(cat, "T", "D", "L", DateTime.UtcNow.AddDays(1), null).Value;
        evt.Publish();

        Result result = evt.Publish();

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(EventErrors.NotDraft);
    }

    // Reschedule(...)
    [Fact]
    public void Reschedule_NewDates_UpdatesAndRaisesEvent()
    {
        var cat = Category.Create("Cat");
        DateTime oldStart = DateTime.UtcNow.AddDays(2);
        Event evt = Event.Create(cat, "T", "D", "L", oldStart, null).Value;
        evt.ClearDomainEvents();

        DateTime newStart = oldStart.AddDays(1);
        evt.Reschedule(newStart, null);

        evt.StartsAtUtc.Should().Be(newStart);
        evt.DomainEvents.Should().ContainSingle(e => e is EventRescheduledDomainEvent);
    }

    // Cancel(now)
    [Fact]
    public void Cancel_WhenBeforeStart_SetsStatusToCanceled()
    {
        var cat = Category.Create("Cat");
        DateTime now = DateTime.UtcNow;
        Event evt = Event.Create(cat, "T", "D", "L", now.AddDays(1), null).Value;

        Result result = evt.Cancel(now);

        result.IsSuccess.Should().BeTrue();
        evt.Status.Should().Be(EventStatus.Canceled);
    }

    [Fact]
    public void Cancel_WhenAlreadyCanceled_ReturnsFailure()
    {
        var cat = Category.Create("Cat");
        DateTime now = DateTime.UtcNow;
        Event evt = Event.Create(cat, "T", "D", "L", now.AddDays(1), null).Value;
        evt.Cancel(now);

        Result result = evt.Cancel(now);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(EventErrors.AlreadyCanceled);
    }

    [Fact]
    public void Cancel_WhenAlreadyStarted_ReturnsFailure()
    {
        var cat = Category.Create("Cat");
        DateTime now = DateTime.UtcNow;
        Event evt = Event.Create(cat, "T", "D", "L", now.AddHours(-1), null).Value;

        Result result = evt.Cancel(now);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(EventErrors.AlreadyStarted);
    }
}

