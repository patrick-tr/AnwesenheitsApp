using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using AnwesenheitsApp.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(NotificationManagerDroid))]
namespace AnwesenheitsApp.Droid
{
    internal class NotificationManagerDroid : INotificationManager
    {
        const string channelId = "default";
        const string channelName = "Default";
        const string channelDescription = "The default channel for notifications.";
        const int pendingIntentId = 0;

        public const string TitleKey = "title";
        public const string MessageKey = "message";

        bool channelInitialized = false;
        int messageId = -1;
        NotificationManager manager;

        public void Initialize()
        {
            CreateNotificationChannel();
        }

        public int ScheduleNotification(string title, string message)
        {
            if (!this.channelInitialized)
                CreateNotificationChannel();

            this.messageId++;

            Intent intent = new Intent(Application.Context, typeof(MainActivity));
            intent.PutExtra(TitleKey, title);
            intent.PutExtra(MessageKey, message);

            PendingIntent pending = PendingIntent.GetActivity(Application.Context,
                pendingIntentId, intent, PendingIntentFlags.OneShot);

            NotificationCompat.Builder builder = new NotificationCompat.Builder(
                Application.Context, channelId)
                .SetContentIntent(pending)
                .SetContentTitle(title)
                .SetContentText(message)
                .SetSmallIcon(Resource.Drawable.abc_btn_radio_material)
                .SetDefaults((int)NotificationDefaults.Vibrate | (int)NotificationDefaults.Sound);

            manager.Notify(messageId, builder.Build());

            return messageId;
        }

        private void CreateNotificationChannel()
        {
            this.manager = (NotificationManager)Application.Context.GetSystemService(
                Application.NotificationService);

            if(Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                var channelNameJava = new Java.Lang.String(channelName);
                var channel = new NotificationChannel(
                    channelId, channelNameJava, NotificationImportance.Default)
                {
                    Description = channelDescription
                };
                manager.CreateNotificationChannel(channel);
            }
            this.channelInitialized = true;
        }
    }
}