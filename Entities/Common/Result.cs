namespace Entities.Common
{
    public class Result<TEntity>(TEntity value, bool isSuccess, string message)
    {
        public TEntity Value { get; set; } = value;

        public bool IsSuccess { get; set; } = isSuccess;

        public string Message { get; set; } = message;
    }
}
