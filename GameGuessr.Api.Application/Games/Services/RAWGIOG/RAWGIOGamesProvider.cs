using CSharpFunctionalExtensions;
using GameGuessr.Api.Domain;
using GameGuessr.Api.Infrastructure;
using GameGuessr.Api.Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace GameGuessr.Api.Application.Games.Services.RAWGIOG;

public partial class RAWGIOGamesProvider : IGamesProvider
{
    private readonly ILogger<RAWGIOGamesProvider> _logger;
    private readonly AppDbContext _dbContext;
    private readonly KeysSettings _keySettings;
    private readonly HttpClient _httpClient;
    private readonly List<Game> _games = new();

    private bool _next = true;

    public RAWGIOGamesProvider(IHttpClientFactory httpClientFactory, ILogger<RAWGIOGamesProvider> logger, AppDbContext dbContext, IOptions<KeysSettings> keySettings)
    {
        _logger = logger;
        _dbContext = dbContext;
        _keySettings = keySettings.Value;
        _httpClient = httpClientFactory.CreateClient();
    }

    public Task<Result<IReadOnlyList<Game>>> ExecuteAsync(CancellationToken cancellationToken) =>
        Result.Success()
           .Map(async () =>
            {
                _logger.LogInformation($"Starting API requests");
                var pageResult = 0;

                do
                {
                    await Result.Success()
                       .Bind(() => GetGame(++pageResult, cancellationToken));

                    await Task.Delay(500, cancellationToken);
                }
                while (_next);

                _logger.LogInformation($"API requests finished");
                return (IReadOnlyList<Game>)_games;
            });

    private Task<Result> GetGame(int page, CancellationToken cancellationToken) =>
        Result.Success()
           .Map(() =>
            {
                var httpRequestMessage =
                    new HttpRequestMessage(HttpMethod.Get,
                        $"https://rawg.io/api/games?ordering=name&page={page}&page_size=100000000&metacritic=40,100&key={_keySettings.RAWGIOGKey}");

                return _httpClient.SendAsync(httpRequestMessage, cancellationToken);
            })
           .Ensure(response => response.IsSuccessStatusCode, "Request failed")
           .Map(async response =>
            {
                var content = await response.Content.ReadAsStringAsync(cancellationToken);
                var gamesBatch = JsonConvert.DeserializeObject<Response>(content);
                        
                var dbGamesExternalIds = await _dbContext.Games.Select(x => x.ExternalId).ToListAsync(cancellationToken);
                
                gamesBatch.Results = gamesBatch.Results.Where(x => x.short_screenshots != null && x.short_screenshots.Any()).ToArray();
                gamesBatch.Results = gamesBatch.Results.Where(x => dbGamesExternalIds.Contains(x.Id) == false).ToArray();
                _logger.LogInformation($"Batch size { gamesBatch.Results.Length }");
                _logger.LogInformation($"Page { page }. Is next: { String.IsNullOrEmpty(gamesBatch.Next) == false }");
                
                return gamesBatch;
            })
           .Tap(response => _next = String.IsNullOrEmpty(response.Next) == false)
           .Bind(response => Result.Combine(response.Results.Select(CreateGameResult)));

    private Result CreateGameResult(ResponseGame responseGame) =>
        Result.Success()
           .Tap(() => _logger.LogInformation($"Processing game {responseGame.Name} with external ID {responseGame.Id}"))
           .Bind(() => Game.Create(responseGame.Name, responseGame.Released.GetValueOrDefault(), responseGame.Metacritic, responseGame.Id))
           .Bind(game =>
            {
                _games.Add(game);
                
                return Result.Combine(
                    game.BindScreenshots(responseGame.short_screenshots.Select(s => s.Image).ToArray()),
                    game.BindPlatforms(responseGame.Platforms?.Select(p => p.Platform.Name).ToArray()),
                    game.BindGenres(responseGame.Genres?.Select(g => g.Name).ToArray())
                    );
            });
}
