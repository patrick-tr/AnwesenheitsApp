using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnwesenheitsApp.DbModels;
using Xamarin.Essentials;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace AnwesenheitsApp.Droid.Alarm
{
    [BroadcastReceiver]
    class AlarmReciver : BroadcastReceiver
    {
        private LocationData _data;
        private Logging.Logging _logger = new Logging.Logging();

        public override void OnReceive(Context context, Intent intent)
        {
            Test();
        }

        private void Test()
        {
            GetLocationData();
        }

        private void SaveDataToDb(LocationData data)
        {
            App.Database.SaveLocationDataToDbAsync(data);
        }

        private async void GetLocationData()
        {
            if (this._data == null)
                this._data = new LocationData();


            try
            {
                var location = await Geolocation.GetLocationAsync();
                if (location == null)
                    location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                    this._data.Latitude = location.Latitude;
                    this._data.Longitude = location.Longitude;

                    var placemark = (await Geocoding.GetPlacemarksAsync(
                        location.Latitude, location.Longitude))?.FirstOrDefault();
                    if (placemark != null)
                    {
                        this._data.Locality = placemark.Locality;
                        this._data.ZipCode = placemark.PostalCode;
                        this._data.AdminArea = placemark.AdminArea;
                        this._data.Country = placemark.CountryName;
                    }
                }
                this._data.CreationDate = DateTime.Now;
                SaveDataToDb(this._data);
            }
            catch (Exception ex)
            {
                this._logger.WriteLogEntry(Logging.LoggingType.ERROR,
                    ex.Message + " GetLocationData() in class PositionServiceDroid");
            }
        }
    }
}