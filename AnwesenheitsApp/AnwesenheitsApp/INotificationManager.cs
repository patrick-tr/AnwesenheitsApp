using System;
using System.Collections.Generic;
using System.Text;

namespace AnwesenheitsApp
{
    public interface INotificationManager
    {
        void Initialize();

        int ScheduleNotification(string title, string message);
    }
}
