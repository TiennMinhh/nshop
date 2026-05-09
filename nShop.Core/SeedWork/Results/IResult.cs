namespace nShop.Core.SeedWork.Results;

public interface IResult
{
    ResultStatus Status { get; }
    IEnumerable<ResultError> Errors { get; }
    bool IsSuccess { get; }
}

public interface IResult<T> where T : class
{
    T? Value { get; }
    ResultStatus Status { get; }
    IEnumerable<ResultError> Errors { get; }
    bool IsSuccess { get; }
}

public enum ResultStatus
{
    Success,
    NotFound,
    Invalid,
    Failure
}