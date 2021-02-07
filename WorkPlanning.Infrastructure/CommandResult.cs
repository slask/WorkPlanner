namespace WorkPlanning.Infrastructure
{
    public class CommandResult
    {
        protected CommandResult(bool isSuccess, string error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }

        public static CommandResult Failure(string error) => new CommandResult(false, error);

        public static CommandResult Success() => new CommandResult(true, null);

        public string Error { get; private set; }

        public bool IsSuccess { get; private set; }

        public bool IsFailure => !IsSuccess;
    }

    public class CommandResult<T> : CommandResult
    {
        private CommandResult(bool isSuccess, string error)
            : base(isSuccess, error)
        {
        }

        private CommandResult(bool isSuccess, T value)
            : base(isSuccess, null)
        {
            Value = value;
        }

        public new static CommandResult<T> Failure(string error) => new CommandResult<T>(false, error);

        public static CommandResult<T> Success(T value) => new CommandResult<T>(true, value);

        public object Value { get; private set; }
    }
}