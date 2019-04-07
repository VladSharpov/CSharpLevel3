using PasswordDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.Wpf
{
    static class SampleData
    {
        public static Dictionary<string, string> Senders { get; } = new Dictionary<string, string>()
        {
            { "79257443993@yandex.ru", Password.Decrypt("1234l;i") },
            { "sok74@yandex.ru", Password.Decrypt(";liq34tjk") }
        };

        public static Dictionary<string, int> Servers { get; } = new Dictionary<string, int>()
        {
            { "smtp.yandex.ru", 587 },
            { "smtp.mail.ru", 465 },
            { "smtp.gmail.com", 465 }
        };
    }
}
