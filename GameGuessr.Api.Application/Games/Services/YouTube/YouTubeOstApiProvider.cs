using CSharpFunctionalExtensions;
using GameGuessr.Api.Application.Common;
using GameGuessr.Api.Application.Common.Services;
using GameGuessr.Api.Domain;
using GameGuessr.Api.Domain.Common;
using GameGuessr.Api.Infrastructure.Settings;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GameGuessr.Api.Application.Games.Services.YouTube;

public class YouTubeOstApiProvider : Strategy<YouTubeOstProviderType>, IYouTubeOstProvider
{
    public override YouTubeOstProviderType StrategyType() => YouTubeOstProviderType.Apiv3;
    
    private readonly ILogger<YouTubeOstApiProvider> _logger;
    private readonly KeysSettings _keysSettings;

    public YouTubeOstApiProvider(ILogger<YouTubeOstApiProvider> logger, IOptions<KeysSettings> keysSettings)
    {
        _logger = logger;
        _keysSettings = keysSettings.Value;
    }

    public Task<Result<Game>> ExecuteAsync(Game game, CancellationToken cancellationToken) =>
        Result.Success()
           .EnsureIsNotDefault(game.Id, nameof(game.Id))
           .EnsureHasValue(game.Name, nameof(game.Name))
           .Bind(() => SetYouTubeOst(game, cancellationToken))
           .Map(() => game);

    private async Task<Result> SetYouTubeOst(Game game, CancellationToken cancellationToken)
    {
        if (String.IsNullOrEmpty(game.OstYouTubeKey) == false && game.OstYouTubeDuration != 0)
            return Result.Success();

        try
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = _keysSettings.YouTubeApiV3Key
            });

            var searchListRequest = youtubeService.Search.List("snippet");
            var gameName = game.Name;
            searchListRequest.Q = $"{gameName} soundtrack"; 
            searchListRequest.MaxResults = 50;
            searchListRequest.VideoEmbeddable = SearchResource.ListRequest.VideoEmbeddableEnum.True__;
            searchListRequest.Type = "video";
        
            var searchListResponse = await searchListRequest.ExecuteAsync(cancellationToken);

            var youtubeVideoKey = searchListResponse.Items
               .FirstOrDefault(x => (x.Snippet.Title.ToLower().Contains("soundtrack") || x.Snippet.Title.ToLower().Contains("ost")));

            if (youtubeVideoKey == null)
                return Result.Failure($"Coulnd't obtain { game.Name } youtube OST. No results.");

            var videoRequest = youtubeService.Videos.List("contentDetails");
            videoRequest.Id = youtubeVideoKey.Id.VideoId;
            videoRequest.MaxResults = 1;
            var videoItemRequestResponse = await videoRequest.ExecuteAsync(cancellationToken);

            // Get the videoID of the first video in the list
            var video = videoItemRequestResponse.Items[0];
            var duration = video.ContentDetails.Duration;
            
            _logger.LogInformation($"Processing { youtubeVideoKey.Snippet.Title }");
            
            return game.SetOstYouTubeKey(youtubeVideoKey.Id.VideoId, GetDurationInSeconds(duration));
        }
        catch (Exception ex)
        {
            _logger.LogCritical($"YouTube API request has failed: { ex.Message }");
        }
        
        return Result.Success();
    }

    private int GetDurationInSeconds(string duration)
    {
        var hours = GetValueBetweenTwoStrings(duration, "PT", "H") * 60 * 60;
        var minutes = GetValueBetweenTwoStrings(duration, "H", "M") * 60;
        var seconds = GetValueBetweenTwoStrings(duration, "M", "S");

        return hours + minutes + seconds;
    }
    
    private int GetValueBetweenTwoStrings(string str , string firstString, string lastString)
    {
        var pos1 = str.IndexOf(firstString, StringComparison.Ordinal) + firstString.Length;

        if (pos1 is -1 or 0)
            return 0;
        var pos2 = str.IndexOf(lastString, StringComparison.Ordinal);
        if (pos2 is -1 or 0)
            return 0;
        var finalString = str.Substring(pos1, pos2 - pos1);
        
        return Int32.Parse(finalString);
    }
}
