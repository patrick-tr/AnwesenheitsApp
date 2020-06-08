﻿using System;
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
    [BroadcastReceiver]
    class AlarmReciver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            NotificationManagerDroid nfm = new NotificationManagerDroid();
            nfm.ScheduleNotification(
                intent.GetStringExtra("title"), intent.GetStringExtra("message"));
        }
    }
}