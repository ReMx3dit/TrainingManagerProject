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
    /// Interaction logic for CyclingReport.xaml
    /// </summary>
    public partial class CyclingReport : Page
    {
        private Report report;
        public CyclingReport()
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
            amountCyclingsessions.Text = report.CyclingSessions.ToString();
            cyclingsessionTime.Text = report.TotalCyclingTrainingTime.ToString();
            cyclingsessionDistance.Text = report.TotalCyclingDistance.ToString();

            List<OverviewData> timeLine = new List<OverviewData>();
            foreach (var x in report.TimeLine)
            {
                if (x.Item1 == SessionType.Cycling)
                    timeLine.Add(new OverviewData(x.Item2 as CyclingSession));
            }
            dgOverview.ItemsSource = timeLine;

            dgMaxWatt.ItemsSource = new List<OverviewData>() { new OverviewData(report.MaxWattSessionCycling) };
            dgMaxDist.ItemsSource = new List<OverviewData>() { new OverviewData(report.MaxDistanceSessionCycling) };
            dgMaxSpeed.ItemsSource = new List<OverviewData>() { new OverviewData(report.MaxSpeedSessionCycling) };
        }

        public List<DateTime> GetPossibleDates()
        {
            Dictionary<string, DateTime> dates = new Dictionary<string, DateTime>();

            BackEndData.GetAllCyclingData().ForEach(x => dates.TryAdd(x.col2.ToString("MM-yyyy"), x.col2));

            List<DateTime> sortedList = dates.Values.ToList();
            sortedList.Sort();
            sortedList.Reverse();
            return sortedList;
        }
    }
}
