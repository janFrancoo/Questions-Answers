using Core.Helpers.Result;
using Entities;
using System;
using System.Collections.Generic;

namespace Business
{
    public interface IQuestionService
    {
        IDataResult<List<Question>> GetAll();
        IDataResult<Question> GetById(int id);
        IDataResult<List<Question>> GetByUser(int userId);
        IDataResult<List<Question>> GetByDate(DateTime date);
        IResult Add(Question question);
        IResult Update(Question question);
        IResult Delete(int id);
    }
}
