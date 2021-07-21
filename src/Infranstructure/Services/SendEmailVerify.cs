using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace Infranstructure.Services
{
    public class SendEmailVerify
    {
        public static void Send(string to, string subject, string body)
        {

            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("Smtp.gmail.com");

            mail.From = new MailAddress("info@salarnili.ir", "Your Portofilo");
            mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            SmtpServer.Host = "smtp.iran.liara.ir";
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        }
    }
}
