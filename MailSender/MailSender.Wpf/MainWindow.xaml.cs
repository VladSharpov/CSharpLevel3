using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Net.Mail;

namespace MailSender.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnSendMail_Click(object sender, RoutedEventArgs e)
        {
            List<string> mails = new List<string> { "easybreezy12345@yandex.ru" };

            try
            {
                foreach (string mail in mails)
                {
                    EmailSendService emailSendService =
                        new EmailSendService(MyHostData.Host, MyHostData.Port, loginBox.Text, passwordBox.Password);
                    emailSendService.Send(mail, headerBox.Text, mailTextBox.Text);
                }

                MessageBox.Show("Mail sent", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Can't send mail", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
