using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;



namespace CensoAPI02.Intserfaces
{
    public class EmailHandler
    {
        private static string emailOrigen = "brauliogrc95@gmail.com";
        private static string pass = "Cuchao101710";
        private static string displayName = "CENSO Test";

        // Envio del correo
        public void sendMails(List<string> userEmails, EmailInformation emailContent)
        {
            try
            {
                List<string> emails = new List<string>();
                emails.Add("dbhs6798@gmail.com");
                emails.Add("brauliogrc95@gmail.com");
                emails.Add("bruno100012@outlook.com");

                foreach (string email in userEmails)
                {
                    MailMessage mailMessage = new MailMessage(emailOrigen, email);
                    mailMessage.Subject = "CENSO RH";
                    mailMessage.Body = $"<p>Sea ha generado una nueva solicitud relacionada al tema {emailContent.themeId}, con numero de folio xxxx </p>";
                    mailMessage.IsBodyHtml = true;

                    SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    smtp.Port = 587;
                    smtp.Credentials = new NetworkCredential(emailOrigen, pass);
                    smtp.Send(mailMessage);
                    smtp.Dispose();
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }           
        }
    }
}
