using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.Wpf
{
    /// <summary>
    /// Класс, который отвечает за работу с базой данных
    /// </summary>
    class Database
    {
        private EmailsDataContext emails = new EmailsDataContext();
        public IQueryable<Email> Emails => from c in emails.Emails select c;
    }
}
