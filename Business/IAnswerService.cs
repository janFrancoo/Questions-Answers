using Core.Helpers.Result;
using Entities;
using System.Collections.Generic;

namespace Business
{
    public interface IAnswerService
    {
        IDataResult<List<Answer>> GetAnswersByQuestion(int questionId);
        IDataResult<List<Answer>> GetAnswersByUser(int userId);
        IResult Add(Answer answer);
        IResult Update(Answer answer);
        IResult Delete(int id);
    }
}
