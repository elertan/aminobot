using System.Collections.Generic;

namespace AminoApi
{
    public interface IModelResolvable
    {
        void JsonResolve(Dictionary<string, object> data);
    }
}