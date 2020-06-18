using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Text.Format;

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

            //For Testing
            var now = DateTime.Now;
            var triggerTime = new DateTime(now.Year, now.Month, now.Day, 18,
                45, 0);

            var triggerOff = new DateTimeOffset(triggerTime);

            var diff = DateTimeOffset.Now.ToLocalTime().Millisecond -
                triggerOff.ToLocalTime().Millisecond;

            alarmManager.SetRepeating(AlarmType.RtcWakeup, diff,
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