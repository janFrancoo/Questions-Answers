using Core.Helpers.Result;
using DataAccess;
using Entities;
using System;
using System.Collections.Generic;

namespace Business
{
    public class QuestionService : IQuestionService
    {
        private IQuestionDao _questionDao;

        public QuestionService(IQuestionDao questionDao)
        {
            _questionDao = questionDao;
        }

        public IResult Add(Question question)
        {
            _questionDao.Add(question);
            return new SuccessResult();
        }

        public IResult Delete(int id)
        {
            var question = _questionDao.Get(q => q.Id == id);
            if (question == default)
                return new ErrorResult("No such question");

            _questionDao.Delete(question);
            return new SuccessResult();
        }

        public IDataResult<List<Question>> GetAll()
        {
            return new SuccessDataResult<List<Question>>(_questionDao.GetList());
        }

        public IDataResult<List<Question>> GetByDate(DateTime date)
        {
            return new SuccessDataResult<List<Question>>(_questionDao.GetList(q => q.Date == date));
        }

        public IDataResult<Question> GetById(int id)
        {
            var question = _questionDao.Get(q => q.Id == id);
            if (question == default)
                return new ErrorDataResult<Question>("No such question");

            return new SuccessDataResult<Question>(question);
        }

        public IDataResult<List<Question>> GetByUser(int userId)
        {
            return new SuccessDataResult<List<Question>>(_questionDao.GetList(q => q.UserId == userId));
        }

        public IResult Update(Question question)
        {
            _questionDao.Update(question);
            return new SuccessResult();
        }
    }
}
