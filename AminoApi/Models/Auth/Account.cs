using System.Collections.Generic;
using AminoApi.Models.User;

namespace AminoApi.Models.Auth
{
    public class Account : UserProfile
    {
        public string Secret { get; set; }
        public string Sid { get; set; }
        public bool PhoneNumberActivation { get; set; }
        public bool EmailActivation { get; set; }
        public string FacebookId { get; set; }
        public AdvancedSettings AdvancedSettings { get; set; }
        public string Email { get; set; }

        public override void JsonResolve(Dictionary<string, object> data)
        {
            Secret = data.Resolve<string>("secret");
            Sid = data.Resolve<string>("sid");

            var account = data["account"].ToJObject().ToDictionary();

            base.JsonResolve(account);
            Email = account.Resolve<string>("email");

            AdvancedSettings = new AdvancedSettings();
            AdvancedSettings.JsonResolve(account["advancedSettings"].ToJObject().ToDictionary());
        }
    }
}