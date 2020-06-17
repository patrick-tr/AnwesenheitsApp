using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Icu.Util;
using Android.OS;
using Android.Runtime;
using Android.Text.Format;
using Android.Views;
using Android.Widget;

namespace AnwesenheitsApp.Droid.Alarm
{
    class AlarmHandler
    {
        public void SetAlarm()
        {
            var alarmIntent = new Intent(Application.Context, typeof(AlarmReciver));
            var pending = PendingIntent.GetBroadcast(Application.Context,
                0, alarmIntent, PendingIntentFlags.UpdateCurrent);
            var alarmManager = Application.Context.GetSystemService(Application.AlarmService)
                .JavaCast<AlarmManager>();

            var now = DateTime.Now;

            var dt = new DateTime(now.Year, now.Month, now.Day,
                19, 5, 0, 0).Ticks / 10000;

            long millis = dt - now.Ticks / 10000;

            alarmManager.SetRepeating(AlarmType.RtcWakeup, millis,
                1000 * 60 * 60 * 4, pending);
        }

        public void UnsetAlarm()
        {
            var alarmIntent = new Intent(Application.Context, typeof(AlarmReciver));
            var pending = PendingIntent.GetBroadcast(Application.Context,
                0, alarmIntent, PendingIntentFlags.UpdateCurrent);
            var alarmManager = Application.Context.GetSystemService(Application.AlarmService)
                .JavaCast<AlarmManager>();

            alarmManager.Cancel(pending);
        }
    }
}