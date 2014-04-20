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
using System.Globalization;
using System.IO;
//using System.Reflection;
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
            /*
            DateTime dt1, dt2;
            string x;
            dt1 = DateTime.Now;
            x = dt1.ToString("ddd", new CultureInfo("en-US") );
            */ 
            double x = 0;
            double z = 0;
            double y = 0;
            RgbToXyz(1, 2, 3, ref x, ref y, ref z);
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
            double x = -1;
            double y = -1;
            double z = -1;
            string ss = Bt.Content.ToString();
            //Bt.Content = "BOO!!!";
            DoEvents();
            vg.red = Convert.ToInt32(E_Red.Text);
            vg.green = Convert.ToInt32(E_Green.Text);
            vg.blue = Convert.ToInt32(E_Blue.Text);
            vg.gray = Convert.ToInt32(E_Gray.Text);
            vg.black = Convert.ToInt32(E_Black.Text);
            vg.white = Convert.ToInt32(E_White.Text);
            DateTime vrem = DateTime.Now;
            string dayString = vrem.ToString("ddd MMM d H:mm:ss yyyy", new CultureInfo("en-US"));
            ThisMemo1.Items.Clear();
            ThisMemo1.Items.Add("CTI1");
            ThisMemo1.Items.Add("");
            ThisMemo1.Items.Add("DESCRIPTOR \"Argyll Calibration Target chart information 1\"");
            ThisMemo1.Items.Add("ORIGINATOR \"Argyll targen\"");
            ThisMemo1.Items.Add("CREATED \"" + dayString + "\"");
            ThisMemo1.Items.Add("KEYWORD \"APPROX_WHITE_POINT\"");
            RgbToXyz(100, 100, 100, ref x, ref y, ref z);
            ThisMemo1.Items.Add("APPROX_WHITE_POINT \"" + x.ToString("0.000000", new CultureInfo("en-US")) + " " +
                                                          y.ToString("0.000000", new CultureInfo("en-US")) + " " +
                                                          z.ToString("0.000000", new CultureInfo("en-US")) + "\"");
            ThisMemo1.Items.Add("KEYWORD \"COLOR_REP\"");
            ThisMemo1.Items.Add("COLOR_REP \"iRGB\"");
            ThisMemo1.Items.Add("KEYWORD \"WHITE_COLOR_PATCHES\"");
            ThisMemo1.Items.Add("WHITE_COLOR_PATCHES \"" + Convert.ToInt32(vg.white) + "\"");
            ThisMemo1.Items.Add("KEYWORD \"BLACK_COLOR_PATCHES\"");
            ThisMemo1.Items.Add("BLACK_COLOR_PATCHES \"" + Convert.ToInt32(vg.black) + "\"");
            ThisMemo1.Items.Add("KEYWORD \"OFPS_PATCHES\"");
            ThisMemo1.Items.Add("OFPS_PATCHES \"512\"");
            ThisMemo1.Items.Add("");
            ThisMemo1.Items.Add("NUMBER_OF_FIELDS 7");
            ThisMemo1.Items.Add("BEGIN_DATA_FORMAT");
            ThisMemo1.Items.Add("SAMPLE_ID RGB_R RGB_G RGB_B XYZ_X XYZ_Y XYZ_Z");
            ThisMemo1.Items.Add("END_DATA_FORMAT");
            ThisMemo1.Items.Add("NUMBER_OF_SETS 520");
            ThisMemo1.Items.Add("BEGIN_DATA");
            int nn = 0;
            int rr, gg, bb;
            // Black
            for (int i = 0; i < vg.black; i++ )
            {
                nn++;
                ThisMemo1.Items.Add(nn.ToString() + " " + RgbToXyz(0, 0, 0, ref x, ref y, ref z));
            }
            // r g b
            for (int b = 0; b < vg.blue; b++ )
            {
                for (int g=0; g<vg.green; g++)
                {
                    for (int r=0; r<vg.red; r++)
                    {
                        if ((r == 0) && (g == 0) && (b == 0))
                            continue;
                        if ((r == (vg.red - 1)) && (g == (vg.green - 1)) && (b == (vg.blue - 1)))
                            continue;
                        nn++;
                        ss = nn.ToString() + " ";
                        rr = 255 * r / (vg.red - 1);
                        gg = 255 * g / (vg.green - 1);
                        bb = 255 * b / (vg.blue - 1);
                        ss = ss + RgbToXyz(Convert.ToDouble(rr) / 2.55,
                                           Convert.ToDouble(gg) / 2.55,
                                           Convert.ToDouble(bb) / 2.55,
                                           ref x, ref y, ref z);
                        ThisMemo1.Items.Add(ss);
                    }
                }
            }
            // gray
            for (int i = 0; i < vg.gray - 1;i++ )
            {
                for (int j=0;j<7;j++)
                {
                    int kr = 0;
                    int kg = 0;
                    int kb = 0;
                    if ((j & (1 << 0)) > 0) kr = 1;
                    if ((j & (1 << 1)) > 0) kg = 1;
                    if ((j & (1 << 2)) > 0) kb = 1;
                    if ((kr == 0) && (kg == 0) && (kb == 0) && (i == 0))
                        continue;
                    nn++;
                    ss = nn.ToString() + " ";
                    rr = 255 * (i + kr) / (vg.gray - 1);
                    gg = 255 * (i + kg) / (vg.gray - 1);
                    bb = 255 * (i + kb) / (vg.gray - 1);
                    ss = ss + RgbToXyz(Convert.ToDouble(rr) / 2.55,
                                       Convert.ToDouble(gg) / 2.55,
                                       Convert.ToDouble(bb) / 2.55,
                                       ref x, ref y, ref z);
                    ThisMemo1.Items.Add(ss);
                }
            }
            // white
            for (int i = 0; i < vg.white - 1;i++ )
            {
                nn++;
                ss = nn.ToString() + " ";
                rr = 255;
                gg = 255;
                bb = 255;
                ss = ss + RgbToXyz(Convert.ToDouble(rr) / 2.55,
                                   Convert.ToDouble(gg) / 2.55,
                                   Convert.ToDouble(bb) / 2.55,
                                   ref x, ref y, ref z);
                ThisMemo1.Items.Add(ss);
            }
            // corr
            ThisMemo1.Items[20] = "NUMBER_OF_SETS " + nn.ToString();
            ThisMemo1.Items[14] = "OFPS_PATCHES " + Convert.ToString(nn - vg.black - vg.white);
            //add
            ThisMemo1.Items.Add("END_DATA");
            ThisMemo1.Items.Add("");
            ThisMemo1.Items.Add("CTI1");
            ThisMemo1.Items.Add("");
            ThisMemo1.Items.Add("DESCRIPTOR \"Argyll Calibration Target chart information 1\"");
            ThisMemo1.Items.Add("ORIGINATOR \"Argyll targen\"");
            ThisMemo1.Items.Add("KEYWORD \"DENSITY_EXTREME_VALUES\"");
            ThisMemo1.Items.Add("DENSITY_EXTREME_VALUES \"8\"");
            dayString = vrem.ToString("MMMM d, yyyy", new CultureInfo("en-US"));
            ThisMemo1.Items.Add("CREATED \"" + dayString + "\"");
            ThisMemo1.Items.Add("");
            ThisMemo1.Items.Add("KEYWORD \"INDEX\"");
            ThisMemo1.Items.Add("NUMBER_OF_FIELDS 7");
            ThisMemo1.Items.Add("BEGIN_DATA_FORMAT");
            ThisMemo1.Items.Add("INDEX RGB_R RGB_G RGB_B XYZ_X XYZ_Y XYZ_Z");
            ThisMemo1.Items.Add("END_DATA_FORMAT");
            ThisMemo1.Items.Add("");
            ThisMemo1.Items.Add("NUMBER_OF_SETS 8");
            ThisMemo1.Items.Add("BEGIN_DATA");
            ThisMemo1.Items.Add("0 " + RgbToXyz(255/2.55, 255/2.55, 255/2.55, ref x, ref y, ref z));
            ThisMemo1.Items.Add("1 " + RgbToXyz(0/2.55, 120/2.55, 255/2.55, ref x, ref y, ref z));
            ThisMemo1.Items.Add("2 " + RgbToXyz(255/2.55, 0/2.55, 202/2.55, ref x, ref y, ref z));
            ThisMemo1.Items.Add("3 " + RgbToXyz(0/2.55, 0/2.55, 150/2.55, ref x, ref y, ref z));
            ThisMemo1.Items.Add("4 " + RgbToXyz(255/2.55, 170/2.55, 0/2.55, ref x, ref y, ref z));
            ThisMemo1.Items.Add("5 " + RgbToXyz(0/2.55, 91/2.55, 0/2.55, ref x, ref y, ref z));
            ThisMemo1.Items.Add("6 " + RgbToXyz(215/2.55, 0/2.55, 0/2.55, ref x, ref y, ref z));
            ThisMemo1.Items.Add("7 " + RgbToXyz(0/2.55, 0/2.55, 0/2.55, ref x, ref y, ref z));
            ThisMemo1.Items.Add("END_DATA");
            ThisMemo1.Items.Add("");
            ThisMemo1.Items.Add("CTI1");
            ThisMemo1.Items.Add("");
            ThisMemo1.Items.Add("DESCRIPTOR \"Argyll Calibration Target chart information 1\"");
            ThisMemo1.Items.Add("ORIGINATOR \"Argyll targen\"");
            ThisMemo1.Items.Add("KEYWORD \"DEVICE_COMBINATION_VALUES\"");
            ThisMemo1.Items.Add("DEVICE_COMBINATION_VALUES \"9\"");
            dayString = vrem.ToString("MMMM d, yyyy", new CultureInfo("en-US"));
            ThisMemo1.Items.Add("CREATED \"" + dayString + "\"");
            ThisMemo1.Items.Add("");
            ThisMemo1.Items.Add("KEYWORD \"INDEX\"");
            ThisMemo1.Items.Add("NUMBER_OF_FIELDS 7");
            ThisMemo1.Items.Add("BEGIN_DATA_FORMAT");
            ThisMemo1.Items.Add("INDEX RGB_R RGB_G RGB_B XYZ_X XYZ_Y XYZ_Z");
            ThisMemo1.Items.Add("END_DATA_FORMAT");
            ThisMemo1.Items.Add("");
            ThisMemo1.Items.Add("NUMBER_OF_SETS 9");
            ThisMemo1.Items.Add("BEGIN_DATA");
            ThisMemo1.Items.Add("0 " + RgbToXyz(255 / 2.55, 255 / 2.55, 255 / 2.55, ref x, ref y, ref z));
            ThisMemo1.Items.Add("1 " + RgbToXyz(0 / 2.55, 255 / 2.55, 255 / 2.55, ref x, ref y, ref z));
            ThisMemo1.Items.Add("2 " + RgbToXyz(255 / 2.55, 0 / 2.55, 255 / 2.55, ref x, ref y, ref z));
            ThisMemo1.Items.Add("3 " + RgbToXyz(0 / 2.55, 0 / 2.55, 255 / 2.55, ref x, ref y, ref z));
            ThisMemo1.Items.Add("4 " + RgbToXyz(255 / 2.55, 255 / 2.55, 0 / 2.55, ref x, ref y, ref z));
            ThisMemo1.Items.Add("5 " + RgbToXyz(0 / 2.55, 255 / 2.55, 0 / 2.55, ref x, ref y, ref z));
            ThisMemo1.Items.Add("6 " + RgbToXyz(255 / 2.55, 0 / 2.55, 0 / 2.55, ref x, ref y, ref z));
            ThisMemo1.Items.Add("7 " + RgbToXyz(0 / 2.55, 0 / 2.55, 0 / 2.55, ref x, ref y, ref z));
            ThisMemo1.Items.Add("8 " + RgbToXyz(127 / 2.55, 127 / 2.55, 127 / 2.55, ref x, ref y, ref z));
            ThisMemo1.Items.Add("END_DATA");
            //Thread.Sleep(5000);
            //Bt.Content = ss;
            Bt.Content = nn.ToString();
            Bt.IsEnabled = true;
            bt_save.IsEnabled = true;
        }
        private void DoEvents()
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new ThreadStart(delegate { }));
        }   
        static string RgbToXyz(double red, double green, double blue, ref double x, ref double y, ref double z)
        {
            var k = new double[3, 3] {
                {0.3935891, 0.3652497, 0.1916313},
                {0.2124132, 0.7010437, 0.0865432},
                {0.0187423, 0.1119313, 0.9581563}
            };
            x = (red * k[0, 0]) + (green * k[0, 1]) + (blue * k[0, 2]);
            y = (red * k[1, 0]) + (green * k[1, 1]) + (blue * k[1, 2]);
            z = (red * k[2, 0]) + (green * k[2, 1]) + (blue * k[2, 2]);
            string ss;
            ss =
                red.ToString("0.000000") + " " +
                green.ToString("0.000000") + " " +
                blue.ToString("0.000000") + " " +
                x.ToString("0.000000") + " " +
                y.ToString("0.000000") + " " +
                z.ToString("0.000000");
            return ss;
        }
        public class vg
        {
            public static int red;
            public static int green;
            public static int blue;
            public static int gray;
            public static int black;
            public static int white;
        }

        private void bt_save_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Document"; // Default file name
            dlg.DefaultExt = ".text"; // Default file extension
            dlg.Filter = "Argyllcms print |*.ti1"; // Filter files by extension
            Nullable<bool> result = dlg.ShowDialog();
            if (result == false)
                return;
            string filename = dlg.FileName;
            List<string> list = new List<string>();
            for (int i=0;i<ThisMemo1.Items.Count;i++)
            {
                list.Add(ThisMemo1.Items[i].ToString());
            }
            System.IO.File.WriteAllLines(filename, list.ToArray(), Encoding.ASCII);
        }
    }
}
