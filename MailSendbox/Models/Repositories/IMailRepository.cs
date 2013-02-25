using System.Collections.Generic;

namespace MailSendbox.Models.Repositories
{
    public interface IMailRepository
    {
        IEnumerable<Mail> Get();
        void Delete(Mail serverMail);
    }
}