using System;

namespace SPapi.NET.Events
{
    public class MessageAddEventArgs : EventArgs
    {
        public string Nickname { get; set; }
        public string Uuid { get; set; }
        public string Content { get; set; }
        public DateTime Time { get; set; }
    }
}
