namespace OT.Assessment.Application.Common
{
    public class Result<T>
    {
        public bool IsSuccess { get; init; }
        public string Error { get; init; }
        public T Value { get; init; }

        public static Result<T> Success(T value) => new Result<T> { IsSuccess = true, Value = value };

        public static Result<T> Failure(string error) => new Result<T> { IsSuccess = false, Error = error };
    }
    public class Result
    {
        public bool IsSuccess { get; init; }
        public string? Error { get; init; }

        public bool IsFailure => !IsSuccess;

        public static Result Success() => new Result { IsSuccess = true };
        public static Result Failure(string error) => new Result { IsSuccess = false, Error = error };
    }
}
