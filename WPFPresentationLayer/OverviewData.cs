using DomainLibrary.Domain;
using System;
using System.Text;
using System.Windows.Media.Imaging;

namespace WPFPresentationLayer
{
    public class OverviewData
    {
        public int colId { get; set; }
        public BitmapImage col0 { get; set; }
        public BitmapImage col1 { get; set; }
        public DateTime col2 { get; set; }
        public string col3 { get; set; }
        public string col4 { get; set; }
        public string col5 { get; set; }
        public string col6 { get; set; }
        public string col7 { get; set; }
        public string col8 { get; set; }

        public OverviewData(CyclingSession cs)
        {
            colId = cs.Id;
            col0 = new BitmapImage(new Uri(@"..\..\cycling.png", UriKind.Relative));
            col1 = OVTrainingType(cs.TrainingType);
            col2 = cs.When;
            col3 = OVkm(cs.Distance);
            col4 = OVTimeSpan(cs.Time);
            col5 = OVkmh(cs.AverageSpeed);
            col6 = OVWatt(cs.AverageWatt);
            col7 = OVBikeType(cs.BikeType);
            col8 = cs.Comments;
        }
        public OverviewData(RunningSession rs)
        {
            colId = rs.Id;
            col0 = new BitmapImage(new Uri(@"..\..\running.png", UriKind.Relative));
            col1 = OVTrainingType(rs.TrainingType);
            col2 = rs.When;
            col3 = OVm(rs.Distance);
            col4 = OVTimeSpan(rs.Time);
            col5 = OVkmh(rs.AverageSpeed);
            col6 = "";
            col7 = "";
            col8 = rs.Comments;
        }

        public string OVkm(float? a)
        {
            //convert distance to string
            if (a != null)
                return a + " km";
            else
                return "";
        }
        public string OVm(int a)
        {
            //convert distance (m) to string
            return a + " m";
        }
        public string OVkmh(float? a)
        {
            //convert speed to 
            if (a != null)
                return Math.Round((decimal)a) + " km/h";
            else
                return "";
        }
        public string OVWatt(int? a)
        {
            //convert watt to string
            if (a != null)
                return a + " Watt";
            else
                return "";
        }
        public string OVTimeSpan(TimeSpan a)
        {
            //convert to string
            StringBuilder sb = new StringBuilder();
            sb.Append(a.ToString(@"hh\:mm\:ss"));
            return sb.ToString();
        }
        public string OVBikeType(BikeType a)
        {
            //convert to string
            switch (a)
            {
                case BikeType.CityBike:
                    return "City bike";
                case BikeType.IndoorBike:
                    return "Indoor bike";
                case BikeType.MountainBike:
                    return "Mountain bike";
                case BikeType.RacingBike:
                    return "Racing bike";
                default:
                    return "Unknown bike";
            }
        }
        public BitmapImage OVTrainingType(TrainingType a)
        {
            //BitmapImage b = new BitmapImage();
            //b.U
            //convert to image
            switch (a)
            {
                case TrainingType.Endurance:
                    return new BitmapImage(new Uri(@"..\..\endurance.png", UriKind.Relative));
                case TrainingType.Interval:
                    return new BitmapImage(new Uri(@"..\..\interval.png", UriKind.Relative));
                case TrainingType.Recuperation:
                    return new BitmapImage(new Uri(@"..\..\recuperation.png", UriKind.Relative));
                default:
                    return new BitmapImage(new Uri(@"..\..\circle.png", UriKind.Relative));
            }
        }

    }
}
