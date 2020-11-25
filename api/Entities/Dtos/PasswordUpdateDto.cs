using Core.Entities;

namespace Entities.Dtos
{
    public class PasswordUpdateDto: IDto
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
