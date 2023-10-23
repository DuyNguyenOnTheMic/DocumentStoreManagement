using System.Text.RegularExpressions;

namespace DocumentStoreManagement.Helpers
{
    /// <summary>
    /// Helper class to transform URL to kebab-case
    /// </summary>
    public partial class ToKebabParameterTransformer : IOutboundParameterTransformer
    {
        /// <summary>
        /// Transform URL into kebab-case
        /// </summary>
        /// <param name="value"></param>
        public string TransformOutbound(object value) => value != null
            ? MyRegex().Replace(value.ToString(), "$1-$2").ToLower() // To kebab 
            : null;

        [GeneratedRegex("([a-z])([A-Z])")]
        private static partial Regex MyRegex();
    }
}
