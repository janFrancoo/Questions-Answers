namespace Core.Helpers.Result
{
    public interface IDataResult<T>: IResult
    {
        T Data { get; }
    }
}
