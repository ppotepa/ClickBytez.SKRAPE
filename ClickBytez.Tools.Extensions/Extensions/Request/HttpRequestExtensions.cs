using ClickBytez.Tools.Extensions.ContentType;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json.Linq;
using System.Buffers;
using System.IO.Pipelines;
using System.Text;

namespace ClickBytez.Tools.Extensions.Request
{
    public static class HttpRequestExtensions
    {
        public static ExtendedContentType GetContentType(this IHeaderDictionary @this)
        {
            StringValues stringValues = string.Empty;
            bool success = @this.TryGetValue("Content-Type", out stringValues);
            return new ExtendedContentType(stringValues);
        }
    }

    public static class ReadResultExtensions
    {
        public static string GetStringFromResult(this ReadResult @this)
        {
            string result = string.Empty;
            ReadOnlySequence<byte>.Enumerator enumerator = @this.Buffer.GetEnumerator();

            do { result += Encoding.UTF8.GetString(enumerator.Current.Span); }
            while (enumerator.MoveNext());
            return result;
        }

        public static JObject GetJObject(this ReadResult @this)
        {
            string @string = GetStringFromResult(@this);
            JObject @object = JObject.Parse(@string);
            return @object;
        }
    }
}
