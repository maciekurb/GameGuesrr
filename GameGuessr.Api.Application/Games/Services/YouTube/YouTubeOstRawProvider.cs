using System.Text.Json;
using CSharpFunctionalExtensions;
using GameGuessr.Api.Application.Common;
using GameGuessr.Api.Application.Common.Services;
using GameGuessr.Api.Domain;
using GameGuessr.Api.Domain.Common;
using Microsoft.Extensions.Logging;

namespace GameGuessr.Api.Application.Games.Services.YouTube;

public class YouTubeOstRawProvider : Strategy<YouTubeOstProviderType>, IYouTubeOstProvider
{
    public override YouTubeOstProviderType StrategyType() => YouTubeOstProviderType.Raw;
    
    
    private readonly ILogger<YouTubeOstRawProvider> _logger;
    private readonly HttpClient _httpClient;

    public YouTubeOstRawProvider(IHttpClientFactory httpClientFactory, ILogger<YouTubeOstRawProvider> logger)
    {
        _logger = logger;
        _httpClient = httpClientFactory.CreateClient();
    }

    public Task<Result<Game>> ExecuteAsync(Game game, CancellationToken cancellationToken)
        => Result.Success()
           .Map(() => Execute(game, cancellationToken));

    public async Task<Game> Execute(Game game, CancellationToken cancellationToken)
    {
        try
        {
            var httpRequestMessage =
                new HttpRequestMessage(HttpMethod.Get,
                    $"https://www.youtube.com/results?search_query={game.Name}+main+theme");

            var response = await _httpClient.SendAsync(httpRequestMessage, cancellationToken);

            var content = await response.Content.ReadAsStringAsync(cancellationToken);

            var ytInitialData = content.Split("ytInitialData = ").Last();

            var closingBracketPosition = FindClosingBracketIndex(ytInitialData);

            var formatedData = ytInitialData.Substring(0, closingBracketPosition + 1);

            var json = JsonSerializer.Deserialize<Root>(formatedData);

            var models = json?
               .contents?
               .twoColumnSearchResultsRenderer?
               .primaryContents?
               .sectionListRenderer?
               .contents?
               .FirstOrDefault()
             ?
            .itemSectionRenderer?
               .contents?
               .Select(x => x?.videoRenderer)
               .Select(x => new
                {
                    Title = x?.title?.runs.FirstOrDefault()?.text ?? String.Empty,
                    Duration = ToDurationInSec(x?.lengthText?.simpleText),
                    VideoId = x?.videoId
                })
               .Where(x => x.Duration >= 30 && x.Title.ToLower().Contains("soundtrack") || x.Title.ToLower().Contains("ost"))
               .ToList();

            var model = models?
               .FirstOrDefault(x => x.Duration >= 30);

            if (model != null)
                game.SetOstYouTubeKey(model.VideoId, model.Duration);

            _logger.LogInformation($"Set {model?.Title} OST for {game.Name}");

            return game;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Parsing error: { ex.Message }");
        }

        return game;
    }
    
    private int FindClosingBracketIndex(string text, char openedBracket = '{', char closedBracket = '}')
    {
        int index = text.IndexOf(openedBracket);
        int bracketCount = 1;
        var textArray = text.ToCharArray();

        for (int i = index + 1; i < textArray.Length; i++)
        {
            if (textArray[i] == openedBracket)
            {
                bracketCount++;
            }
            else if (textArray[i] == closedBracket)
            {
                bracketCount--;
            }

            if (bracketCount == 0)
            {
                index = i;
                break;
            }
        }

        return index;
    }

    private int ToDurationInSec(string duration)
    {
        if (duration == null || duration.HasValue() == false)
            return 0;
        
        if (duration.Contains(':') == false)
            return Int32.Parse(duration);
        
        var splitedDuration = duration.Split(":");
        var durationInSec = 0;

        switch (splitedDuration.Length)
        {
            case 3:
                durationInSec += Int32.Parse(splitedDuration[0]) * 60 * 60;
                durationInSec += Int32.Parse(splitedDuration[1]) * 60;
                durationInSec += Int32.Parse(splitedDuration[2]);

                break;
            case 2:
                durationInSec += Int32.Parse(splitedDuration[0]) * 60;
                durationInSec += Int32.Parse(splitedDuration[1]);

                break;
        }

        return durationInSec;
    }
}
