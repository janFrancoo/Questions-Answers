using System.Threading.Tasks;

namespace Core.Helpers.Mail
{
    public interface IMailer
    {
        Task SendMailAsync(string mail, string subject, string body);
    }
}
