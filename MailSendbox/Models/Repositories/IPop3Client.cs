using OpenPop.Mime;
using OpenPop.Pop3;

namespace MailSendbox.Models.Repositories {
    public interface IPop3Client {

        void Connect(string host, int port, bool useSsl);
        void Authenticate(string user, string password, AuthenticationMethod authMethod);
        int GetMessageCount();
        Message GetMessage(int messageId);
        void Disconnect();

    }
}