using System.Configuration;
using System.Net.Mail;
using System.Threading.Tasks;
using FolderMailService.ViewModel;


namespace FolderMailService.Classes
{
    public class SendEmail
    {
        private MailViewModel email = new MailViewModel();
        public SendEmail(MailViewModel _mail)
        {
            email = _mail;
        }
        public async Task SendMail()
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(email.SENDER, email.SENDERNAME);
            var recipients = email.RECEIVERS;
            foreach (string recipient in recipients)
            {
                mail.To.Add(recipient);
            }
            mail.IsBodyHtml = false;
            mail.Body = email.MESSAGE;
            mail.Subject = email.SUBJECT;
            using (SmtpClient client = new SmtpClient())
            {
                client.Port = int.Parse(ConfigurationManager.AppSettings["Port"]);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Host = ConfigurationManager.AppSettings["Host"];
                client.DeliveryFormat = SmtpDeliveryFormat.SevenBit;
                await client.SendMailAsync(mail);
            }
        }

    }
}
