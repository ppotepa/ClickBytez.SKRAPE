using Newtonsoft.Json;

namespace ClickBytez.Tools.Extensions.Object
{
    public static class ObjectExtensions
    {
        public static string ToJson(this object @object, bool indent = false) 
            => JsonConvert.SerializeObject(@object, indent ? Formatting.Indented : Formatting.None);

    }
}
