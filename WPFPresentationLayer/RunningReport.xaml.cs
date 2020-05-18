using DomainLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for RunningReport.xaml
    /// </summary>
    public partial class RunningReport : Page
    {
        private Report report;
        public RunningReport()
        {
            InitializeComponent();
            datePicker.ItemsSource = GetPossibleDates();
            datePicker.SelectedIndex = 0;
        }

        private void datePicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime picked = (DateTime)datePicker.SelectedItem;
            report = BackEndData.GenerateMonthReport(picked.Month, picked.Year);
            UpdateValues();
        }

        private void UpdateValues()
        {
            amountRunsessions.Text = report.RunningSessions.ToString();
            runsessionTime.Text = report.TotalRunningTrainingTime.ToString();
            runsessionDistance.Text = report.TotalRunningDistance.ToString();

            List<OverviewData> timeLine = new List<OverviewData>();
            foreach (var x in report.TimeLine)
            {
                if (x.Item1 == SessionType.Running)
                    timeLine.Add(new OverviewData(x.Item2 as RunningSession));
            }
            dgOverview.ItemsSource = timeLine;

            dgMaxDist.ItemsSource = new List<OverviewData>() { new OverviewData(report.MaxDistanceSessionRunning) };
            dgMaxSpeed.ItemsSource = new List<OverviewData>() { new OverviewData(report.MaxSpeedSessionRunning) };
        }

        public List<DateTime> GetPossibleDates()
        {
            Dictionary<string, DateTime> dates = new Dictionary<string, DateTime>();

            BackEndData.GetAllRunningData().ForEach(x => dates.TryAdd(x.col2.ToString("MM-yyyy"), x.col2));

            List<DateTime> sortedList = dates.Values.ToList();
            sortedList.Sort();
            sortedList.Reverse();
            return sortedList;
        }
    }
}
