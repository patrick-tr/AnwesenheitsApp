using System;
using System.Collections.Generic;
using System.Text;

namespace AnwesenheitsApp
{
    public interface IAlarmHandler
    {
        void Start();
        void Stop();
    }
}
