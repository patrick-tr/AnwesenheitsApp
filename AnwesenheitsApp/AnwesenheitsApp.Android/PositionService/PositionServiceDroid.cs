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
using AnwesenheitsApp.DbModels;
using AnwesenheitsApp.Droid.Alarm;
using Xamarin.Essentials;

namespace AnwesenheitsApp.Droid
{
    [Service]
    class PositionServiceDroid : Service
    {
        private Logging.Logging _logger = new Logging.Logging();
        private static AlarmHandler alarm = new AlarmHandler();

        public override StartCommandResult OnStartCommand(Intent intent,
            StartCommandFlags flags, int startId)
        {
            int messageID = 90000;

            var notifMngr = new NotificationManagerDroid();
            Notification notification = notifMngr.ReturnNotification(
                "Positions Service", "Die überwachung der Position für die" +
                " automatische Prüfung der Anwesenheit läuft!");

            StartForeground(messageID, notification);

            alarm.SetAlarm();

            return StartCommandResult.Sticky;
        }

        public override bool StopService(Intent name)
        {
            alarm.UnsetAlarm();
            return base.StopService(name);
        }

        public override void OnDestroy()
        {
            alarm.UnsetAlarm();
            base.OnDestroy();
        }

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

       
    }
}