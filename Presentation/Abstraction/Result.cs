namespace Presentation.Abstraction;

public sealed class Result
{
    private Result(bool isSuccess, params Error[] errors)
    {
        if (isSuccess && errors.Length != 0 || !isSuccess && errors.Length == 0)
        {
            throw new ArgumentException("Invalid error", nameof(errors));
        }

        IsSuccess = isSuccess;
        Errors = [.. errors];
    }

    private Result(bool isSuccess, List<Error> errors)
    {
        if (isSuccess && errors.Count != 0 || !isSuccess && errors.Count == 0)
        {
            throw new ArgumentException("Invalid error", nameof(errors));
        }

        IsSuccess = isSuccess;
        Errors = [.. errors];
    }

    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public List<Error> Errors { get; } = [];

    public static Result Success() => new Result(true);

    public static Result Failure(params Error[] errors) => new Result(false, errors);
    public static Result Failure(List<Error> errors) => new Result(false, errors);


    public static implicit operator Result(Error error) => Failure(error);

    public static implicit operator Result(Error[] errors) => Failure(errors);

    public static implicit operator Result(List<Error> errors) => Failure(errors);
}

public sealed class Result<T>
{
    private Result(bool isSuccess, T? value = default, params Error[] errors)
    {
        if (isSuccess && errors.Length != 0 || !isSuccess && errors.Length == 0)
        {
            throw new ArgumentException("Invalid error", nameof(errors));
        }

        IsSuccess = isSuccess;
        Errors = [.. errors];
        Value = value;
    }

    private Result(bool isSuccess, List<Error> errors, T? value = default)
    {
        if (isSuccess && errors.Count != 0 || !isSuccess && errors.Count == 0)
        {
            throw new ArgumentException("Invalid error", nameof(errors));
        }

        IsSuccess = isSuccess;
        Errors = [.. errors];
        Value = value;
    }

    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public T? Value { get; }

    public List<Error> Errors { get; }

    public static Result<T> Success(T? value) => new Result<T>(true, value);

    public static Result<T> Failure(params Error[] errors) => new Result<T>(false, value: default, errors);
    public static Result<T> Failure(List<Error> errors) => new Result<T>(false, errors);


    public static implicit operator Result<T>(T? value) => Success(value);

    public static implicit operator Result<T>(Error error) => Failure(error);

    public static implicit operator Result<T>(Error[] errors) => Failure(errors);

    public static implicit operator Result<T>(List<Error> errors) => Failure(errors);

    public static implicit operator Result(Result<T> result)
    {
        if (result.IsSuccess)
        {
            return Result.Success();
        }
        return Result.Failure(result.Errors);
    }
}