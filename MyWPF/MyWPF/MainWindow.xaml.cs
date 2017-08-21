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

namespace MyWPF
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            //初级窗体跳转
            Window1 wm = new Window1();
            wm.Show();
            wm.WindowState = WindowState.Normal;
            this.Close();

            //NavigationService.GetNavigationService(this).Navigate(new Uri("Window1.xaml", UriKind.Relative));
            //NavigationService.GetNavigationService(this).GoForward(); //向后转
            //NavigationService.GetNavigationService(this).GoBack(); //向前转

            //NavigationWindow window = new NavigationWindow();
            //window.Source = new Uri("Window1.xaml", UriKind.Relative);
            //window.Show();
        }
    }
}
