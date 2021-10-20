using CENSO.Models;
using CensoAPI02.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.ServiceModel;

//using WebMailServiceDLL.WSMailing;
//using WebMailServiceDLL;
//using WSMailingSend;
using MailingWebservice;



namespace CensoAPI02.Intserfaces
{
    public class EmailHandler
    {
        private static string emailOrigen = "au_gl_sm_gl_local_apps@continental.com";
        private static string displayName = "CENSO Test";

        // Obtencion del nombre del tema que se encuentra relacionado con el ticket
        public string getThemeName(CDBContext _context, int themeId)
        {
            var ticketTheme = from theme in _context.Theme
                           where theme.tId == themeId
                           select new { theme.tName };

            if (ticketTheme == null || ticketTheme.Count() == 0)
            {
                return null;
            }

            return ticketTheme.First().tName.ToString();
        }

        // Obtencion del correo de los usuarios relacionados al tema
        public List<string> getUserEmails(CDBContext _context, int themeId)
        {
            var mails = from user in _context.HRU
                         join ut in _context.HRUsersThemes on user.uEmployeeNumber equals ut.UserId
                         join theme in _context.Theme on ut.ThemeId equals theme.tId
                         where user.uStatus == true && theme.tId == themeId
                         select new { user.uEmail };

            if (mails == null || mails.Count() == 0)
            {
                return null;
            }

            List<string> emails = new List<string>();

            foreach (var mail in mails)
            {
                emails.Add(mail.uEmail);
            }

            return emails;
        }

        // Envio del correo
        public void sendMails(MailData mailData)
        {
            try
            {
                foreach (string email in mailData.emails)
                {
                    MailMessage mailMessage = new MailMessage(emailOrigen, email);
                    mailMessage.Subject = "CENSO Regional";
                    mailMessage.Body = $"<p>Sea ha generado una nueva solicitud relacionada al tema {mailData.themeName}, con numero de folio {mailData.ticketId} </p>";
                    mailMessage.IsBodyHtml = true;

                    //MS.SendEmailAsync("CENSO", email, "", mailMessage.Subject, mailMessage.Body, TypeOfMail.HTML);

                    // Implementación del mailing service
                    MailingWebServiceClient client = new MailingWebServiceClient();
                    SendEmailRequest request = new SendEmailRequest("CENSO", email, "", mailMessage.Subject, mailMessage.Body, TypeOfMail.HTML);
                    client.SendEmail(request);

                    /*
                    SmtpClient smtp = new SmtpClient("SMTPHubEU.contiwan.com");
                    //smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    smtp.Port = 2525;
                    //smtp.Credentials = new NetworkCredential(emailOrigen, pass);
                    smtp.Send(mailMessage);
                    smtp.Dispose();
                    */
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }           
        }
    }
}
