namespace OrderingSystem.Global.Common
{
    public class Result<T>
    {
        public bool IsSuccess { get; private set; }
        public T? ReturnedObj { get; private set; }
        public string? Message { get; private set; }

        private Result(bool isSuccess, T? returnObject, string? message)
        {
            IsSuccess = isSuccess;
            ReturnedObj = returnObject;
            Message = message;
        }

        public static Result<T> Success(T returnObject, string? message = null!) => new Result<T>(true, returnObject, message);
        public static Result<T> Failure(string errorMessage) => new Result<T>(false, default, errorMessage);
    }
}
