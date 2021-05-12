using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace BusinessLayer.Services
{
    public class EmailSender
    {
        private const string username = "rvtvote@gmail.com";
        private const string password = "Ialoveni1";
        public static void Send(string email, string pass)
        {
            MailAddress From = new MailAddress("rvtvote@gmail.com", "RVT Vote");
            MailAddress To = new MailAddress(email);
            MailMessage msg = new MailMessage(From, To);
            msg.Subject = "Înregistrare - RVT";
            msg.Body = "Codul de înregistrare - " + pass;
            msg.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential(username, password);
            smtp.EnableSsl = true;
            smtp.Send(msg);
        }

        public static void SendVoteResponse(string email, string message)
        {
            MailAddress From = new MailAddress("rvtvote@gmail.com", "RVT Vote");
            MailAddress To = new MailAddress(email);
            MailMessage msg = new MailMessage(From, To);
            msg.Subject = "Răspunsul de votare - RVT";
            msg.Body = message;
            msg.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential(username, password);
            smtp.EnableSsl = true;
            smtp.Send(msg);
        }
    }
}
