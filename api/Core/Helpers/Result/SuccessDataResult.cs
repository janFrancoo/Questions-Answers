﻿namespace Core.Helpers.Result
{
    public class SuccessDataResult<T>: DataResult<T>
    {
        public SuccessDataResult() : base(true, default) { }
        public SuccessDataResult(string message) : base(true, message, default) { }
        public SuccessDataResult(T data) : base(true, data) { }
        public SuccessDataResult(string message, T data) : base(true, message, data) { }
    }
}
