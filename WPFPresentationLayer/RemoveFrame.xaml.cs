using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
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
    /// Interaction logic for RemoveFrame.xaml
    /// </summary>
    public partial class RemoveFrame : Page
    {
        public RemoveFrame()
        {
            InitializeComponent();
            radioLoopTraining.IsChecked = true;
        }
        public static void SortDataGrid(DataGrid dataGrid, int columnIndex = 0, ListSortDirection sortDirection = ListSortDirection.Descending)
        {
            var column = dataGrid.Columns[columnIndex];

            // Clear current sort descriptions
            dataGrid.Items.SortDescriptions.Clear();

            // Add the new sort description
            dataGrid.Items.SortDescriptions.Add(new SortDescription(column.SortMemberPath, sortDirection));

            // Apply sort
            foreach (var col in dataGrid.Columns)
            {
                col.SortDirection = null;
            }
            column.SortDirection = sortDirection;

            // Refresh items to display sort
            dataGrid.Items.Refresh();
        }
        private void radioLoopTraining_Checked(object sender, RoutedEventArgs e)
        {
            dgOverview.ItemsSource = BackEndData.GetAllRunningData();
            SortDataGrid(dgOverview, 2);
        }

        private void radioFietsTraining_Checked(object sender, RoutedEventArgs e)
        {
            dgOverview.ItemsSource = BackEndData.GetAllCyclingData();
            SortDataGrid(dgOverview, 2);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var SelectedItem = dgOverview.SelectedIndex;
            OverviewData item = dgOverview.Items[SelectedItem] as OverviewData;
            if(radioLoopTraining.IsChecked == true)
            {
                BackEndData.RemoveRunningSession(item.colId);
            }
            else
            {
                BackEndData.RemoveCyclingSession(item.colId);
            }
        }
    }
}
