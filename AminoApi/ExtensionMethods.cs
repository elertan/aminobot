using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace AminoApi
{
    public static class ExtensionMethods
    {
        public static Dictionary<string, object> ToDictionary(this JObject @object)
        {
            return @object.ToObject<Dictionary<string, object>>();
        }

        public static JObject ToJObject(this object obj)
        {
            return (JObject) obj;
        }
    }
}