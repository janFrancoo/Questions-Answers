using Core.Helpers.Result;
using DataAccess;
using Entities;
using System.Collections.Generic;

namespace Business
{
    public class AnswerService : IAnswerService
    {
        private IAnswerDao _answerDao;

        public AnswerService(IAnswerDao answerDao)
        {
            _answerDao = answerDao;
        }

        public IResult Add(Answer answer)
        {
            _answerDao.Add(answer);
            return new SuccessResult();
        }

        public IResult Delete(int id)
        {
            var answer = _answerDao.Get(a => a.Id == id);
            if (answer == default)
                return new ErrorResult("No such answer");

            _answerDao.Delete(answer);
            return new SuccessResult();
        }

        public IDataResult<List<Answer>> GetAnswersByQuestion(int questionId)
        {
            return new SuccessDataResult<List<Answer>>(_answerDao.GetList(a => a.QuestionId == questionId));
        }

        public IDataResult<List<Answer>> GetAnswersByUser(int userId)
        {
            return new SuccessDataResult<List<Answer>>(_answerDao.GetList(a => a.UserId == userId));
        }

        public IResult Update(Answer answer)
        {
            _answerDao.Update(answer);
            return new SuccessResult();
        }
    }
}
