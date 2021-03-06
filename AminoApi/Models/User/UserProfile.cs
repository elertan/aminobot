﻿using System.Collections.Generic;

namespace AminoApi.Models.User
{
    public class UserProfile : ModelBase
    {
        public int Status { get; set; }
        public string Uid { get; set; }
        public string Nickname { get; set; }
        public string IconUrl { get; set; }

        public override void JsonResolve(Dictionary<string, object> data)
        {
            if (data.ContainsKey("userProfile"))
            {
                JsonResolve(data["userProfile"].ToJObject().ToObject<Dictionary<string, object>>());
                return;
            }

            Status = data.Resolve<int>("status");
            Uid = data.Resolve<string>("uid");
            Nickname = data.Resolve<string>("nickname");

            var iconString = data.Resolve<string>("icon");
            if (iconString != null)
                IconUrl = iconString;
        }
    }
}