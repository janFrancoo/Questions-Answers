using Core;

namespace Entities
{
    public class AnswerLike: IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AnswerId { get; set; }
    }
}
