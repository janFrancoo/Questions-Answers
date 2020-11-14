using Core;
using System;

namespace Entities
{
    public class Answer: IEntity
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int UserId { get; set; }
        public string AnswerText { get; set; }
        public DateTime Date { get; set; }
    }
}
