﻿namespace Core.Helpers.Result
{
    public class DataResult<T>: Result, IDataResult<T>
    {
        public T Data { get; }

        public DataResult(bool success, T data): base(success)
        {
            Data = data;
        }

        public DataResult(bool success, string message, T data) : base(success, message)
        {
            Data = data;
        }
    }
}
