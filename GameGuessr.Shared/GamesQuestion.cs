using GameGuessr.Shared.Common;

namespace GameGuessr.Shared;

public class GamesQuestion
{
    public string[] Games { get; set; }
    public string CorrectGame { get; set; }
    public string[] ImgUrl { get; set; }
    public string Platforms { get; set; }
    public string Genres { get; set; }
    public string OstYouTubeQuery { get; set; }
    
    public static GamesQuestionBuilder Create(string correctGame, string[] games, string[] urls) =>
        new(correctGame, games, urls);
}

public class GamesQuestionBuilder : IBuilder<GamesQuestion>
{
    private readonly GamesQuestion _gamesQuestion;
    
    internal GamesQuestionBuilder(string correctGame, string[] games, string[] urls)
    {
        var random = new Random();

        _gamesQuestion = new GamesQuestion
        {
            CorrectGame = correctGame,
            Games = games.OrderBy(g => random.Next()).ToArray(),
            ImgUrl = urls.OrderBy(g => random.Next()).ToArray()
        };
    }

    public GamesQuestion Build() => _gamesQuestion;

    public GamesQuestionBuilder WithPlatforms(string[] platforms)
    {
        _gamesQuestion.Platforms = String.Join(',', platforms);

        return this;
    }
    
    public GamesQuestionBuilder WithGenres(string[] genres)
    {
        _gamesQuestion.Genres = String.Join(',', genres);

        return this;
    }
    
    public GamesQuestionBuilder WithYouTubeOst(string youtubeVideoId, int duration)
    {
        var randomOstTime = new Random();
        var time = duration > 30 ? randomOstTime.Next(1, duration - 30) : 0;
        
        _gamesQuestion.OstYouTubeQuery = $"{youtubeVideoId}?autoplay=1&start={time}&end={time+25}";

        return this;
    }
}
