using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace MovieRecommendationSystem.Infrastructure.ServicesInfra
{
    public class MailHelper
    {
        public static void SendRecommandationMail(string email, string mailBody)
        {

            MailMessage mail = new MailMessage();

            SmtpClient smtp = new SmtpClient();

            smtp.Credentials = new NetworkCredential("recommandationsystemdeneme@gmail.com", "oqsa ywhq scfu zbxb");
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;

            mail.IsBodyHtml = true;
            mail.To.Add(email);
            mail.From = new MailAddress("recommandationsystemdeneme@gmail.com");
            mail.Subject = "Recommandation of Movie";
            mail.Body = mailBody;


            smtp.Send(mail);
        }

    }
}
