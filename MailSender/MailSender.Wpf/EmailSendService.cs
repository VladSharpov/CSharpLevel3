using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MailSender.Wpf
{
    class EmailSendService
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string From { get; set; }
        public string Password { get; set; }

        public EmailSendService (string host, int port, string from, string password)
        {
            Host = host;
            Port = port;
            From = from;
            Password = password;
        }

        public void Send(string mailAdress, string header, string body)
        {
            using (MailMessage mm = new MailMessage(From, mailAdress))
            {
                mm.Subject = header;
                mm.Body = body;
                mm.IsBodyHtml = false;
                mm.Priority = MailPriority.High;

                using (SmtpClient sc = new SmtpClient(MyHostData.Host, MyHostData.Port))
                {
                    sc.EnableSsl = true;
                    sc.DeliveryMethod = SmtpDeliveryMethod.Network;
                    sc.UseDefaultCredentials = false;
                    sc.Credentials = new NetworkCredential(From, Password);
                    sc.Send(mm);
                }
            }
        }
    }
}
