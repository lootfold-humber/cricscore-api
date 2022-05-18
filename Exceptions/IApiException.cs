namespace CricScore.Exceptions;

public interface IApiException
{
    int HttpStatusCode { get; }
    string Message { get; }
}
