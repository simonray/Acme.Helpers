using System.Collections.Generic;

namespace Acme.Helpers.Core.Extensions
{
    /// <exclude/>
    internal static partial class ExtensionMethods
    {
        /// <exclude/>
        public static IDictionary<string, string> CombineAttribute(this IDictionary<string, string> @this, string key, string value)
        {
            if (@this.ContainsKey(key))
            {
                if (@this[key] == null)
                    @this[key] = value;
                else if (@this[key] != value &&
                    !@this[key].Contains($" {value}") &&
                    !@this[key].Contains($"{value} "))
                {
                    if (key.Equals("class"))
                        @this[key] += $" {value}";
                    else
                        @this[key] += $"; {value}";
                }
            }
            else
                @this.Add(key, value);
            return @this;
        }
    }
}
