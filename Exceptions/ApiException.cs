namespace CricScore.Exceptions;

public class ApiValidationException : Exception, IApiException
{
    public ApiValidationException(string message)
    : base(message)
    {
        HttpStatusCode = 400;
    }
    public int HttpStatusCode { get; }
}
