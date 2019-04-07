using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EmailSendServiceDLL
{
    class EmailSendServiceClass
    {
        private string login;         // email, c которого будет рассылаться почта
        private string password;  // пароль к email, с которого будет рассылаться почта
        private string smtp = "smtp.yandex.ru"; // smtp-server
        private int port = 587;                // порт для smtp-server
        private string body;                    // текст письма для отправки
        private string subject;                 // тема письма для отправки

        public EmailSendServiceClass(string login, string password)
        {
            this.login = login;
            this.password = password;
        }

        private void SendMail(string mail, string name) // Отправка email конкретному адресату
        {
            using (MailMessage message = new MailMessage(login, mail))
            {
                message.Subject = subject;
                message.Body = $"Hello, { name }!";
                message.IsBodyHtml = false;
                SmtpClient client = new SmtpClient(smtp, port)
                {
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(login, password)
                };

                try
                {
                    client.Send(message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Can't send email", 
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public void SendMails(IQueryable<Email> emails)
        {
            foreach (Email email in emails)
            {
                SendMail(email.Value, email.Name);
            }
        }
    }

    //class EmailSendService
    //{
    //    public string Host { get; set; }
    //    public int Port { get; set; }
    //    public string From { get; set; }
    //    public string Password { get; set; }

    //    public EmailSendService (string host, int port, string from, string password)
    //    {
    //        Host = host;
    //        Port = port;
    //        From = from;
    //        Password = password;
    //    }

    //    public void Send(string mailAdress, string header, string body)
    //    {
    //        using (MailMessage mm = new MailMessage(From, mailAdress))
    //        {
    //            mm.Subject = header;
    //            mm.Body = body;
    //            mm.IsBodyHtml = false;
    //            mm.Priority = MailPriority.High;

    //            using (SmtpClient sc = new SmtpClient(MyHostData.Host, MyHostData.Port))
    //            {
    //                sc.EnableSsl = true;
    //                sc.DeliveryMethod = SmtpDeliveryMethod.Network;
    //                sc.UseDefaultCredentials = false;
    //                sc.Credentials = new NetworkCredential(From, Password);
    //                sc.Send(mm);
    //            }
    //        }
    //    }
    //}
}
