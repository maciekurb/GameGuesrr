using CSharpFunctionalExtensions;
using GameGuessr.Api.Domain;
using GameGuessr.Api.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace GameGuessr.Api.Application.Games.Services.RAWGIOG;

public partial class RAWGIOGamesSuggestionsProvider : IGamesSuggestionsProvider
{
    private readonly ILogger<RAWGIOGamesProvider> _logger;
    private readonly AppDbContext _dbContext;
    private readonly HttpClient _httpClient;
    private readonly List<GameSuggestion> _gameSuggestions = new();

    public RAWGIOGamesSuggestionsProvider(IHttpClientFactory httpClientFactory, ILogger<RAWGIOGamesProvider> logger, AppDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
        _httpClient = httpClientFactory.CreateClient();
    }

    public Task<Result<IReadOnlyList<GameSuggestion>>> ExecuteAsync(IReadOnlyList<Game> games, CancellationToken cancellationToken) =>
        Result.Success()
           .Map(async () =>
            {
                var gamesSuggestionProccessed = 1;

                foreach (var game in games)
                {
                    await Result.Success()
                       .Bind(() => GetGameSuggestons(game, cancellationToken));
                    
                    _logger.LogInformation($"GameSuggestion proccessed {gamesSuggestionProccessed}/{games.Count}");

                    await Task.Delay(100, cancellationToken);
                    gamesSuggestionProccessed++;
                }

                return (IReadOnlyList<GameSuggestion>)_gameSuggestions;
            });

    private Task<Result> GetGameSuggestons(Game game, CancellationToken cancellationToken) =>
        Result.Success()
           .Map(() =>
            {
                var httpRequestMessage =
                    new HttpRequestMessage(HttpMethod.Get,
                        $"https://rawg.io/api/games/{game.ExternalId}/suggested?key=c542e67aec3a4340908f9de9e86038af");

                return _httpClient.SendAsync(httpRequestMessage, cancellationToken);
            })
           .Ensure(response => response.IsSuccessStatusCode, "Request failed")
           .Map(async response =>
            {
                var content = await response.Content.ReadAsStringAsync(cancellationToken);
                var gamesSuggestionBatch = JsonConvert.DeserializeObject<RAWGIOGamesSuggestionsProvider.Response>(content);

                var dbGames = await _dbContext.Games.ToDictionaryAsync(x => x.ExternalId, x => x.Id, cancellationToken);

                gamesSuggestionBatch.Results = gamesSuggestionBatch.Results.Where(x => dbGames.ContainsKey(x.Id)).ToArray();

                _logger.LogInformation($"Batch size {gamesSuggestionBatch.Results.Length}");

                return (gamesSuggestionBatch, dbGames);
            })
           .Bind(data => 
                Result.Combine(data.gamesSuggestionBatch.Results.Select(rsg => CreateGameSuggestion(game.Id, data.dbGames[rsg.Id]))));

    private Result CreateGameSuggestion(Guid gameId, Guid suggestedGameId) =>
        Result.Success()
           .Bind(() => GameSuggestion.Create(gameId, suggestedGameId))
           .Tap(game =>
            {
                _logger.LogInformation($"Processing game suggestion with Id { suggestedGameId }");
                _gameSuggestions.Add(game);
            });
}
