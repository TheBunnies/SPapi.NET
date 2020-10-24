using System;
using SPapi.NET.Enums;

namespace SPapi.NET.Events
{
    public class TimeTicksUpdateEventArgs : EventArgs
    {
        public DayTime DayTime { get; set; }
        public int Ticks { get; set; }
    }
}
