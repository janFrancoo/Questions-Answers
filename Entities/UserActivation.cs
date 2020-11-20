using Core;

namespace Entities
{
    public class UserActivation: IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ActivationCode { get; set; }
    }
}
