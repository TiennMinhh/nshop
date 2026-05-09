namespace nShop.Core.SeedWork.Results;

public class Result : IResult
{
    public required ResultStatus Status { get; set; }
    public IEnumerable<ResultError> Errors { get; set; } = [];
    public bool IsSuccess => Status == ResultStatus.Success;
    
    public static Result Success()
    {
        return new Result
        {
            Status = ResultStatus.Success
        };
    }
    
    public static Result Failure()
    {
        return new Result
        {
            Status = ResultStatus.Failure,
        };
    }

    public static Result Failure(ResultError error)
    {
        return new Result
        {
            Status = ResultStatus.Failure,
            Errors = [error]
        };
    }

    public static Result Failure(IEnumerable<ResultError> errors)
    {
        return new Result
        {
            Status = ResultStatus.Failure,
            Errors = errors
        };
    }

    public static Result NotFound()
    {
        return new Result
        {
            Status = ResultStatus.NotFound
        };
    }
}

public class Result<T> : IResult<T> where T : class
{
    public T? Value { get; set;  }
    public required ResultStatus Status { get; set; }
    public IEnumerable<ResultError> Errors { get; set; } = [];
    public bool IsSuccess => Status == ResultStatus.Success;
    
    public static Result<T> Success(T value)
    {
        return new Result<T>
        {
            Value = value,
            Status = ResultStatus.Success
        };
    }

    public static Result<T> Failure()
    {
        return new Result<T>
        {
            Value = null,
            Status = ResultStatus.Failure,
        };
    }

    public static Result<T> Failure(ResultError error)
    {
        return new Result<T>
        {
            Value = null,
            Status = ResultStatus.Failure,
            Errors = [error]
        };
    }

    public static Result<T> Failure(IEnumerable<ResultError> errors)
    {
        return new Result<T>
        {
            Value = null,
            Status = ResultStatus.Failure,
            Errors = errors
        };
    }

    public static Result<T> NotFound()
    {
        return new Result<T>
        {
            Value = null,
            Status = ResultStatus.NotFound
        };
    }
}