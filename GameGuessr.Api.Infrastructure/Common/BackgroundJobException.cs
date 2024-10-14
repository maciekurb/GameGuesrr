namespace GameGuessr.Api.Infrastructure.Common
{
    public class BackgroundJobException : Exception
    {
        public BackgroundJobException() { }
        public BackgroundJobException(string message)
            : base(message) { }
    }
}
