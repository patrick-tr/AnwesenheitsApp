using System;
using System.Collections.Generic;
using System.Text;

namespace AnwesenheitsApp
{
    public interface IPositionService
    {
        void StartPositionService();
        void StopPositionService();
    }
}
