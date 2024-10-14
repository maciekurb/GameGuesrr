namespace GameGuessr.Api.Application.Games.Services.RAWGIOG;

public partial class RAWGIOGamesSuggestionsProvider
{
    private class Response
    {
        public ResponseSuggestedGame[] Results { get; set; }
    }

    private class ResponseSuggestedGame
    {
        public int Id { get; set; }
    }
}
