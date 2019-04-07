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

            TabSwitcher.TabSwitcherControl tabSwitcher = new TabSwitcher.TabSwitcherControl();
            griddd.Children.Add(tabSwitcher);

            comboBoxSenderSelect.ItemsSource = SampleData.Senders;
            comboBoxSenderSelect.DisplayMemberPath = "Key";
            comboBoxSenderSelect.SelectedValuePath = "Value";

            comboBoxSmtpSelect.ItemsSource = SampleData.Servers;
            comboBoxSmtpSelect.DisplayMemberPath = "Key";
            comboBoxSmtpSelect.SelectedValuePath = "Value";

            Database db = new Database();
            dataGridEmails.ItemsSource = db.Emails;
        }

        private void MenuItemClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonSendAsScheduled_Click(object sender, RoutedEventArgs e)
        {
            if (BodyTextEmpty())
            {
                MessageBox.Show("Mail body empty", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var scheduler = new MyScheduler();

            TimeSpan sendTime = scheduler.GetSendTime(timePicker.Text);

            if (sendTime == new TimeSpan())
            {
                MessageBox.Show("Incorrect date format", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            DateTime sendDateTime = (calendar.SelectedDate ?? DateTime.Today).Add(sendTime);

            if (sendDateTime < DateTime.Now)
            {
                MessageBox.Show("Date and time can't be earlier than now", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                tabControl.SelectedItem = tabItemMailEditor;
                return;
            }

            EmailSendServiceClass emailSender =
                new EmailSendServiceClass(comboBoxSenderSelect.Text, comboBoxSenderSelect.SelectedValue.ToString());

            scheduler.SendEmails(sendDateTime, emailSender, dataGridEmails.ItemsSource as IQueryable<Email>);
        }

        private bool BodyTextEmpty()
        {
            TextRange range = new TextRange(richTextBoxBody.Document.ContentStart, richTextBoxBody.Document.ContentEnd);
            string bodyText = range.Text;

            return string.IsNullOrWhiteSpace(bodyText);
        }

        private void ButtonSendImmediately_Click(object sender, RoutedEventArgs e)
        {
            if (BodyTextEmpty())
            {
                MessageBox.Show("Mail body empty", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                tabControl.SelectedItem = tabItemMailEditor;
                return;
            }

            string login = comboBoxSenderSelect.Text;
            string password = comboBoxSenderSelect.SelectedValue.ToString();

            if (string.IsNullOrEmpty(login))
            {
                MessageBox.Show("Enter sender login", "Invalid input", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Enter sender password", "Invalid input", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            EmailSendServiceClass emailSender = new EmailSendServiceClass(login, password);
            emailSender.SendMails((IQueryable<Email>)dataGridEmails.ItemsSource);
        }

        private void TabSwitcher1_ButtonNextClick(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedItem = tabItemScheduler;
        }

        private void TabSwitcher2_ButtonPreviousClick(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedItem = tabItemFormMailingGroups;
        }

        private void TabSwitcher2_ButtonNextClick(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedItem = tabItemMailEditor;
        }

        private void TabSwitcherControl_ButtonPreviousClick(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedItem = tabItemScheduler;
        }
    }
}
