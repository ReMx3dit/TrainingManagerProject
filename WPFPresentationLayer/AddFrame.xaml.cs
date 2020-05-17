using DomainLibrary.Domain;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for AddFrame.xaml
    /// </summary>
    public partial class AddFrame : Page
    {
        public AddFrame()
        {
            InitializeComponent();
            timeWhenHr.Text = "00";
            timeWhenMin.Text = "00";
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (!InputValidator())
            {
                MessageBox.Show("Er is een fout opgetreden met het formulier. Controleer uw invoer en probeer opnieuw. \n\n Indien dit probleem aanhoudt, contacteer uw administrator.");
            }
            else
            {
                InputSender();
                NavigationService.Navigate(new Uri("OverviewFrame.xaml", UriKind.Relative));
            }
        }

        private bool InputValidator()
        {
            bool when = false;
            bool distance = false;
            bool speed = false;
            bool watt = false;

            if (dateWhen.SelectedDate != null && dateWhen.SelectedDate < DateTime.Now)
                when = true;

            if (textboxSnelheid.Text != "")
            {
                if (float.TryParse(textboxSnelheid.Text, out float _))
                    speed = true;
                else
                    speed = false;
            }
            else
                speed = true;

            if (radioFietsTraining.IsChecked == true)
            {
                if (textboxAfstand.Text != "")
                {
                    if (float.TryParse(textboxAfstand.Text, out float _))
                        distance = true;
                    else
                        distance = false;
                }
                else
                    distance = true;

                if (textboxWattage.Text != "")
                {
                    if (int.TryParse(textboxWattage.Text, out int _))
                        watt = true;
                    else
                        watt = false;
                }
                else
                    watt = true;

                if (when && distance && speed && watt)
                    return true;
                else
                    return false;
            }
            else
            {
                if (textboxAfstand.Text != "")
                {
                    if (int.TryParse(textboxAfstand.Text, out int _))
                        distance = true;
                    else
                        distance = false;
                }
                else
                    distance = true;

                if (when && distance && speed)
                    return true;
                else
                    return false;
            }
            
        }

        private void InputSender()
        {
            int hr = int.Parse(timeWhenHr.Text);
            int min = int.Parse(timeWhenMin.Text);
            DateTime when = new DateTime(dateWhen.SelectedDate.Value.Year, dateWhen.SelectedDate.Value.Month, dateWhen.SelectedDate.Value.Day, hr, min, 0);

            TimeSpan time = DoubleToTimeSpan(sliderTimeSpan.Value);

            float? avgSpeed;
            if (textboxSnelheid.Text != "" && float.TryParse(textboxSnelheid.Text, out float _))
                avgSpeed = float.Parse(textboxSnelheid.Text);
            else
                avgSpeed = null;

            TrainingType trainingType = TrainingType.Endurance;
            switch (comboboxTrainingType.SelectedIndex)
            {
                case 0:
                    trainingType = TrainingType.Endurance;
                    break;
                case 1:
                    trainingType = TrainingType.Interval;
                    break;
                case 2:
                    trainingType = TrainingType.Recuperation;
                    break;
                default:
                    break;
            }
            string comments = richtextComments.Text.ToString();

            if(radioFietsTraining.IsChecked == true)
            {
                float? distance;
                if (textboxAfstand.Text != "" && float.TryParse(textboxAfstand.Text, out float _))
                    distance = float.Parse(textboxAfstand.Text);
                else
                    distance = null;

                int? avgWatt;
                if (textboxWattage.Text != "" && int.TryParse(textboxWattage.Text, out int _))
                    avgWatt = int.Parse(textboxWattage.Text);
                else
                    avgWatt = null;

                BikeType bikeType = BikeType.CityBike;
                switch (comboboxFietsType.SelectedIndex)
                {
                    case 0:
                        bikeType = BikeType.IndoorBike;
                        break;
                    case 1:
                        bikeType = BikeType.RacingBike;
                        break;
                    case 2:
                        bikeType = BikeType.CityBike;
                        break;
                    case 3:
                        bikeType = BikeType.MountainBike;
                        break;
                    default:
                        break;
                }

                BackEndData.AddCyclingTraining(when, distance, time, avgSpeed, avgWatt, trainingType, bikeType, comments);
            }
            else
            {
                int distance = int.Parse(textboxAfstand.Text);
                BackEndData.AddRunningTraining(when, distance, time, avgSpeed, trainingType, comments);
            }
        }

        private TimeSpan DoubleToTimeSpan(double toConvert)
        {
            int fullHours = (int)toConvert;
            int minutes = (int)toConvert - fullHours;
            return new TimeSpan(fullHours, minutes, 0);
        }
    }
}
