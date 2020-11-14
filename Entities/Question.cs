using Core;
using System;

namespace Entities
{
    public class Question: IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string QuestionText { get; set; }
        public DateTime Date { get; set; }
    }
}
