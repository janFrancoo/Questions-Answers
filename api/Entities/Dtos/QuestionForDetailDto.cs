using Core.Entities;

namespace Entities.Dtos
{
    public class QuestionForDetailDto : IDto
    {
        public Question Question { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Avatar { get; set; }
    }
}
