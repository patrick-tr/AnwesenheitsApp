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

namespace AnwesenheitsApp.Droid
{
    [Service]
    class PositionServiceDroid : Service
    {
        public override StartCommandResult OnStartCommand(Intent intent,
            StartCommandFlags flags, int startId)
        {
            int messageID;

            var notifMngr = new NotificationManagerDroid();
            Notification notification = notifMngr.ReturnNotification(
                "Positions Service", "Die überwachung der Position für die" +
                " automatische Prüfung der Anwesenheit läuft!", out messageID);

            StartForeground(messageID, notification);

            //Todo: Long Running Opperation

            return StartCommandResult.Sticky;
        }

        public override bool StopService(Intent name)
        {
            return base.StopService(name);
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
        }

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }
    }
}