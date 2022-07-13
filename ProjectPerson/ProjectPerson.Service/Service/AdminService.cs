using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using ProjectPerson.Service.IService;
using ProjectPerson.DataAccess.IRepository;
using ProjectPerson.Common.Models;
using ProjectPerson.Service.EmailService;

namespace ProjectPerson.Service.Service
{
    public class AdminService : IAdminService
    {
        public readonly IGenericRepository<Person> _genericRepositroy;
        public readonly IEmailSender _emailSender;
        public AdminService(IGenericRepository<Person> genericRepositroy, IEmailSender emailSender)
        {
            _genericRepositroy = genericRepositroy;
            _emailSender = emailSender;
        }

        public async Task AcceptAccount(int idPerson)
        {
            await Verification(Enums.VerificationStatus.Active, idPerson);
        }

        public async Task DeniedAccount(int idPerson)
        {
            await Verification(Enums.VerificationStatus.Denied, idPerson);
        }

        private async Task Verification(Enums.VerificationStatus status, int idPerson)
        {
            Person person = await _genericRepositroy.GetByObject(idPerson);
            if (person == null)
            {
                throw new KeyNotFoundException("User does not exists.");
            }
            if (person.PersonType == Enums.PersonType.Customer || person.PersonType == Enums.PersonType.Admin || person.PersonType == Enums.PersonType.None)
            {
                throw new Exception("Deliverer only can be verified.");
            }

            person.Verification = status;
            await _genericRepositroy.Update(person);
            await _genericRepositroy.Save();


            await SendMail(person.Email, status);
        }

        private async Task SendMail(string email, Enums.VerificationStatus status)
        {
            StringBuilder sb = new StringBuilder();
            string username = email.Split('@')[0];

            if (email == null)
            {
                throw new ArgumentNullException("Email does not exist.");
            }
            if (email == "")
            {
                throw new FormatException("Email does not exist.");
            }

            sb.AppendLine($"Hi {username},\n");
            sb.AppendLine($"This is status of your verification {status.ToString().ToUpper()}.");
            sb.AppendLine("\nKind regards");

            var message = new Message(
                new string[] { email },
                "Verification status",
                sb.ToString()
            );

            await _emailSender.SendEmail(message);
        }

    }
}
