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

namespace WPFPresentationLayer
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

        private void trainingAdd_Click(object sender, RoutedEventArgs e)
        {
            ContFrame.Source = new Uri("AddFrame.xaml", UriKind.Relative);
        }

        private void trainingOverview_Click(object sender, RoutedEventArgs e)
        {
            ContFrame.Source = new Uri("OverviewFrame.xaml", UriKind.Relative);
        }

        private void trainingRemove_Click(object sender, RoutedEventArgs e)
        {
            ContFrame.Source = new Uri("RemoveFrame.xaml", UriKind.Relative);
        }

        private void reportTotal_Click(object sender, RoutedEventArgs e)
        {
            ContFrame.Source = new Uri("TrainingReport.xaml", UriKind.Relative);
        }

        private void reportRun_Click(object sender, RoutedEventArgs e)
        {

        }

        private void reportCyc_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
