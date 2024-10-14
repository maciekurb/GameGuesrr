using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using GameGuessr.Api.Application.Games.Services.YouTube;
using GameGuessr.Api.Domain;
using GameGuessr.Tests;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace GameGuessr.Api.Application.Tests.Games.Services;

public class YouTubeOstRawProvider_Tests : InMemoryTestBase, IAsyncLifetime
{
    private readonly YouTubeOstRawProvider _provider;
    private readonly Game _game;

    public YouTubeOstRawProvider_Tests()
    {
        _game = new NonPublicFaker<Game>()
           .UseNonPublicFakerConstructor()
           .RuleFor(g => g.Id, f => f.Random.Guid())
           .RuleFor(g => g.Name, f => "Gothic")
           .Generate();

        var httpClient = new HttpClient();

        _provider = new YouTubeOstRawProvider(new Mock<IHttpClientFactory>().Object, new Mock<ILogger<YouTubeOstRawProvider>>().Object);
    }
    
    public async Task InitializeAsync()
    {
    }

    public Task DisposeAsync() => Task.CompletedTask;

    [Fact]
    public async Task Returns_SingleGameQuestion()
    {
        //Act
        var result = await Act();
        
        //Assert
        result.OstYouTubeKey.Should().NotBeNull();
        result.OstYouTubeDuration.Should().Be(0);
    }
    
    private Task<Game> Act() =>
        _provider.Execute(_game, CancellationToken.None);
}


