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

namespace TabSwitcher
{
    /// <summary>
    /// Interaction logic for TabSwitcherControl.xaml
    /// </summary>
    public partial class TabSwitcherControl : UserControl
    {
        public event RoutedEventHandler ButtonNextClick;
        public event RoutedEventHandler ButtonPreviousClick;

        public TabSwitcherControl()
        {
            InitializeComponent();
        }

        #region Properties

        private bool previousIsHidden = false; // поле, соответствующее тому, будет ли скрыта кнопка «Предыдущий»
        private bool nextIsHidden = false; // поле, соответствующее тому, будет ли скрыта кнопка «Следующий»

        /// <summary>
        /// Свойство, соответствующее тому, будет ли скрыта кнопка «Предыдущий». 
        /// Чтобы Свойство отразилось на PropertiesGrid у нашего контрола, его нужно представить именно свойством, а не полем
        /// </summary>
        public bool PreviousIsHidden
        {
            get => previousIsHidden;
            set
            {
                previousIsHidden = value;
                SetButtons(); // метод, который отвечает на отрисовку кнопок
            }
        }

        public bool NextIsHidden
        {
            get => nextIsHidden;
            set
            {
                nextIsHidden = value;
                SetButtons(); // метод, который отвечает за отрисовку кнопок
            }
        }

        private void SetPreviousOffNextOn()
        {
            buttonNext.Visibility = Visibility.Hidden;
            buttonPrevious.Visibility = Visibility.Visible;
            buttonPrevious.Width = 150;
            buttonNext.Width = 0;
            buttonPrevious.HorizontalAlignment = HorizontalAlignment.Stretch;
        }

        private void SetPreviousOnNextOff()
        {
            buttonPrevious.Visibility = Visibility.Hidden;
            buttonNext.Visibility = Visibility.Visible;
            buttonNext.Width = 150;
            buttonPrevious.Width = 0;
            buttonNext.HorizontalAlignment = HorizontalAlignment.Stretch;
        }

        private void SetPreviousOffNextOff()
        {
            buttonNext.Visibility = Visibility.Visible;
            buttonPrevious.Visibility = Visibility.Visible;
            buttonNext.Width = 75;
            buttonPrevious.Width = 75;
        }

        private void SetPreviousOnNextOn()
        {
            buttonPrevious.Visibility = Visibility.Hidden;
            buttonNext.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Метод, который отвечает за отрисовку кнопок.
        /// Есть три варианта: когда обе кнопки доступны; доступна одна и недоступна вторая; обе кнопки недоступны
        /// </summary>
        private void SetButtons()
        {
            if (previousIsHidden && nextIsHidden)
            {
                SetPreviousOnNextOn();
            }
            else if (!nextIsHidden && !previousIsHidden)
            {
                SetPreviousOffNextOff();
            }
            else if (nextIsHidden && !previousIsHidden)
            {
                SetPreviousOffNextOn();
            }
            else if (!nextIsHidden && previousIsHidden)
            {
                SetPreviousOnNextOff();
            }
        }


        #endregion

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            ButtonNextClick?.Invoke(sender, e);
        }

        private void ButtonPrevious_Click(object sender, RoutedEventArgs e)
        {
            ButtonPreviousClick?.Invoke(sender, e);
        }
    }
}
