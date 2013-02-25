namespace MailSendbox.Models.Repositories
{
    public class Pop3Client : OpenPop.Pop3.Pop3Client, IPop3Client
    {
        #region IPop3Client Members

        public Mail GetMail(int messageNumber)
        {
            var message = this.GetMessage(messageNumber);
            var plainText = message.FindFirstPlainTextVersion();
            var mail = new Mail
                           {
                               MessageId = message.Headers.MessageId,
                               Body = plainText == null ? string.Empty : plainText.GetBodyAsText(),
                               Date = message.Headers.DateSent,
                               From = message.Headers.From.Address,
                               To = message.Headers.To.Aggregate("", (seed, recipient) => seed += ", " + recipient.Address).Trim(' ', ','),
                               Subject = message.Headers.Subject
                           };
            return mail;
        }

        public void Delete(int messageId)
        {
            Delete(messageId);
        }

        #endregion
    }
}