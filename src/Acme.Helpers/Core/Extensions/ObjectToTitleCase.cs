using System;

namespace Acme.Helpers.Core.Extensions
{
    /// <exclude/>
    internal static partial class ExtensionMethods
    {
        /// <exclude/>
        public static String ToTitleCase(this object s)
        {
            if (s == null) return s.ToString();

            String[] words = s.ToString().Split(' ');
            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Length == 0) continue;

                Char firstChar = Char.ToUpper(words[i][0]);
                String rest = "";
                if (words[i].Length > 1)
                {
                    rest = words[i].Substring(1).ToLower();
                }
                words[i] = firstChar + rest;
            }
            return String.Join(" ", words);
        }
    }
}
