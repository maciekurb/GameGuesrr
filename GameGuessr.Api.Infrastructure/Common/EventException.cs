namespace GameGuessr.Api.Infrastructure.Common;

public class EventException : Exception
{
    public EventException() { }
    public EventException(string message)
        : base(message) { }
}