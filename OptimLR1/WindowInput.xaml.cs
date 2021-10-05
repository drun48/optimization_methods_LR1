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
using System.Windows.Shapes;

namespace OptimLR1
{
    /// <summary>
    /// Логика взаимодействия для WindowInput.xaml
    /// </summary>
    public partial class WindowInput : Window
    {
        public WindowInput()
        {
            InitializeComponent();
        }

        private void ok_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).Get_NM(Convert.ToInt16(txt_N.Text), Convert.ToInt16(txt_M.Text));
            this.Close();
        }
    }
}
