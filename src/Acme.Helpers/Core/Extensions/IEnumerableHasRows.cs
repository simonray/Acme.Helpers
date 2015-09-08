using System.Collections;

namespace Acme.Helpers.Core.Extensions
{
    /// <exclude/>
    internal static partial class ExtensionMethods
    {
        /// <exclude/>
        public static bool HasRows(this IEnumerable @this)
        {
            var rows = @this.GetEnumerator();
            rows.Reset();
            return rows.MoveNext();
        }
    }
}
