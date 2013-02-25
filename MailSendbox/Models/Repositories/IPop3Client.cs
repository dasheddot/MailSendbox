namespace MailSendbox.Models.Repositories
{
    public interface IPop3Client
    {
        void Connect(string host, int port, bool useSsl);
        void Authenticate(string user, string password, AuthenticationMethod authMethod);
        int GetMessageCount();
        Mail GetMail(int messageNumber);
        void Disconnect();
        void Delete(int messageId);
    }
}