using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace MailSender.Wpf
{
    /// <summary>
    /// Класс-планировщик, который создает расписание, следит за его выполнением и напоминает о событиях
    /// Также помогает автоматизировать рассылку писем в соответствии с расписанием
    /// </summary>
    class MyScheduler
    {
        DispatcherTimer timer = new DispatcherTimer(); // таймер 
        EmailSendServiceClass emailSender;         // экземпляр класса, отвечающего за отправку писем
        DateTime dateTimeSend;                           // дата и время отправки
        IQueryable<Email> emails;                  // коллекция email-ов адресатов

        /// <summary>
        /// Метод, который превращает строку из текстбокса textBoxTimePicker в TimeSpan
        /// </summary>
        /// <param name="strSendTime"></param>
        /// <returns></returns>
        public TimeSpan GetSendTime(string strSendTime)
        {
            TimeSpan tsSendTime = new TimeSpan();

            try
            {
                tsSendTime = TimeSpan.Parse(strSendTime);
            }
            catch { }

            return tsSendTime;
        }

        /// <summary>
        //// Непосредственно отправка писем
        /// </summary>
        /// <param name="dtSend"></param>
        /// <param name="emailSender"></param>
        /// <param name="emails"></param>
        public void SendEmails(DateTime dateTimeSend, EmailSendServiceClass emailSender, IQueryable<Email> emails)
        {
            this.emailSender = emailSender; // Экземпляр класса, отвечающего за отправку писем, присваиваем 
            this.dateTimeSend = dateTimeSend;
            this.emails = emails;

            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (dateTimeSend.ToShortTimeString() == DateTime.Now.ToShortTimeString())
            {
                emailSender.SendMails(emails);
                timer.Stop();
                MessageBox.Show("Mails sent", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
