using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Bogus;
using CSharpFunctionalExtensions;
using FluentAssertions;
using GameGuessr.Api.Application.Questions.Queries;
using GameGuessr.Api.Application.Questions.Services;
using GameGuessr.Api.Domain;
using GameGuessr.Api.Domain.Screenshots;
using GameGuessr.Shared;
using GameGuessr.Tests;
using Moq;
using Xunit;

namespace GameGuessr.Api.Application.Tests.Questions.Queries;

public class GetSingleQuestionQuery_Tests : InMemoryTestBase, IAsyncLifetime
{
    private readonly GetSingleQuestionQueryHandler _handler;

    public GetSingleQuestionQuery_Tests()
    {
        var allGamesQuestionProvider = new Mock<IGamesQuestionProvider>();
        var topGamesQuestionProvider = new Mock<IGamesQuestionProvider>();
        var relatedGamesQuestionProvider = new Mock<IGamesQuestionProvider>();

        var fakeScreenshots = new NonPublicFaker<Screenshot>()
           .UseNonPublicFakerConstructor()
           .RuleFor(s => s.Id, f => f.Random.Guid())
           .RuleFor(s => s.Url, f => f.Image.PicsumUrl())
           .Generate(2);

        var fakeGamesList = new NonPublicFaker<Game>()
           .UseNonPublicFakerConstructor()
           .RuleFor(g => g.Id, f => f.Random.Guid())
           .RuleFor(g => g.Name, f => f.Company.CompanyName())
           .RuleFor(g => g.Screenshots, f => fakeScreenshots)
           .Generate(4);

        var result = Result.Success().Map(() => fakeGamesList);

        allGamesQuestionProvider.Setup(x => x.Execute(It.IsAny<CancellationToken>()))
           .ReturnsAsync(result);
        
        allGamesQuestionProvider.Setup(x => x.Is(GameMode.All))
           .Returns(true);

        topGamesQuestionProvider.Setup(x => x.Execute(It.IsAny<CancellationToken>()))
           .ReturnsAsync(result);
        
        topGamesQuestionProvider.Setup(x => x.Is(GameMode.TopGames))
           .Returns(true);

        relatedGamesQuestionProvider.Setup(x => x.Execute(It.IsAny<CancellationToken>()))
           .ReturnsAsync(result);
        
        relatedGamesQuestionProvider.Setup(x => x.Is(GameMode.RelatedGames))
           .Returns(true);

        var providers = new List<IGamesQuestionProvider> { allGamesQuestionProvider.Object, topGamesQuestionProvider.Object, relatedGamesQuestionProvider.Object };
        
        _handler = new GetSingleQuestionQueryHandler(providers);
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
        result.IsSuccess.Should()
           .BeTrue();

        result.Value.Games.Length
           .Should()
           .Be(4);
        
        result.Value.ImgUrl
           .Should()
           .NotBeNull();
        
        result.Value.CorrectGame
           .Should()
           .NotBeNullOrEmpty();
        
        result.Value.CorrectGame
           .Should()
           .NotBeNullOrEmpty();
    }
    
    private Task<Result<GamesQuestion>> Act() =>
        _handler.Handle(new GetSingleQuestionQuery(new Faker().PickRandom<GameMode>()), CancellationToken.None);
}
