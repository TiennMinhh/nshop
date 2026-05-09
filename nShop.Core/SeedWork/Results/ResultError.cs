namespace nShop.Core.SeedWork.Results;

public class ResultError(string errorCode, string errorMessage, Exception? exception = default)
{
    public string ErrorCode { get; } = errorCode;
    public string ErrorMessage { get; } = errorMessage;
    public Exception? Exception { get; } = exception;
}