using System.Text.RegularExpressions;

namespace DocumentStoreManagement.Helpers
{
    public partial class ToKebabParameterTransformer : IOutboundParameterTransformer
    {
        public string TransformOutbound(object value) => value != null
            ? MyRegex().Replace(value.ToString(), "$1-$2").ToLower() // To kebab 
            : null;

        [GeneratedRegex("([a-z])([A-Z])")]
        private static partial Regex MyRegex();
    }
}
