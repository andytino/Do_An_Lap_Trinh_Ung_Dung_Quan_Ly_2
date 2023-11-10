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

namespace PosApp.Components
{
    /// <summary>
    /// Interaction logic for TitleBar.xaml
    /// </summary>
    public partial class TitleBar : UserControl
    {
        public TitleBar()
        {
            InitializeComponent();
        }

        private void Window_Hide(object sender, MouseButtonEventArgs e)
        {
            Window window = Window.GetWindow(Application.Current.MainWindow);
            if (window != null)
            {
                window.WindowState = WindowState.Minimized;
            }
        }

        private void Window_Close(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_dragMove(object sender, MouseButtonEventArgs e)
        {
            Window window = Window.GetWindow(Application.Current.MainWindow);
            if (window != null)
            {
                window.DragMove();
            }

        }

        private void Window_Resize(object sender, MouseButtonEventArgs e)
        {
            Window window = Window.GetWindow(Application.Current.MainWindow);
            if (window != null)
            {
                if (window.WindowState == WindowState.Maximized)
                {
                    window.WindowState = WindowState.Normal;
                }
                else if (window.WindowState == WindowState.Normal)
                {
                    window.WindowState = WindowState.Maximized;
                }
            }
        }
    }
}
