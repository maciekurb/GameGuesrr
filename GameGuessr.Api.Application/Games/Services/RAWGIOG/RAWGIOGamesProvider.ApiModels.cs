namespace GameGuessr.Api.Application.Games.Services.RAWGIOG;

public partial class RAWGIOGamesProvider
{
    private class Response
    {
        public string Next { get; set; }
        public ResponseGame[] Results { get; set; }
    }

    private class ResponseGame
    {
        public string Name { get; set; }
        public ResponseGamePlatform[] Platforms { get; set; }
        public DateTime? Released { get; set; }
        public int Metacritic { get; set; }
        public int Id { get; set; }
        public ResponseGameScreenshot[] short_screenshots { get; set; }
        public ResponseGameGenre[] Genres { get; set; }
    }

    private class ResponseGamePlatform
    {
        public ResponseGamePlatform Platform { get; set; }
        public string Name { get; set; }
    }

    private class ResponseGameScreenshot
    {
        public int Id { get; set; }
        public string Image { get; set; }
    }

    private class ResponseGameGenre
    {
        public string Name { get; set; }
    }
}
