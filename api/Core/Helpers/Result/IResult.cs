namespace Core.Helpers.Result
{
    public interface IResult
    {
        bool Success { get; }
        string Message { get; }
    }
}
