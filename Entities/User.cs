using Core;

namespace Entities
{
    public class User: IEntity
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public bool Status { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
    }
}
