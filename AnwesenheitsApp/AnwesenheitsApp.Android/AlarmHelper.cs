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
using AnwesenheitsApp.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(AlarmHelper))]
namespace AnwesenheitsApp.Droid
{
    internal class AlarmHelper : IAlarmHandler
    {
        public void Start()
        {
            var alarmIntent = new Intent(Application.Context, typeof(AlarmReciver));
            alarmIntent.PutExtra("title", "Hallo!");
            alarmIntent.PutExtra("message", "Ich bin ein vom Alarm Manager ausgelöster Code!");

            var pending = PendingIntent.GetBroadcast(Application.Context,
                0, alarmIntent, PendingIntentFlags.UpdateCurrent);

            var alarmManager = Application.Context.GetSystemService(Application.AlarmService).JavaCast<AlarmManager>();

            alarmManager.Set(AlarmType.ElapsedRealtimeWakeup, SystemClock.ElapsedRealtime() + 2 * 60 * 1000,
                pending);
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}