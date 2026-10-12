namespace Obt.Api.Common;

public class ApiException : Exception
{
    public int StatusCode { get; }

    public ApiException(int statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }

    public static ApiException BadRequest(string message) => new(StatusCodes.Status400BadRequest, message);
    public static ApiException Unauthorized(string message) => new(StatusCodes.Status401Unauthorized, message);
    public static ApiException Forbidden(string message) => new(StatusCodes.Status403Forbidden, message);
    public static ApiException NotFound(string message) => new(StatusCodes.Status404NotFound, message);
    public static ApiException NotImplemented(string message) => new(StatusCodes.Status501NotImplemented, message);
}
