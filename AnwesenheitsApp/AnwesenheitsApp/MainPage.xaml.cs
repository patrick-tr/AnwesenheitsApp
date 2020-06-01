using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using Java.Util;
using Android.Content;
using Android.OS;
using Xamarin.Forms.Internals;

namespace AnwesenheitsApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private string _PlaceName;
        private string _CurrentLocation;
        private string _CurrentDate;
        private string _ServiceControllBtnText;
        private bool _ServiceCtrlBtnState;

        public string ServiceControllBtnText
        {
            get
            {
                return this._ServiceControllBtnText;
            }
            private set
            {
                this._ServiceControllBtnText = value;
                OnPropertyChanged();
            }
        }
        public string CurrentLocation
        {
            get
            {
                return this._CurrentLocation;
            }
            private set
            {
                this._CurrentLocation = value;
                OnPropertyChanged();
            }
        }
        public string PlaceName
        {
            get
            {
                return this._PlaceName;
            }
            private set
            {
                this._PlaceName = value;
                OnPropertyChanged();
            }
        }
        public string CurrentDate
        {
            get
            {
                return this._CurrentDate;
            }
            private set
            {
                this._CurrentDate = value;
                OnPropertyChanged();
            }
        }

        public MainPage()
        {
            GetLastKownLocation();
            GetCurrentDate();
            this.ServiceControllBtnText = "Start";
            this._ServiceCtrlBtnState = false;

            InitializeComponent();

            var log = new Logging.Logging();

            log.WriteLogEntry("[TEST] App gestartet!");

            this.BindingContext = this;
        }

        public void DownSwipeEvent(object sender, SwipedEventArgs e)
        {
            ReloadPage();
        }

        public void ServiceCtrlBtn_Clicked(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            this._ServiceCtrlBtnState = !this._ServiceCtrlBtnState;
            this.ServiceControllBtnText = this._ServiceCtrlBtnState == true ? "Stop" : "Start";
            btn.BackgroundColor = this._ServiceCtrlBtnState == true ? Color.Red : Color.Green;
        }

        public void LogButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LogPage());
        }

        private void ReloadPage()
        {
            GetCurrentLocation();
            GetCurrentDate();
        }

        private void GetCurrentDate()
        {
            this.CurrentDate = DateTime.Now.ToString("dd.MM.yyyy");
        }

        private async void GetLastKownLocation()
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                    this.CurrentLocation = location.Latitude.ToString("#.0000") +
                        ", " + location.Longitude.ToString("#.0000");

                    GetPlaceNameFromCoord(location.Longitude, location.Latitude);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private async void GetCurrentLocation()
        {
            try
            {
                var location = await Geolocation.GetLocationAsync();

                if (location != null)
                {
                    this.CurrentLocation = location.Latitude.ToString("#.0000") +
                        ", " + location.Longitude.ToString("#.0000");

                    GetPlaceNameFromCoord(location.Longitude, location.Latitude);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private async void GetPlaceNameFromCoord(double lon, double lat)
        {
            try
            {
                var placemarks = await Geocoding.GetPlacemarksAsync(lat, lon);

                var placemark = placemarks?.FirstOrDefault();
                if (placemark != null)
                {
                    this.PlaceName = placemark.Locality + ", " + placemark.PostalCode +
                        "\n" + placemark.AdminArea + ", " + placemark.CountryName;
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
