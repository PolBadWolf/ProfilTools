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
using System.Windows.Threading;
using System.Threading;
//using System.Threading.Tasks;

namespace CardGen1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        KeyConverter converter = new KeyConverter();
        public MainWindow()
        {
            InitializeComponent();
            ThisMemo1.Items.Clear();
        }
        private void E_Key_PreviewKeyDown(object sender, KeyEventArgs e)
        {

            //ThisMemo1.Items[0] = e.Key;
            e.Handled = true;
            string ks = converter.ConvertToString(e.Key);
            char k;
            if (ks.Length>1 )
            {
                if (ks=="Backspace")
                    e.Handled = false;
            }
            else
            {
                k = ks[0];
                if ( (k>='0') && (k<='9') )
                    e.Handled = false;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button Bt = (Button)sender;
            Bt.IsEnabled = false;
            string ss = Bt.Content.ToString();
            Bt.Content = "BOO!!!";
            DoEvents();
            Thread.Sleep(5000);
            Bt.Content = ss;
            Bt.IsEnabled = true;
        }
        private void DoEvents()
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new ThreadStart(delegate { }));
        }   

    }
}
