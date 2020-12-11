using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace BusinessLayer.Services
{
    public class EmailSender
    {
        public static void Send(string email, string pass)
        {
            // Who send?
            MailAddress From = new MailAddress("rvtvote@gmail.com", "RVT Vote");
            string utilizator = "rvtvote@gmail.com";
            string password = "Ialoveni1";
            // where to send?
            MailAddress To = new MailAddress(email);
            MailMessage msg = new MailMessage(From, To);
            msg.Subject = "Registration - RVT";
            msg.Body = "Registration Code - " + pass;
            msg.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential(utilizator, password);
            smtp.EnableSsl = true;
            smtp.Send(msg);
        }
    }
}
