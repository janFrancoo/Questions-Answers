using Core.Helpers.Result;
using DataAccess;
using Entities;
using System.Collections.Generic;

namespace Business
{
    public class AnswerService : IAnswerService
    {
        private IAnswerDao _answerDao;
        private IAnswerLikeDao _answerLikeDao;

        public AnswerService(IAnswerDao answerDao, IAnswerLikeDao answerLikeDao)
        {
            _answerDao = answerDao;
            _answerLikeDao = answerLikeDao;
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
            return new SuccessDataResult<List<Answer>>(_answerDao.GetWithLikeCount(a => a.QuestionId == questionId));
        }

        public IDataResult<List<Answer>> GetAnswersByUser(int userId)
        {
            return new SuccessDataResult<List<Answer>>(_answerDao.GetWithLikeCount(a => a.UserId == userId));
        }

        public IResult LikeAnswer(int userId, int answerId)
        {
            var likeToCheck = _answerLikeDao.Get(l => l.UserId == userId);

            if (likeToCheck != default)
                _answerLikeDao.Delete(likeToCheck);
            else
                _answerLikeDao.Add(new AnswerLike
                {
                    UserId = userId,
                    AnswerId = answerId
                });

            return new SuccessResult();
        }

        public IResult Update(Answer answer)
        {
            _answerDao.Update(answer);
            return new SuccessResult();
        }
    }
}
