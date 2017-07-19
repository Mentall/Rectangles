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

namespace Rectangulation
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowVM();
        }

        private void mainCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            (DataContext as MainWindowVM).MouseDown(e.GetPosition(mainCanvas));
        }

        private void mainCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            (DataContext as MainWindowVM).MouseMove(e.GetPosition(mainCanvas));
        }

        private void mainCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            (DataContext as MainWindowVM).MouseUp(e.GetPosition(mainCanvas));
        }
    }
}
