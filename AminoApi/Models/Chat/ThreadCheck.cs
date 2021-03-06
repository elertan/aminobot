﻿using System;

namespace AminoApi.Models.Chat
{
    public class ThreadCheck : ModelBase
    {
        public string ThreadId { get; set; }
        public DateTime LastMessageTime { get; set; }
        public DateTime YourLastMessageTime { get; set; }
        public int SomeNumber { get; set; }

        public override void JsonResolveArray(object[] data)
        {
            ThreadId = Convert.ToString(data[0]);
            LastMessageTime = DateTime.Parse(Convert.ToString(data[1]));
            YourLastMessageTime = DateTime.Parse(Convert.ToString(data[2]));
            SomeNumber = Convert.ToInt32(data[3]);
        }

        public bool HasReceivedNewMessage => YourLastMessageTime != LastMessageTime;
    }
}