using System;
using SPapi.NET.Enums;

namespace SPapi.NET.Events
{
    public class WeatherUpdateEventArgs : EventArgs
    {
        public Weather Before { get; set; }
        public Weather Weather { get; set; }
    }
}
