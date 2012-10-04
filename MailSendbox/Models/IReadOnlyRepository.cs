using System.Collections.Generic;

namespace MailSendbox.Models {
    public interface IReadOnlyRepository<out T> where T : class {

        IEnumerable<T> Get();

    }
}