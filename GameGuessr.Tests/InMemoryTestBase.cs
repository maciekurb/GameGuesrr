using System;
using Bogus;
using GameGuessr.Api.Infrastructure;
using GameGuessr.Api.Infrastructure.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;

namespace GameGuessr.Tests;

public abstract class InMemoryTestBase : IDisposable
{
    protected readonly Mock<IDateTimeProvider> DateTimeProviderMock = new();
    protected DateTime CurrentDateTime;
    protected AppDbContext DbContext;

    protected InMemoryTestBase()
    {
        ConfigureDbContext();
        ConfigureDateTimeProvider();
    }

    protected IDateTimeProvider DateTimeProvider => DateTimeProviderMock.Object;

    public void Dispose() => DbContext?.Dispose();

    private void ConfigureDbContext()
    {
        var dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
           .UseInMemoryDatabase(Guid.NewGuid().ToString())
           .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
           .EnableSensitiveDataLogging()
           .Options;

        DbContext = new AppDbContext(dbContextOptions, DateTimeProvider, new Mock<IMediator>().Object);
    }

    private void ConfigureDateTimeProvider()
    {
        CurrentDateTime = new Faker().Date.Past();

        DateTimeProviderMock.Setup(provider => provider.UtcNow)
           .Returns(CurrentDateTime);
    }
}