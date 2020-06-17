using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AnwesenheitsApp.Droid.PositionService;
using SQLitePCL;

[assembly: Xamarin.Forms.Dependency(typeof(PositionServiceHelpers))]
namespace AnwesenheitsApp.Droid.PositionService
{
    internal class PositionServiceHelpers : IPositionService
    {
        private static Context _context = global::Android.App.Application.Context;
        private Logging.Logging _logger = new Logging.Logging();

        public bool ServiceState { get; set; }

        public void StartPositionService()
        {
            var intent = new Intent(_context, typeof(PositionServiceDroid));

            try
            {
                if (Android.OS.Build.VERSION.SdkInt >= BuildVersionCodes.O)
                    _context.StartForegroundService(intent);
                else
                    _context.StartService(intent);

                this.ServiceState = true;
                this._logger.WriteLogEntry(Logging.LoggingType.INFO,
                    "Position service successful started!");
            }
            catch(Exception ex)
            {
                this._logger.WriteLogEntry(Logging.LoggingType.ERROR,
                    ex.Message + " StartPositionService() in class" +
                    " PositionServiceHelpers");
            }
        }

        public void StopPositionService()
        {
            try
            {
                var intent = new Intent(_context, typeof(PositionServiceDroid));
                _context.StopService(intent);

                this.ServiceState = false;
                this._logger.WriteLogEntry(Logging.LoggingType.INFO,
                    "Position service succesfull stoped!");
            }
            catch(Exception ex)
            {
                this._logger.WriteLogEntry(Logging.LoggingType.INFO,
                    ex.Message + " StopPositionService() in class" +
                    " PositionServiceHelpers");
            }
        }
    }
}