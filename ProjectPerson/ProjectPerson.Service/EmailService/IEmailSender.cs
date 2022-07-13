using System;
using System.Threading.Tasks;

namespace ProjectPerson.Service.EmailService
{
    public interface IEmailSender
    {
        Task SendEmail(Message message);
    }
}
